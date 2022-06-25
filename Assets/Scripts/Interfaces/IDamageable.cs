/// <summary>
/// Interface that describes an entity that can be damaged
/// </summary>
public interface IDamageable 
{
    int MaxHealth { get; }
    int Health { get; }

    /// <summary>
    /// Increases health by given amount 
    /// </summary>
    /// <param name="amount">
    /// Amount to increase health by
    /// </param>
    void IncreaseHealth(int amount);

    /// <summary>
    /// Reduces health by given amount 
    /// </summary>
    /// <param name="amount">
    /// Amount to reduce health by
    /// </param>
    void ReduceHealth(int amount);
}