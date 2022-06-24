public class EnemyOnWordFinishedPayload : MessageContentPayload
{
    public Turn Turn { get; private set; }

    public EnemyOnWordFinishedPayload(Turn turn)
    {
        Turn = turn;
    }
}
