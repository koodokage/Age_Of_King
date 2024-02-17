namespace AgeOfKing.Abstract.Components
{
    public interface IHittable
    {
        public int CurrentHealth { get; }
        public bool Hit(int damage);
    }

}
