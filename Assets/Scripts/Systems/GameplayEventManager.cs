using System;
using UnityEngine;
using UnityEngine.Events;

namespace AgeOfKing.Systems
{
    public class GameplayEventManager : MonoBehaviour
    {
        [SerializeField] UnityEvent OnUnitMoved;
        [SerializeField] UnityEvent OnUnitAttack;
        [SerializeField] UnityEvent OnUnitHit;
        [SerializeField] UnityEvent OnUnitDie;

        private void Start()
        {
        }

    }


}
