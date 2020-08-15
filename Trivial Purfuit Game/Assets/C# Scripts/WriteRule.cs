using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class WriteRule : MonoBehaviour
{

    private static readonly string ResourcePath = "TextFiles/gameRule";
    private TextAsset gamerule;


    // Start is called before the first frame update
    void Start()
    {
        
        postRule();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void postRule()
    {
        gamerule = Resources.Load(ResourcePath, typeof(TextAsset)) as TextAsset;
        //string path = "Assets/Resources/TextFiles/Trivial Purfuit Game Rules.docx";
        
        //fgCSVReader.LoadFromTextAsset(questionFile, new fgCSVReader.ReadLineDelegate(ReadLineIntoQuestion));

        //StreamReader reader = new StreamReader(path);
        var guiTextObject = this.gameObject.GetComponent<Text>();
        
        guiTextObject.text = $"{gamerule.ToString()}";
        
        //reader.Close();
    }

    

        
    
}
