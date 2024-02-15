namespace AgeOfKing.Abstract.Components
{
    public interface IResource
    {
        public string GetName { get; }
        public void Collect();
        public int Get();
    }
}
