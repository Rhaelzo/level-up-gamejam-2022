using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WordObjectsManager : MonoBehaviour 
{
    private static List<WordObject> AllWordObjects = new List<WordObject>();
    private static List<int> IndexesAlreadyVisited = new List<int>();

    [SerializeField] WordObject prefab;

    private void Awake()
    {
        ReadInsultsFromFile("Assets/Words/insults.txt");
        // AllInsults.ForEach(Debug.Log);
        SceneManager.LoadScene("InsultsTest");
    }

    private void ReadInsultsFromFile(string path)
    {
        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Debug.Log(line);
                if (string.IsNullOrEmpty(line)){
                    continue;
                }
                string[] lineParts = line.Split(',');

                WordObject wo = Instantiate<WordObject>(prefab);
                wo.Initialize(new Word(lineParts[0], lineParts[1] == "0" ? 1 : (float) 2.5 ));
                AllWordObjects.Add(wo);
            }
        }
    }

    public static List<WordObject> getRandom(int n){
        List<WordObject> toRet = new List<WordObject>();
        for (int i = 0; i < n; i++){
            int randIndex = Random.Range(0, AllWordObjects.Count);

            IndexesAlreadyVisited.Add(randIndex);
            toRet.Add(AllWordObjects[randIndex]);
        }
        return toRet;
    }

}