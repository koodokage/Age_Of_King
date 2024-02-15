using System;
using UnityEngine;

namespace AgeOfKing.Data
{
    public abstract class AModelData: MonoBehaviour
    {
        [SerializeField] protected int _value;
        public int GetValue { get => _value; }
        public event Action<int,int> OnIncrease;
        public event Action<int,int> OnDecrease;
        public event Action<int> OnChange;


        public virtual void SetValue(int amount)
        {
            _value = amount;
            OnIncrease?.Invoke(_value, 0);
            OnChange?.Invoke(_value);
        }

        public virtual void Increase(int amount)
        {
            _value += amount;
            OnIncrease?.Invoke(_value, amount);
            OnChange?.Invoke(_value);
        }

        public virtual void Decrease(int amount)
        {
            _value -= amount;
            if(_value <= 0)
            {
                _value = 0;
            }

            OnDecrease?.Invoke(_value,amount);
            OnChange?.Invoke(_value);
        }

        public virtual bool CheckEnough(int amount)
        {
            return _value >= amount;
        }

    }



}
