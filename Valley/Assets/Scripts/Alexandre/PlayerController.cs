using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

    public float speed;
    private Vector3 moveDirection;

    public Transform body;

    private float x = 0f;    //Cross Directionnal Up and Down
    private float z = 0f;    //Cross directionnal Left and Right

    void FixedUpdate()
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
        moveDirection = Vector3.zero;

        if(Input.GetKey(KeyCode.Z)) { moveDirection += ( Vector3.forward * speed); }
        if(Input.GetKey(KeyCode.S)) { moveDirection += (-Vector3.forward * speed); }
        if(Input.GetKey(KeyCode.Q)) { moveDirection += (-Vector3.right   * speed); }
        if(Input.GetKey(KeyCode.D)) { moveDirection += ( Vector3.right   * speed); }

        if (moveDirection != Vector3.zero)
        {
            MoveCharacter(moveDirection);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E)) { SandBoxManager.instance.CutTree(); }

        if (Input.GetKeyDown(KeyCode.R)) { SandBoxManager.instance.PutBalise(transform.position + body.forward); }
    }

    private void MoveCharacter(Vector3 direction)
    {
        characterController.Move(direction);
        body.forward = direction;
    }
}
