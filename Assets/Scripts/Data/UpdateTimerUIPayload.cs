public class UpdateTimerUIPayload : MessageContentPayload
{
    public float CurrentTime { get; private set; }
    public float TimePerRound { get; private set; }

    public UpdateTimerUIPayload(float currentTime, float timePerRound)
    {
        CurrentTime = currentTime;
        TimePerRound = timePerRound;
    }
}
