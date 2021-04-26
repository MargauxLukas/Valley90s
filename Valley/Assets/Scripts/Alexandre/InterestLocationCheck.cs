using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestLocationCheck : MonoBehaviour
{
    [System.NonSerialized] public bool isNearInterestPoint = false;

    public LayerMask interestPointMask;
    public Transform collisionObjectCheck;
    public float interestPointDistance = 0.5f;

    public Collider[] collisions;
    private LocationInfo locationInfo;

    private void FixedUpdate()
    {
        isNearInterestPoint = Physics.CheckSphere(collisionObjectCheck.position, interestPointDistance, interestPointMask);
        collisions = Physics.OverlapSphere(collisionObjectCheck.position, interestPointDistance, interestPointMask);

        
        if (isNearInterestPoint)
        {
            locationInfo = collisions[0].gameObject.GetComponent<LocationInfo>();

            
            if (!locationInfo.discovered)
            {
                locationInfo.discovered = true;
                UIManager.instance.ShowLocation(locationInfo.name);
            }
        }
        else
        {
            
        }
    }
}
