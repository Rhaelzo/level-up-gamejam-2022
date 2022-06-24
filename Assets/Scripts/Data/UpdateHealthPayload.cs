public class UpdateHealthPayload : MessageContentPayload
{
    public CharacterType TargetType { get; private set; }
    public int Value { get; private set; }

    public UpdateHealthPayload(CharacterType targetType, int value)
    {
        TargetType = targetType;
        Value = value;
    }
}
