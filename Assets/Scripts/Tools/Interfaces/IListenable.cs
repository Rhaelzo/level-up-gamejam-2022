public interface IListenable
{
    void RegisterListener(ListenerCallbackData data);
    void UnregisterListener(ListenerCallbackData data);
    void Event_Raise(object eventData = null);
}
