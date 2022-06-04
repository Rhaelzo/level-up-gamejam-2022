using System;
using UnityEngine;

[Serializable]
public class Word
{
    [field: SerializeField, ReadOnly]
    public string Value { get; private set; }

    [field: SerializeField, ReadOnly]
    public Font Font { get; private set; } = Resources.GetBuiltinResource<Font>("Arial.ttf");

    [field: SerializeField, ReadOnly]
    public int Points { get; private set; }

    [field: SerializeField, ReadOnly]
    public float Multiplier { get; private set; }

    public Word(string value, float multiplier)
    {
        Value = string.IsNullOrEmpty(value) ? "Default" : value;
        Points = Value.Length;
        Multiplier = multiplier;
    }

    public bool MatchStart(string otherValue)
    {
        return Value.StartsWith(otherValue);
    }

    public override string ToString()
    {
        return "Value: " + Value + " Font: " + Font.ToString() + " Points: " + Points + " Multiplier: " + Multiplier;
    }
}
