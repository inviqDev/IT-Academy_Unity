namespace Interfaces
{
    public interface IPoolable
    {
        public string UniquePoolKey { get; }
        
        public void ReturnObjectToPool();
    }
}