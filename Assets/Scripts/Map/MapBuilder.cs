using System;
using System.Collections.Generic;
using FindTheWay.Map.Objects;
using FindTheWay.Map.Path;
using UnityEngine;

namespace FindTheWay.Map
{
    /// <summary>
    /// Build objects on the map and give information to the 
    /// </summary>
    public class MapBuilder : MonoBehaviour
    {
        [SerializeField] ObjectData objectData;
        [SerializeField] GameObject parentObjects;
        [SerializeField] ObjectsAudio objectsAudio;

        public event Action<List<Vector2Int>> OnPathGenerated;
        public event Action OnNoPath;

        ObjectContainer objectContainer = new ObjectContainer();
        GridMap map = new GridMap();
        ElementType currentType = ElementType.none;

        public void CreateObject(Vector3 position, Vector2Int cell)
        {
            if (currentType == ElementType.none)
                return;

            if (objectContainer.CheckPoints(position, currentType))
            {
                map.SetElement(cell, currentType);
                return;
            }

            GameObject newObject = CreateObjectByType(position, currentType, cell);
            if (newObject == null)
                return;

            objectsAudio.PlayPutObject(position);

            map.SetElement(cell, currentType);
        }

        public GameObject CreateObjectByType(Vector3 position, ElementType type, Vector2Int cell)
        {
            GameObject prefab = objectData.GetPrefab(type);
            if (prefab == null)
                return null;

            GameObject newObject = Instantiate(prefab,
                                 position,
                                 Quaternion.identity,
                                 parentObjects.transform);

            objectContainer.Add(newObject, type, cell);

            if (type == ElementType.pointB)
                AddFinishSoundToPlay(newObject);

            return newObject;
        }

        private void AddFinishSoundToPlay(GameObject pointB)
        {
            Finish finish = pointB.GetComponentInChildren<Finish>();
            if (finish != null)
                finish.OnSelebrateFinish += objectsAudio.PlayReachPointB;
        }

        public void SetCurrentType(ElementType typeObject)
        {
            currentType = typeObject;
        }

        public ElementType GetCurrentType()
        {
            return currentType;
        }

        public void Resize(int width, int hight)
        {
            map.SetSize(width, hight);
            objectContainer.Clear();
        }

        public void AddListenerPointsAdded(Action listener)
        {
            if (objectContainer == null)
                objectContainer = new ObjectContainer();
            objectContainer.OnPointsAdded += listener;
        }

        public bool isMapCellFree(Vector2Int cell)
        {
            if (map == null)
                map = new GridMap();
            return map.isCellFree(cell);
        }

        public void GeneratePath()
        {
            objectContainer.ClearPath();
            List<Vector2Int> path = map.GetPath();
            if (path != null)
            {
                OnPathGenerated?.Invoke(path);
                return;
            }
            OnNoPath?.Invoke();
        }

        public void TryDeleteObsticle(Vector2Int cell)
        {
            map.DeleteObstacle(cell);
            objectContainer.DeleteObstacle(cell);
        }
    }

}
