using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Pedro");
        Debug.Log(WordObjectsManager.getRandom(1)[0].Word.Value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
