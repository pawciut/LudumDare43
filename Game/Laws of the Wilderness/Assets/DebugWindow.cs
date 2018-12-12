using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class DebugWindow : MonoBehaviour
{

    public Text DebugWindowOutput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Log(string message)
    {
        DebugWindowOutput.text = message;
    }
}
