using AgeOfKing.Systems.UI;
using System;
using System.Collections;
using UnityEngine;

namespace AgeOfKing.Systems
{
    public class TurnManager : ASingleBehaviour<TurnManager>
    {
        IPlayer _currentPlayer;
        int _playedTurnCount;

        public IPlayer GetTurnPlayer { get => _currentPlayer; }

        public event Action<IPlayer,int> OnTurnChange;

        public event Action OnTurnPassToP1; 
        public event Action OnTurnPassToP2;


        internal void StartGame()
        {
            _playedTurnCount = 1;
            _currentPlayer = PlayerManager.P1;
            OnTurnPassToP1?.Invoke();
        }

        /// <summary>
        ///  Pass the turn to other side
        /// </summary>
        public void PassTurn(IPlayer played)
        {
            played.GetVillage.UnbindUI_PlayerVillage();

            if(played == PlayerManager.P1)
            {
                _currentPlayer = PlayerManager.P2;
                OnTurnPassToP2?.Invoke();
            }
            else
            {
                _currentPlayer = PlayerManager.P1;
                OnTurnPassToP1?.Invoke();
            }

            PlayerManager.SwapPlayer(_currentPlayer);

            _playedTurnCount++;
            _currentPlayer.GetVillage.BindUI_PlayerVillage();
            OnTurnChange?.Invoke(_currentPlayer,_playedTurnCount);
        }


        public void RegisterPlayer(IPlayer player,Action action)
        {
            if(_currentPlayer == null)
            {
                _currentPlayer = player;
                OnTurnPassToP1 += action;
                return;
            }

            OnTurnPassToP2 += action;

        }

       
    }

}