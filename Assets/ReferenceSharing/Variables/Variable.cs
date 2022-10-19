using System;
using UnityEngine;

namespace ReferenceSharing.Variables
{
    public class Variable<T> : ScriptableObject
    {
        [SerializeField] private T _value;
        public event Action<T> OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        public void SetValue(T value) => Value = value;
    }
}