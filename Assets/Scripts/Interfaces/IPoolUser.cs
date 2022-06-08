public interface IPoolUser<T>
{
    T LoadNewObjectFromPool();
    void ReturnObjectToPool(T objectToReturn);
}