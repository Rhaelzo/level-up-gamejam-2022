/// <summary>
/// Interface that describes an entity that can needs access to
/// an object pooler
/// </summary>
public interface IPoolUser<T>
{
    /// <summary>
    /// Loads a new object from the pool
    /// </summary>
    /// <returns>
    /// Object from the pool
    /// </returns>
    T LoadNewObjectFromPool();

    /// <summary>
    /// Returns an active object to the pool
    /// </summary>
    /// <param name="objectToReturn">
    /// Object to be returned
    /// </param>
    void ReturnObjectToPool(T objectToReturn);
}