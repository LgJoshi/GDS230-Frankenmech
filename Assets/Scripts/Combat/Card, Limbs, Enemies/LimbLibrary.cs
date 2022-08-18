using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LimbLibrary : MonoBehaviour
{
    string jsonString;
    public LimbDataLibrary limbLibraryArray;

    [System.Serializable]
    public class LimbData {
        public string limbName = "default limbName";
        public string limbDescription = "default limbDescription";
        public int limbID = 999;
        public string limbEffect = "default effect";
        public int limbEffectIntOne = 0;
        public int limbEffectIntTwo = 0;
        public string limbType = "arm";
    }

    [System.Serializable]
    public class LimbDataLibrary{
        public List<LimbData> limbDataLibrary;
    }


    void Awake()
    {
        jsonString = File.ReadAllText("Assets/Scripts/JSON/" + "LimbDataLibrary.json" );
        Debug.Log("jsonstring is: " + jsonString);
        limbLibraryArray = JsonUtility.FromJson<LimbDataLibrary>(jsonString);
        //Debug.Log(cardLibraryArray.cardDataLibrary[1].cardName);
        //Debug.Log(cardLibraryArray.cardDataLibrary.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
