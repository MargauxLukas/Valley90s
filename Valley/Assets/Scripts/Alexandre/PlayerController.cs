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

        if(Input.GetAxisRaw("D-Pad (Horizontal)") != 0){ moveDirection += ( Vector3.right   * Input.GetAxisRaw("D-Pad (Horizontal)") * speed); }
        if(Input.GetAxisRaw("D-Pad (Vertical)") != 0)  { moveDirection += (-Vector3.forward * Input.GetAxisRaw("D-Pad (Vertical)") * speed); }

        if (moveDirection != Vector3.zero)
        {
            MoveCharacter(moveDirection);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButton("A Button")) 
        {
            if (UIManager.instance.treeUI.activeSelf) { SandBoxManager.instance.CutTree(); }
            if (UIManager.instance.treasureUI.activeSelf) { SandBoxManager.instance.OpenChest(); }
        }

        if (Input.GetKeyDown(KeyCode.R)) { SandBoxManager.instance.PutBalise(transform.position + body.forward); }

        if (Input.GetKeyDown(KeyCode.M)) { PinManager.instance.OpenMap(); }
    }

    private void MoveCharacter(Vector3 direction)
    {
        characterController.Move(direction);
        body.forward = direction;
    }
}
