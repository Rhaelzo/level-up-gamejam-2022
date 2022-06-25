/// <summary>
/// Content payload <see cref="MessageContentPayload"/> used in message
/// communication between controllers and controllables related to the
/// health state of a character
/// </summary>
public class UpdateHealthPayload : MessageContentPayload
{
    public CharacterType TargetType { get; private set; }
    public int Value { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateHealthPayload"/> class.
    /// </summary>
    /// <param name="targetType">
    /// Target to get health updated
    /// </param>
    /// <param name="value">
    /// Value to update health with
    /// </param>
    public UpdateHealthPayload(CharacterType targetType, int value)
    {
        TargetType = targetType;
        Value = value;
    }
}
