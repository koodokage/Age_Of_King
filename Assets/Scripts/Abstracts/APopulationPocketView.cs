namespace AgeOfKing.UI
{
    public abstract class APopulationPocketView : AValuePocketView
    {
        public abstract void CapacityIncreased(int updated);
        public abstract void CapacityDecreased(int updated);
    }

}
