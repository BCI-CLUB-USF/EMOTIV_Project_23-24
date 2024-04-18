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
    private int direction = 0;
    
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
        if (Input.GetKey(KeyCode.Escape))
        {
            uiToggle = false;
        }

        emergencyBreak = Input.GetKey(KeyCode.Space);
        if (emergencyBreak)
        {
            bluetooth.WriteToBleDevice("0");
            return;
        }
        
        headsetParser();
        
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
            Debug.Log("EMERGENCY BREAK ACTIVATED!");
            return;
        }

        bluetooth.WriteToBleDevice(signal);
    }

    void MoveForward()
    {
        if (emergencyBreak)
        {
            bluetooth.WriteToBleDevice("0");
            return;
        }

        direction = 1;

        scrollingGrid.MoveForward();
        SendBleSignal("F");
    }

    void MoveBackward()
    {
        if (emergencyBreak)
        {
            bluetooth.WriteToBleDevice("0");
            return;
        }

        direction = -1;

        scrollingGrid.MoveBackward();
        SendBleSignal("B");
    }

    void MoveLeft()
    {
        if (emergencyBreak)
        {
            bluetooth.WriteToBleDevice("0");
            return;
        }
        
        scrollingGrid.MoveLeft();
        SendBleSignal("L");
    }

    void MoveRight()
    {
        if (emergencyBreak)
        {
            bluetooth.WriteToBleDevice("0");
            return;
        }
        
        scrollingGrid.MoveRight();
        SendBleSignal("R");
    }

    void TurnLeft()
    {
        if (emergencyBreak)
        {
            bluetooth.WriteToBleDevice("0");
            return;
        }

        if (direction == 1) {
            scrollingGrid.MoveForward();
            scrollingGrid.TurnLeft();
        } else if (direction == -1) {
            scrollingGrid.MoveBackward();
            scrollingGrid.TurnRight();
        }
        
        SendBleSignal("<");
    }

    void TurnRight()
    {
        if (emergencyBreak)
        {
            bluetooth.WriteToBleDevice("0");
            return;
        }
        
        if (direction == 1) {
            scrollingGrid.MoveForward();
            scrollingGrid.TurnRight();
        } else if (direction == -1) {
            scrollingGrid.MoveBackward();
            scrollingGrid.TurnLeft();
        }

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
