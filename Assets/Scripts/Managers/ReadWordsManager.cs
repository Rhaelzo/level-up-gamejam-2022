using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ReadWordsManager : MonoBehaviour 
{
    public static List<Word> AllInsults = new List<Word>();

    private void Awake()
    {
        ReadInsultsFromFile();
    }

    public static void ReadInsultsFromFile()
    {
        using (StreamReader sr = new StreamReader("Assets/Words/insults.txt"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] lineParts = line.Split(',');
                AllInsults.Add(new Word(lineParts[0], ))
            }
        }
    }

}
