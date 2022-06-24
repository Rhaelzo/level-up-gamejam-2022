using System;

public interface IMessenger<T> where T : Enum
{ 
    Action<T, MessageContentPayload> SendCustomMessage { get; set; }
}
