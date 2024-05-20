using UnityEngine;

namespace FindTheWay.UIInput
{
    /// <summary>
    /// make tiling for visible cells and match tiles with grid 
    /// </summary>
    public class Tiling : MonoBehaviour
    {
        [SerializeField] Material material;
        [SerializeField] Transform plane;
        [SerializeField] Grid grid;

        const float coefPlaneScale = 10f;
        const float coefGridPos = 2f;
        const float deltaGridPos = 0.0f;

        public void SetScale(int x, int y)
        {
            Vector3 newPlaneScale = new Vector3();
            newPlaneScale.x = x / coefPlaneScale;
            newPlaneScale.y = 1;
            newPlaneScale.z = y / coefPlaneScale;
            plane.localScale = newPlaneScale;

            material.mainTextureScale = new Vector2(x, y);

            SetGridPos(newPlaneScale);
        }

        private void SetGridPos(Vector3 planeScale)
        {
            planeScale *= coefPlaneScale;
            Vector3 newGridPosisition = -planeScale / coefGridPos + Vector3.one * deltaGridPos;
            newGridPosisition.y = 0;
            grid.transform.position = newGridPosisition;
        }

    }

}
