using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectMenu : MonoBehaviour
{
    int _isOpen = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            gameObject.GetComponent<CanvasScaler>().scaleFactor = _isOpen;
            _isOpen = ++_isOpen % 2;
        }
    }
}
