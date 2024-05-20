using System.Collections.Generic;
using FindTheWay.Character;
using FindTheWay.GameCamera;
using FindTheWay.Map;
using FindTheWay.UIInput;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FindTheWay.Controllers
{
    /// <summary>
    /// Control putting objects on map 
    /// </summary>
    public class MapController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Selector selector;
        [SerializeField] Position mapPosition;
        [SerializeField] MapBuilder mapBuilder;
        [SerializeField] SpawnCharacter spawnCharacter;
        [SerializeField] CameraFreeView cameraFreeView;

        private void Start()
        {
            mapBuilder.OnPathGenerated += ShowPath;
        }

        private void ShowPath(List<Vector2Int> path)
        {
            List<Vector3> pathPoints = new List<Vector3>();
            for (int i = path.Count - 1; i >= 0; i--)
            {
                Vector3 positionOnMap = mapPosition.GetMapPosition((Vector3Int)path[i]);
                pathPoints.Add(positionOnMap);
                mapBuilder.CreateObjectByType(positionOnMap, ElementType.path, path[i]);
            }
            spawnCharacter.CreateRunner(pathPoints);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (cameraFreeView.GetIsFreeMode() ||
               (eventData.button == PointerEventData.InputButton.Right))
                return;
            Vector2Int cell = GetCell();
            if (mapBuilder.isMapCellFree(cell))
            {
                CreateObject(cell);
            }
            else
            {
                mapBuilder.TryDeleteObsticle(cell);
            }
        }

        private Vector2Int GetCell()
        {
            Vector3Int cell3D = selector.GetCurrentCell();
            return new Vector2Int(cell3D.x, cell3D.y);
        }

        private void CreateObject(Vector2Int cell)
        {
            Vector3 position = selector.GetCellPosition();
            mapBuilder.CreateObject(position, cell);
        }

        private void OnDestroy()
        {
            mapBuilder.OnPathGenerated -= ShowPath;
        }

    }

}
