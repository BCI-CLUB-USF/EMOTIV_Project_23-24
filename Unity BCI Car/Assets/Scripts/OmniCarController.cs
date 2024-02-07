using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OmniCarController : MonoBehaviour
{
    public float speed = 20;
    public Vector3 _rotationSpeed = new Vector3 (0, 50, 0);
    public bool headset = false;

    // Update is called once per frame
    void FixedUpdate()
    {   if(!headset){

            Vector3 movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //transform.Translate(movementDirection * speed * Time.deltaTime);
            forwardBack(movementDirection);

            if(Input.GetKey("k")){
                transform.Rotate(_rotationSpeed * Time.deltaTime);
            }
            if(Input.GetKey("j")){
                transform.Rotate( -_rotationSpeed * Time.deltaTime);
            }

        }
        else{
            Vector3 dir = headsetParser();
            forwardBack(dir);
        }
    }

    void forwardBack(Vector3 input){
        transform.Translate(input * speed * Time.deltaTime);
    }

    Vector3 headsetParser(){
        return new Vector3(1,0,0);
    }
}
