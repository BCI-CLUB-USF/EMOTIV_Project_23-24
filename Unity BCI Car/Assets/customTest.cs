using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;

public class customTest : MonoBehaviour
{

    EmotivUnityItf _eItf = EmotivUnityItf.Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_eItf != null)
        {
            if(_eItf.LatestMentalCommand.act != "NULL")
            {
                Debug.Log("CUSTOM :: MENTAL COMMAND :" + _eItf.LatestMentalCommand.act);
            }
        }
    }
}
