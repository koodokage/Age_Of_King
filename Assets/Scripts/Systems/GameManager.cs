using AgeOfKing.Systems;
using UnityEngine.Events;

public class GameManager : ASingleBehaviour<GameManager>
{
    public UnityEvent<IPlayer> OnPlayerWin;

    public void OnKingDies(IPlayer deadPlayer)
    {
        PlayerManager.OnGameOver();

        if(deadPlayer == PlayerManager.P1)
        {
            OnPlayerWin?.Invoke(PlayerManager.P2);
        }
        else
        {
            OnPlayerWin?.Invoke(PlayerManager.P1);
        }
    }
}
