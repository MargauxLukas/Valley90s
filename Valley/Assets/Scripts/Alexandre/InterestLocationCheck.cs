using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestLocationCheck : MonoBehaviour
{
    [System.NonSerialized] public bool isNearInterestPoint = false;

    public LayerMask interestPointMask;
    public Transform collisionObjectCheck;
    public float interestPointDistance = 0.5f;

    public Collider[] collision;

    private void FixedUpdate()
    {
        isNearInterestPoint = Physics.CheckSphere(collisionObjectCheck.position, interestPointDistance, interestPointMask);

        if (isNearInterestPoint)
        {
            
            UIManager.instance.ShowLocation();
        }
        else
        {
            
        }
    }
}
