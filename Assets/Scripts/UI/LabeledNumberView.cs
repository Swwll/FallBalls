using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LabeledNumberView : MonoBehaviour
    {
        [SerializeField]
        private string _stringFormat;

        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetNumber(int number)
        {
            _text.text = String.Format(_stringFormat, number);
        }
    }
}