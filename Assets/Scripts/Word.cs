using System;
using UnityEngine;

[Serializable]
public class Word
{
    [field: SerializeField, ReadOnly]
    public string Value { get; private set; }

    [field: SerializeField, ReadOnly]
    public Font Font { get; private set; }

    [field: SerializeField, ReadOnly]
    public int Points { get; private set; }

    [field: SerializeField, ReadOnly]
    public float Multiplier { get; private set; }

    public Word(string value, Font font, float multiplier)
    {
        Value = string.IsNullOrEmpty(value) ? "Default" : value;
        Font = font;
        Points = Value.Length;
        Multiplier = multiplier;
    }

    public bool MatchStart(string otherValue)
    {
        return Value.StartsWith(otherValue);
    }
}
