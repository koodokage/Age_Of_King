using System;
using UnityEngine;

namespace AgeOfKing.Data
{
    public enum CharacterStatGenre {MOVEMENT,ATTACK,ATTACKRANGE,HEALTH}

    [Serializable]
    public struct CharacterStatData
    {
        [SerializeField] private CharacterStatGenre genre;
        [SerializeField] private int value;

        public CharacterStatGenre GetGenre { get => genre; }
        public int GetValue { get => value; }
    }
}
