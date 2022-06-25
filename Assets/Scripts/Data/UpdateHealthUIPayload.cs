/// <summary>
/// Content payload <see cref="MessageContentPayload"/> used in message
/// communication between controllers and UI controllables related to the
/// health state of a character to be displayed in the UI
/// </summary>
public class UpdateHealthUIPayload : MessageContentPayload
{
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Difference { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateHealthUIPayload"/> class.
    /// </summary>
    /// <param name="health">
    /// Current health
    /// </param>
    /// <param name="maxHealth">
    /// Max health
    /// </param>
    /// <param name="difference">
    /// Difference between the current and max health
    /// </param>
    public UpdateHealthUIPayload(int health, int maxHealth, int difference)
    {
        Health = health;
        MaxHealth = maxHealth;
        Difference = difference;
    }
}
