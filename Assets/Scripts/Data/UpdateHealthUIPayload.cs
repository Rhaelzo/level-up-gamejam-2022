public class UpdateHealthUIPayload : MessageContentPayload
{
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Difference { get; private set; }

    public UpdateHealthUIPayload(int health, int maxHealth, int difference)
    {
        Health = health;
        MaxHealth = maxHealth;
        Difference = difference;
    }
}
