using TMPro;
using UnityEngine;

namespace ReferenceSharing.ReferencedComponents
{
    public class ReferencedTextMeshProUGUIFloat : ReferencedComponent<float>
    {
        [SerializeField] private string format, prefix, suffix;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void OnValueChanged(float value)
        {
            _text.text = $"{prefix}{value.ToString(format)}{suffix}";
        }
    }
}