using System;
using System.Collections.Generic;
using UnityEngine;
namespace FindTheWay.Map.Objects
{
    /// <summary>
    /// Collect and destroy all map objects 
    /// </summary>
    public class ObjectContainer
    {
        public event Action OnPointsAdded;

        Dictionary<Vector2Int, GameObject> obstacles = new Dictionary<Vector2Int, GameObject>();
        List<GameObject> path = new List<GameObject>();
        Dictionary<ElementType, GameObject> points = new Dictionary<ElementType, GameObject>();

        public void Add(GameObject newObject, ElementType type, Vector2Int cell)
        {
            if (newObject == null)
                return;
            switch (type)
            {
                case ElementType.pointA:
                case ElementType.pointB:
                    points.Add(type, newObject);
                    if (points.Count == 2)
                        OnPointsAdded?.Invoke();
                    break;
                case ElementType.obsticle:
                    obstacles.Add(cell, newObject);
                    break;
                case ElementType.path:
                    path.Add(newObject);
                    break;
            }

        }

        public bool CheckPoints(Vector3 position, ElementType type)
        {
            GameObject point;
            if (points.TryGetValue(type, out point))
            {
                if (point != null)
                {
                    point.transform.position = position;
                    return true;
                }
            }
            return false;
        }

        public void DeleteObstacle(Vector2Int cell)
        {

            if (obstacles.TryGetValue(cell, out GameObject value) && value != null)
            {
                GameObject.Destroy(value);
                obstacles.Remove(cell);
            }
        }

        public void Clear()
        {
            ClearObstacles();
            ClearPoints();
            ClearPath();
        }

        public void ClearPath()
        {
            foreach (GameObject gameObject in path)
            {
                GameObject.Destroy(gameObject);
            }
            path.Clear();
        }

        private void ClearPoints()
        {
            foreach (GameObject gameObject in points.Values)
            {
                GameObject.Destroy(gameObject);
            }
            points.Clear();
        }

        private void ClearObstacles()
        {
            foreach (GameObject gameObject in obstacles.Values)
            {
                if (gameObject != null)
                {
                    GameObject.Destroy(gameObject);
                }
            }
            obstacles.Clear();
        }

    }
}