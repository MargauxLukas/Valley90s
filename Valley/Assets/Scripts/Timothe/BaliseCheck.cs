using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaliseCheck : MonoBehaviour
{
    [System.NonSerialized] public bool isNearBalise = false;

    public LayerMask balideMask;
    public Transform collisionObjectCheck;
    public float checkDistance = 0.5f;

    public Collider[] collisions;

    private void FixedUpdate()
    {
        isNearBalise = Physics.CheckSphere(collisionObjectCheck.position, checkDistance, balideMask);
        collisions = Physics.OverlapSphere(collisionObjectCheck.position, checkDistance, balideMask);

        if (isNearBalise)
        {
            SandBoxManager.instance.SaveBalise(collisions[0].gameObject);
            UIManager.instance.ShowRemoveBaliseUI(collisions[0].transform.position);
        }
        else
        {
            SandBoxManager.instance.SaveBalise(null);
            UIManager.instance.HideRemoveBaliseUI();
        }
    }
}
