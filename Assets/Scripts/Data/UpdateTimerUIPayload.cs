/// <summary>
/// Content payload <see cref="MessageContentPayload"/> used in message
/// communication between controllers and UI controllables related to the
/// timer UI, such as current time and time per round
/// </summary>
public class UpdateTimerUIPayload : MessageContentPayload
{
    public float CurrentTime { get; private set; }
    public float TimePerRound { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTimerUIPayload"/> class.
    /// </summary>
    /// <param name="currentTime">
    /// Current time
    /// </param>
    /// <param name="timePerRound">
    /// Time per round
    /// </param>
    public UpdateTimerUIPayload(float currentTime, float timePerRound)
    {
        CurrentTime = currentTime;
        TimePerRound = timePerRound;
    }
}
