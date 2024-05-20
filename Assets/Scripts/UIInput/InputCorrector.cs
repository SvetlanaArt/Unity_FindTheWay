using System;
using TMPro;
using UnityEngine;

namespace FindTheWay.UIInput
{
    /// <summary>
    /// Correct input values 
    /// </summary>
    public class InputCorrector : MonoBehaviour
    {
        [Header("UI elements")]
        [SerializeField] TMP_InputField width;
        [SerializeField] TMP_InputField hight;
        [Header("Default value")]
        [SerializeField] int widthDefault = 10;
        [SerializeField] int hightDefault = 10;

        public event Action<int, int> OnResize;

        private void Start()
        {
            width.text = widthDefault.ToString();
            hight.text = hightDefault.ToString();
            OnResize?.Invoke(widthDefault, hightDefault);
        }

        public void Resize()
        {
            int widthInt = GetCorrectValue(widthDefault, width);
            int hightInt = GetCorrectValue(hightDefault, hight);
            OnResize?.Invoke(widthInt, hightInt);
        }

        private int GetCorrectValue(int defaultValue, TMP_InputField fieldValue)
        {
            int value;
            if (!int.TryParse(fieldValue.text, out value) || value <= 0)
            {
                value = defaultValue;
                fieldValue.text = value.ToString();
            }
            return value;
        }
    }

}
