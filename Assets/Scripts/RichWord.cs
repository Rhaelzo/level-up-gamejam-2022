using System;
using WordNS;
using UnityEngine;

public class RichWord : Word{
    private int multiplier;

    public RichWord(string word, Font font, int multiplier) : base(word, font)
    {
        this.multiplier = multiplier;
    }

    public int getMultiplier(){
        return multiplier;
    }
}