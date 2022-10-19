using TMPro;
using UnityEngine;

namespace ReferenceSharing.ReferencedComponents
{
    public class ReferencedTextMeshProUGUIDouble : ReferencedComponent<double>
    {
        [SerializeField] private string format, prefix, suffix;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void OnValueChanged(double value)
        {
            _text.text = $"{prefix}{value.ToString(format)}{suffix}";
        }
    }
}