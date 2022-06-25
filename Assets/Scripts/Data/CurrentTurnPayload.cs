public class CurrentTurnPayload : MessageContentPayload
{
    public Turn Turn { get; private set; }

    public CurrentTurnPayload(Turn turn)
    {
        Turn = turn;
    }
}
