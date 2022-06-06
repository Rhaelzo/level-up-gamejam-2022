using System;
using TMPro;
using UnityEngine;

[Serializable]
public class Word
{
    [field: SerializeField]
    public string Value { get; private set; }

    [field: SerializeField]
    public TMP_FontAsset Font { get; private set; }

    [field: SerializeField]
    public int Points { get; private set; }

    [field: SerializeField]
    public float Multiplier { get; private set; }

    public Word(string value, float multiplier, TMP_FontAsset font)
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

    public override string ToString()
    {
        return "Value: " + Value + " Font: " + Font.ToString() + " Points: " + Points + " Multiplier: " + Multiplier;
    }

    public bool IsMatch(string otherValue)
    {
        return Value.ToLower() == otherValue.ToLower();
    }

    public string Substring(string otherValue)
    {
        return Value.Substring(otherValue.Length);
    }
}
