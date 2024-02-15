namespace AgeOfKing.UI
{
    public interface  IValueViewChangeListener
    {
        public abstract void OnValueChanged(int leftAmount);
    }

    public interface IPopulationViewChangeListener
    {
        public abstract void OnPopulationChanged(int leftAmount);
    }

}
