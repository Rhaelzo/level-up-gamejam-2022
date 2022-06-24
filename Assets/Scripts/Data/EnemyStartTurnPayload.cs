public class EnemyStartTurnPayload : MessageContentPayload
{
    public Turn Turn { get; private set; }

    public EnemyStartTurnPayload(Turn turn)
    {
        Turn = turn;
    }
}
