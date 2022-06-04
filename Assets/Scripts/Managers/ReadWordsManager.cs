using UnityEngine;

public class ReadWordsManager : MonoBehaviour 
{
    private void OnAwake() 
    {
          
    }

    public static void ReadString()
   {
       string path = Application.persistentDataPath + "/test.txt";
       //Read the text from directly from the test.txt file
       StreamReader reader = new StreamReader(path);
       Debug.Log(reader.ReadToEnd());
       reader.Close();
   }

}
