using UnityEngine;

namespace FindTheWay.UIInput
{
    /// <summary>
    /// Convert grid position to world (on map) position
    /// </summary>
    public class Position : MonoBehaviour
    {
        [SerializeField] Camera sceneCamera;
        [SerializeField] LayerMask placementLayermask;
        [SerializeField] Grid grid;

        private Vector3 lastPosition;

        public Vector3Int GetGridPosition(Vector3 position)
        {
            Vector3 positionOnMap = GetOnMap(position);
            return grid.WorldToCell(positionOnMap);
        }

        public Vector3 GetMapPosition(Vector3Int gridPosition)
        {
            return grid.CellToWorld(gridPosition); ;
        }

        private Vector3 GetOnMap(Vector3 position)
        {
            position.z = sceneCamera.nearClipPlane;
            Ray ray = sceneCamera.ScreenPointToRay(position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, placementLayermask))
            {
                lastPosition = hit.point;
            }
            return lastPosition;
        }

    }
}
