using System;

public interface IMessageable<T> : IConnectable where T : Enum
{
    MessageCallbackData<T>[] CallbackDatas { get; }
}
