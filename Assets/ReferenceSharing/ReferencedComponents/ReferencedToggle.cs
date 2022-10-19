using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ReferenceSharing.ReferencedComponents
{
    public class ReferencedToggle : ReferencedComponent<bool>
    {
        [SerializeField] private UnityEvent onTrue, onFalse;
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        protected override void OnValueChanged(bool value)
        {
            _toggle.isOn = value;
            if (value) onTrue?.Invoke();
            else onFalse?.Invoke();
        }
    }
}