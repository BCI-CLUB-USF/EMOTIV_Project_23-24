using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;

public class ArduinoCarController : MonoBehaviour
{
    private ScrollingGrid scrollingGrid;
    EmotivUnityItf _eItf = EmotivUnityItf.Instance;
    public bool headset = false;
    
    // Start is called before the first frame update
    void Start()
    {
        scrollingGrid = GetComponent<ScrollingGrid>();

        if (scrollingGrid == null)
        {
            Debug.LogError("ScrollingGrid component not found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        headsetParser();
        
        
        // Check for movement inputs and call corresponding methods on ScrollingGrid
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveBackward();
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        // Check for rotation inputs and call corresponding methods on ScrollingGrid
        if (Input.GetKey(KeyCode.Q))
        {
            TurnLeft();
        }
        if (Input.GetKey(KeyCode.E))
        {
            TurnRight();
        }
    }

    void MoveForward()
    {
        scrollingGrid.MoveForward();
    }

    void MoveBackward()
    {
        scrollingGrid.MoveBackward();
    }

    void MoveLeft()
    {
        scrollingGrid.MoveLeft();
    }

    void MoveRight()
    {
        scrollingGrid.MoveRight();
    }

    void TurnLeft()
    {
        scrollingGrid.TurnLeft();
    }

    void TurnRight()
    {
        scrollingGrid.TurnRight();
    }

    void headsetParser(){
        if(_eItf != null)
        {
            if(_eItf.LatestMentalCommand.act != "NULL")
            {
                string current_command = _eItf.LatestMentalCommand.act;
                switch (current_command){
                    case "push": 
                        MoveForward();
                        break;
                    case "pull": 
                        MoveBackward();
                        break;
                    case "left": 
                        MoveLeft();
                        break;
                    case "right": 
                        MoveRight();
                        break;
                    case "rotateRight":
                        TurnRight();
                        break;
                    case "rotateLeft":
                        TurnLeft();
                        break;
                }
            }
        }
    }
}
