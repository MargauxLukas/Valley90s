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

    [SerializeField]
    private bool canCutTree;

    [SerializeField]
    private AudioSource audioSource;

    private float timeBeforeStepSound;

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
            timeBeforeStepSound -= Time.fixedDeltaTime;
            PlayerManager.instance.PlayWalk();
            MoveCharacter(moveDirection);
        }
        else { PlayerManager.instance.StopWalk(); }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetButtonDown("A Button")) 
        {
            Debug.Log(canCutTree);
            if (UIManager.instance.treeUI.activeSelf && canCutTree) { SandBoxManager.instance.CutTree(); }
            if (UIManager.instance.treasureUI.activeSelf) { canCutTree = SandBoxManager.instance.OpenChest(canCutTree); }
        }

        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetButtonDown("X Button")) {SandBoxManager.instance.PutBalise(transform.position); }

        if (Input.GetKeyDown(KeyCode.Keypad3) ||Input.GetButtonDown("Y Button")) { PinManager.instance.OpenMap(); }
    }

    private void MoveCharacter(Vector3 direction)
    {
        if(timeBeforeStepSound < 0)
        {
            timeBeforeStepSound = 5*speed;
            Debug.Log(timeBeforeStepSound);
            audioSource.Play();
        }
        characterController.Move(direction);
        body.forward = direction;
    }
}
