using UnityEngine;
using UnityEngine.UI;

namespace ReferenceSharing.ReferencedComponents
{
    public class ReferencedProgressBarInt : ReferencedComponent<int>
    {
        [SerializeField] private Reference<int> maxIntValueRef;
        private Image _fill;

        private void Awake()
        {
            _fill = GetComponent<Image>();
        }

        protected override void OnValueChanged(int value)
        {
            _fill.fillAmount = (float) valueRef.Value / maxIntValueRef.Value;
        }
    }
}