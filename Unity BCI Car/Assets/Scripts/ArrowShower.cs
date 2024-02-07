using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowShower : MonoBehaviour
{
    [SerializeField] RawImage leftArrow;
    [SerializeField] RawImage rightArrow;
    [SerializeField] RawImage upArrow;
    [SerializeField] RawImage downArrow;
    // Start is called before the first frame update
    void Start()
    {   //makes arrows disapear
        leftArrow.enabled = upArrow.enabled = downArrow.enabled = rightArrow.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   //detects key input and makes arrow visable
        if (Input.GetKey(KeyCode.UpArrow)){
            upArrow.enabled = true;
        }
        else{
            upArrow.enabled = false;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            downArrow.enabled = true;
        }
        else{
            downArrow.enabled = false;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            leftArrow.enabled = true;
        }
        else{
            leftArrow.enabled = false;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            rightArrow.enabled = true;
        }
        else{
            rightArrow.enabled = false;
        }
        
        
    }
}
