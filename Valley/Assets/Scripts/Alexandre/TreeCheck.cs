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
    //private bool canCut;
    GameObject currentTargetedTree;

    private void FixedUpdate()
    {
        isNearTree = Physics.CheckSphere(collisionObjectCheck.position, treeDistance, treeMask);
        collisions = Physics.OverlapSphere(collisionObjectCheck.position, treeDistance, treeMask);

        if (isNearTree)
        {
            if(currentTargetedTree != GetClosestTree())
            {
                currentTargetedTree = GetClosestTree();
                SandBoxManager.instance.SaveTree(currentTargetedTree);
                UIManager.instance.ShowCutTreeUI(currentTargetedTree.transform.position);
            }
        }
        else
        {
            UIManager.instance.HideCutTreeUI();
            currentTargetedTree = null;
        }
    }

    private GameObject GetClosestTree()
    {
        float distance = -1;
        GameObject treeToReturn = null;

        foreach(Collider col in collisions)
        {
            if(distance < 0 || distance > Vector3.Distance(col.transform.position, transform.position))
            {
                treeToReturn = col.gameObject;
                distance = Vector3.Distance(col.transform.position, transform.position);
            }
        }

        return treeToReturn;
    }
}
