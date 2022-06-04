public interface IDamageable 
{
    int MaxHealth { get; }
    int Health { get; }

    void IncreaseHealth(int amount);
    void ReduceHealth(int amount);
}