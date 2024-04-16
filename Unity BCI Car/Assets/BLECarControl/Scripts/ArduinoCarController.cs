using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoCarController : MonoBehaviour
{
    private ScrollingGrid scrollingGrid;
    
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
    void Update()
    {
        // Check for movement inputs and call corresponding methods on ScrollingGrid
        if (Input.GetKey(KeyCode.W))
        {
            scrollingGrid.MoveForward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            scrollingGrid.MoveBackward();
        }
        if (Input.GetKey(KeyCode.A))
        {
            scrollingGrid.MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            scrollingGrid.MoveRight();
        }

        // Check for rotation inputs and call corresponding methods on ScrollingGrid
        if (Input.GetKey(KeyCode.Q))
        {
            scrollingGrid.TurnLeft();
        }
        if (Input.GetKey(KeyCode.E))
        {
            scrollingGrid.TurnRight();
        }
    }
}
