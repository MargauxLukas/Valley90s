using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

    public float speed;

    private float x = 0f;    //Cross Directionnal Up and Down
    private float z = 0f;    //Cross directionnal Left and Right

    void Update()
    {
        /**************************
         *      Pad Controller    *
         **************************/
        /*
        x = Input.GetAxis("D-Pad (Horizontal)");          //Pad XBox
        z = Input.GetAxis("D-Pad (Vertical)");            //Pad XBox

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
        */

        /**************************
        *         KeyBoard        *
        **************************/
        if(Input.GetKey(KeyCode.Z)){ characterController.Move( transform.forward * speed); }
        if(Input.GetKey(KeyCode.S)){ characterController.Move(-transform.forward * speed); }
        if(Input.GetKey(KeyCode.Q)){ characterController.Move(-transform.right   * speed); }
        if(Input.GetKey(KeyCode.D)){ characterController.Move( transform.right   * speed); }
    }
}
