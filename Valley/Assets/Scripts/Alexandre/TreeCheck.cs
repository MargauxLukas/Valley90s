using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCheck : MonoBehaviour
{
    [System.NonSerialized] public bool isNearTree = false;

    public LayerMask treeMask;
    public Transform collisionObjectCheck;
    public float treeDistance = 0.5f;

    public Collider[] collisions;
    private bool canCut;

    private void FixedUpdate()
    {
        isNearTree = Physics.CheckSphere(collisionObjectCheck.position, treeDistance, treeMask);
        collisions = Physics.OverlapSphere(collisionObjectCheck.position, treeDistance, treeMask);

        if (isNearTree)
        {
            if (!canCut)
            {
                SandBoxManager.instance.SaveTree(collisions[0].gameObject);
                UIManager.instance.ShowCutTreeUI(collisions[0].transform.position);
                canCut = true;
            }
        }
        else
        {
            UIManager.instance.HideCutTreeUI();
            canCut = false;
        }
    }
}
