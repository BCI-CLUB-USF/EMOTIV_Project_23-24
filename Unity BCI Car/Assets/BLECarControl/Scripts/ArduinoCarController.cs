using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;

public class ArduinoCarController : MonoBehaviour
{
    public GameObject GridObj;
    private ScrollingGrid scrollingGrid;
    public GameObject BLE;
    private BLEHandler bluetooth;
    EmotivUnityItf _eItf = EmotivUnityItf.Instance;
    public bool headset = false;
    private bool emergencyBreak = false;
    private bool uiToggle = true;
    
    // Start is called before the first frame update
    void Start()
    {
        scrollingGrid = GridObj.GetComponent<ScrollingGrid>();
        bluetooth = BLE.GetComponent<BLEHandler>();

        if (scrollingGrid == null)
        {
            Debug.LogError("ScrollingGrid component not found on " + GridObj.name);
        }

        if (bluetooth == null)
        {
            Debug.LogError("BLE component not found on " + BLE.name);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(headset)
        {
            headsetParser();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            uiToggle = false;
        }

        emergencyBreak = Input.GetKey(KeyCode.Space);
        if (emergencyBreak)
        {
            SendBleSignal("0");
            Debug.Log("EMERGENCY BREAK ACTIVATED!");
        }
        
        // Check for movement inputs and call corresponding methods on ScrollingGrid
        if (Input.GetKey(KeyCode.W) && !uiToggle)
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.S) && !uiToggle)
        {
            MoveBackward();
        }
        if (Input.GetKey(KeyCode.A) && !uiToggle)
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D) && !uiToggle)
        {
            MoveRight();
        }

        // Check for rotation inputs and call corresponding methods on ScrollingGrid
        if (Input.GetKey(KeyCode.Q)  && !uiToggle)
        {
            TurnLeft();
        }
        if (Input.GetKey(KeyCode.E) && !uiToggle)
        {
            TurnRight();
        }
    }

    void SendBleSignal(string signal)
    {
        if (bluetooth.isConnected == false)
        {
            Debug.LogError("BLE is not connected yet.");
            return;
        }

        if (emergencyBreak)
        {
            bluetooth.WriteToBleDevice("0");
            return;
        }

        bluetooth.WriteToBleDevice(signal);
    }

    void MoveForward()
    {
        if (emergencyBreak)
        {
            return;
        }

        scrollingGrid.MoveForward();
        SendBleSignal("F");
    }

    void MoveBackward()
    {
        if (emergencyBreak)
        {
            return;
        }

        scrollingGrid.MoveBackward();
        SendBleSignal("B");
    }

    void MoveLeft()
    {
        if (emergencyBreak)
        {
            return;
        }
        
        scrollingGrid.MoveLeft();
        SendBleSignal("L");
    }

    void MoveRight()
    {
        if (emergencyBreak)
        {
            return;
        }
        
        scrollingGrid.MoveRight();
        SendBleSignal("R");
    }

    void TurnLeft()
    {
        if (emergencyBreak)
        {
            return;
        }
        
        scrollingGrid.TurnLeft();
        SendBleSignal("<");
    }

    void TurnRight()
    {
        if (emergencyBreak)
        {
            return;
        }
        
        scrollingGrid.TurnRight();
        SendBleSignal(">");
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
