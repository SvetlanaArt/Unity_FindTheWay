using System.Collections;
using UnityEngine;

namespace FindTheWay.UIInput
{
    /// <summary>
    /// put cellector onto pointed cell of map
    /// </summary>
    public class Selector : MonoBehaviour
    {
        [SerializeField] Position mapPosition;
        [SerializeField] GameObject cellLighter;

        Vector3Int currentCell = Vector3Int.zero;
        Coroutine moveCoroutine;
        bool isMoving = false;

        public void StartMoving()
        {
            if (isMoving)
                return;
            isMoving = true;
            cellLighter.SetActive(true);
            currentCell = Vector3Int.zero;
            moveCoroutine = StartCoroutine(FindCell());
        }

        public void StopMoving()
        {
            isMoving = false;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            cellLighter.SetActive(false);
        }

        private IEnumerator FindCell()
        {
            while (true)
            {
                Vector3Int gridPosition = mapPosition.GetGridPosition(Input.mousePosition);
                if (gridPosition != currentCell && gridPosition.z == 0)
                {
                    currentCell = gridPosition;
                    cellLighter.transform.position = mapPosition.GetMapPosition(gridPosition);
                }
                yield return null;
            }

        }

        public Vector3 GetCellPosition()
        {
            return cellLighter.transform.position;
        }

        public Vector3Int GetCurrentCell()
        {
            return currentCell;
        }

    }

}
