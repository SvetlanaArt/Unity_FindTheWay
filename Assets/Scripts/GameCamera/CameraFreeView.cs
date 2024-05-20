using UnityEngine;

namespace FindTheWay.GameCamera
{
	/// <summary>
	/// Allow free view on game
	/// </summary>
	public class CameraFreeView : MonoBehaviour
	{

		[SerializeField] Transform target;
		[SerializeField] Transform mainCamera;

		[Header("Rotate")]
		[SerializeField] float speedRotation = 3f;
		[SerializeField] float minVertical;
		[SerializeField] float maxVertical;

		[Header("Zoom")]
		[SerializeField] float speedZoom = 3f;
		[SerializeField] float minZoom = 1.5f;
		[SerializeField] float maxZoom = 25f;

		[Header("Move")]
		[SerializeField] float speedMove = 3f;

		[Header("Auto")]
		[SerializeField] float zoom;
		[SerializeField] Vector3 offsetPosition;

		private Vector3 newRotation;
		private bool isFreeView;

		private Vector3 startOffsetPosition;
		private float startZoom;
		private Quaternion startRotation;

		void Start()
		{
			isFreeView = false;
			newRotation = new Vector3();
			startZoom = zoom;
			startOffsetPosition = offsetPosition;
			startRotation = mainCamera.localRotation;
			AutoCamera();
		}

		void LateUpdate()
		{
			if (isFreeView)
			{
				ZoomByScroll();
				MoveForward();
				mainCamera.localEulerAngles = GetNewRotation();
				mainCamera.position = GetNewPosition(zoom, offsetPosition);
			}
			if (Input.GetMouseButtonUp(1))
				isFreeView = !isFreeView;
		}

		public void AutoCamera()
		{
			mainCamera.localRotation = startRotation;
			mainCamera.position = GetNewPosition(startZoom, startOffsetPosition);
		}

		public bool GetIsFreeMode()
		{
			return isFreeView;
		}

		private void MoveForward()
		{
			if (Input.GetMouseButton(0))
			{
				offsetPosition += mainCamera.forward * Time.deltaTime * speedMove;
				offsetPosition.y = 0;
			}
		}

		private void ZoomByScroll()
		{
			float scroll = Input.GetAxis("Mouse ScrollWheel");
			if (scroll != 0)
			{
				zoom += Mathf.Sign(scroll) * speedZoom;
			}
		}

		private Vector3 GetNewRotation()
		{
			newRotation.y = mainCamera.localEulerAngles.y + Input.GetAxis("Mouse X") * speedRotation;
			newRotation.x = mainCamera.localEulerAngles.x - Input.GetAxis("Mouse Y") * speedRotation;
			newRotation.x = Mathf.Clamp(newRotation.x, minVertical, maxVertical);

			return newRotation;
		}

		private Vector3 GetNewPosition(float zoom, Vector3 offset)
		{
			zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
			Vector3 zoomOffset = new Vector3(0, 0, -zoom);
			return mainCamera.localRotation * zoomOffset + target.position + offset;
		}

	}
}