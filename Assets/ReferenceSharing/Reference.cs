using System;
using ReferenceSharing.Variables;
using UnityEngine;

namespace ReferenceSharing
{
    [Serializable]
    public class Reference<T>
    {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private T constantValue;
        [SerializeField] private Variable<T> Variable;

        public T Value
        {
            get { return (!Variable || useConstant) ? constantValue : Variable.Value;}
            set
            {
                if (useConstant)
                    constantValue = value;
                else
                    Variable.Value = value;
            }
        }

        public void AddEventListener(Action<T> callback)
        {
            if (!Variable || useConstant) return;
            Variable.OnValueChanged += callback;
        }
        
        public void RemoveEventListener(Action<T> callback)
        {
            if (!Variable || useConstant) return;
            Variable.OnValueChanged -= callback;
        }
        
        public static implicit operator T(Reference<T> reference)
        {
            return reference.Value;
        }
    }
}