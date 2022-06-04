using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ReadWordsManager : MonoBehaviour 
{
    public static List<Word> AllInsults = new List<Word>();

    private void Awake()
    {
        ReadInsultsFromFile();
        // AllInsults.ForEach(Debug.Log);
        SceneManager.LoadScene("InsultsTest");
    }

    private void ReadInsultsFromFile()
    {
        using (StreamReader sr = new StreamReader("Assets/Words/insults.txt"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                // Debug.Log(line);
                if (string.IsNullOrEmpty(line)){
                    continue;
                }
                string[] lineParts = line.Split(',');
                AllInsults.Add(new Word(lineParts[0], lineParts[1] == "0" ? 1 : (float) 2.5 ));
            }
        }
    }

}
