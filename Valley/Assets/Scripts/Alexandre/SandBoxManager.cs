using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoxManager : MonoBehaviour
{
    public static SandBoxManager instance;
    public GameObject saveTree;

    private void Awake()
    {
        instance = this;
    }

    public void SaveTree(GameObject tree)
    {
        saveTree = tree;
    }

    public void CutTree()
    {
        saveTree.SetActive(false);           //Oui c'est con pour le moment 
    }
}