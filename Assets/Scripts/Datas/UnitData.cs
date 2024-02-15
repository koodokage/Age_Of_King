using AgeOfKing.Abstract.Components;
using AgeOfKing.Abstract.Datas;
using UnityEngine;

namespace AgeOfKing.Datas
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Unit", order = 2)]
    public class UnitData : AEntityData
    {
        [SerializeField] AUnit unitPrefab;
        [Header("VISUALIZATION")]
        [SerializeField] Sprite spriteImage;
        [Header("STATS")]
        [SerializeField] protected int movement = 5;
        [SerializeField] protected int attack = 2;
        [SerializeField] protected int health = 10;

        public AUnit GetPrefab { get => unitPrefab; }
        public Sprite GetSprite { get => spriteImage; }
        public int GetMovement { get => movement; }
        public int GetAttack { get => attack; }
        public int GetHealth { get => health; }

    }

}