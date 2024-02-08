using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EmotivUnityPlugin;

public class ArrowShower : MonoBehaviour
{   
    EmotivUnityItf _eItf = EmotivUnityItf.Instance;
    public bool headset = false;
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
    {   if(!headset){
            //detects key input and makes arrow visable
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
        else headsetParser_Input();
        
        
    }

    void headsetParser_Input(){
        if(_eItf != null)
        {
            if(_eItf.LatestMentalCommand.act != "NULL")
            {   //need to use if else statements so when command switches to other command arrow clears
                string current_command = _eItf.LatestMentalCommand.act;
                if (current_command == "push")
                    upArrow.enabled = true;
                
                else upArrow.enabled = false;
            
                if(current_command == "pull")
                    downArrow.enabled = true;
                
                else downArrow.enabled = false;
                
                if(current_command == "left")
                    leftArrow.enabled = true;
                
                else leftArrow.enabled = false;
                
                if (current_command == "right")
                    rightArrow.enabled = true;
                
                else rightArrow.enabled = false;
                

                
            }
        }
    }
}
