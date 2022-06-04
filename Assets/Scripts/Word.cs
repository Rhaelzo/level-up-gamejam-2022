using UnityEngine;

namespace WordNS{

public class Word {
    private string word;
    private Font font;
    private int points;

    public Word(string word, Font font)
    {
        this.word = word;
        this.font = font;
        this.points = word.Length;
    }
}

}

