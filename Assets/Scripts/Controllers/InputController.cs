using FindTheWay.Character;
using FindTheWay.Map;
using FindTheWay.UIInput;
using UnityEngine;
using UnityEngine.UI;

namespace FindTheWay.Controllers
{
    /// <summary>
    /// Control UI input  
    /// </summary>
    public class InputController : MonoBehaviour
    {
        [Header("Conected components")]
        [SerializeField] Selector selector;
        [SerializeField] MapBuilder mapBuilder;
        [SerializeField] Tiling tiling;
        [SerializeField] InputCorrector inputCorrector;
        [SerializeField] SpawnCharacter spawnCharacter;
        [Header("UI elements")]
        [SerializeField] Button buttonFindPath;
        [SerializeField] ToggleGroup toggleGroup;
        [SerializeField] GameObject messagePanel;
        [SerializeField] GraphicRaycaster uiRaycaster;

        private void Awake()
        {
            inputCorrector.OnResize += OnResizeMap;
            spawnCharacter.OnRunnerFinished += EnableUI;
            mapBuilder.OnNoPath += ShowMessageNoWay;
            mapBuilder.AddListenerPointsAdded(EnableFindPath);
        }

        public void FindPath()
        {
            DisableUI();
            DeselectObject();
            mapBuilder.GeneratePath();
        }

        public void SelectObject(int numType)
        {
            ElementType type = (ElementType)numType;
            mapBuilder.SetCurrentType(type);
            selector.StartMoving();
        }

        private void DeselectObject()
        {
            selector.StopMoving();
            mapBuilder.SetCurrentType(ElementType.none);
            toggleGroup.SetAllTogglesOff();
        }

        private void OnResizeMap(int width, int hight)
        {
            mapBuilder.Resize(width, hight);

            tiling.SetScale(width, hight);

            DeselectObject();

            buttonFindPath.interactable = false;
        }

        private void EnableFindPath()
        {
            buttonFindPath.interactable = true;
        }

        private void DisableUI()
        {
            uiRaycaster.enabled = false;
        }

        private void EnableUI()
        {
            uiRaycaster.enabled = true;
        }

        private void ShowMessageNoWay()
        {
            messagePanel.SetActive(true);
        }

        private void OnDestroy()
        {
            inputCorrector.OnResize -= OnResizeMap;
            spawnCharacter.OnRunnerFinished -= EnableUI;
            mapBuilder.OnNoPath -= ShowMessageNoWay;
        }
    }

}
