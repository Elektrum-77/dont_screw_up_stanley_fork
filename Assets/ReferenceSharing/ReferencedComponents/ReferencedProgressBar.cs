using UnityEngine;
using UnityEngine.UI;

namespace ReferenceSharing.ReferencedComponents
{
    public class ReferencedProgressBar : ReferencedComponent<float>
    {
        [SerializeField] private Reference<float> maxFloatValueRef;
        private Image _fill;

        private void Awake()
        {
            _fill = GetComponent<Image>();
        }

        protected override void OnValueChanged(float value)
        {
            _fill.fillAmount = valueRef.Value / maxFloatValueRef.Value;
        }
    }
}