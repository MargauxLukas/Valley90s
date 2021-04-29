using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject playerBody;
    public Animator playerAnimator;

    private void Awake()
    {
        instance = this;
    }

    public void PlayWalk()
    {
        playerAnimator.SetBool("isWalking", true);
    }

    public void StopWalk()
    {
        playerAnimator.SetBool("isWalking", false);
    }
}
