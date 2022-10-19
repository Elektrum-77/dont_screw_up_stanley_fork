using TMPro;
using UnityEngine;

namespace ReferenceSharing.ReferencedComponents
{
    public class ReferencedTextMeshProUGUIString : ReferencedComponent<string>
    {
        [SerializeField] private string prefix, suffix;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void OnValueChanged(string value)
        {
            _text.text = $"{prefix}{value}{suffix}";
        }
    }
}