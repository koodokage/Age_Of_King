using System;
using UnityEngine;

namespace AgeOfKing.Data
{
    public abstract class APopulationModelData : AModelData
    {
        [SerializeField] protected int _capacity;
        public int GetCapacity { get => _capacity; }
        public event Action<int> OnCapacityIncrease;
        public event Action<int> OnCapacityDecrease;

        public virtual void SetCapacity(int amount)
        {
            _capacity = amount;
            OnCapacityIncrease?.Invoke(_capacity);
        }

        public virtual void IncreaseCapacity(int amount)
        {
            _capacity += amount;
            OnCapacityIncrease?.Invoke(_capacity);
        }

        public virtual void DecreaseCapacity(int amount)
        {
            _capacity -= amount;
            if (_capacity <= 0)
            {
                _capacity = 0;
            }

            OnCapacityDecrease?.Invoke(_capacity);
        }

        public override bool CheckEnough(int amount)
        {
            return _value < _capacity;
        }

    }



}
