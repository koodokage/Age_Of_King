using AgeOfKing.Systems;

namespace AgeOfKing.Abstract.Components
{
    public interface ITurnListener
    {
        public void OnTurnChange(IPlayer side, int turnIndex);
    }
}
