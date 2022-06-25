/// <summary>
/// Content payload <see cref="MessageContentPayload"/> used in message
/// communication between controllers and controllables related to the
/// current turn state
/// </summary>
public class CurrentTurnPayload : MessageContentPayload
{
    public Turn Turn { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentTurnPayload"/> class.
    /// </summary>
    /// <param name="turn">
    /// Current turn state
    /// </param>
    public CurrentTurnPayload(Turn turn)
    {
        Turn = turn;
    }
}
