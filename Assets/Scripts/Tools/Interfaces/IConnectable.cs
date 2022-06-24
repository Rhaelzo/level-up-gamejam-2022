using System;

public interface IConnectable
{
    Action<IConnectable> Connect { get; set; }
    Action<IConnectable> Disconnect { get; set; }
}
