using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class WordObjectsManager : MonoBehaviour 
{
    private static List<WordObject> s_allWordObjects;
    private static List<int> s_indexesInUse;

    [SerializeField] WordObject prefab;

    private void Awake()
    {
        s_allWordObjects = new List<WordObject>();
        s_indexesInUse = new List<int>();
        ReadInsultsFromFile("Assets/Words/insults.txt", prefab);
    }

    private static void ReadInsultsFromFile(string path, WordObject wordObjectPrefab)
    {
        using (StreamReader sr = new StreamReader(path))
        {
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == string.Empty)
                {
                    continue;
                }
                string[] lineParts = line.Split(',');
                WordObject wordObject = Instantiate<WordObject>(wordObjectPrefab);
                wordObject.Initialize(new Word(lineParts[0], lineParts[1] == "0" ? 1f : 1.5f));
                wordObject.gameObject.SetActive(false);
                s_allWordObjects.Add(wordObject);
            }
        }
    }

    public static List<WordObject> GetRandom(int numberOfObjects){
        List<WordObject> result = new List<WordObject>();
        for (int i = 0; i < numberOfObjects; i++)
        {
            result.Add(GetRandom());
        }
        return result;
    }

    public static WordObject GetRandom()
    {
        int randIndex = Random.Range(0, s_allWordObjects.Count);
        while (CheckInUse(randIndex))
        {
            randIndex = Random.Range(0, s_allWordObjects.Count);
        }
        s_indexesInUse.Add(randIndex);
        return s_allWordObjects[randIndex];
    }

    public static void ReturnToPool(WordObject wordObject)
    {
        int index = s_allWordObjects.IndexOf(wordObject);
        wordObject.ResetObject();
        wordObject.gameObject.SetActive(false);
        s_indexesInUse.Remove(index);
    }

    private static bool CheckInUse(int index)
    {
        return s_indexesInUse.Contains(index);
    }
}