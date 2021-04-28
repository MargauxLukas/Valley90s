using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoxManager : MonoBehaviour
{
    public static SandBoxManager instance;
    public GameObject saveTree;
    public GameObject saveBalise;
    public GameObject saveTreasure;
    [SerializeField]
    private List<GameObject> balisePrefabs;

    private Transform visitorSpawner;

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

    public void SaveTreasure(GameObject treasure)
    {
        saveTreasure = treasure;
    }

    public void OpenChest()
    {
        saveTreasure.SetActive(false);
        UIManager.instance.ShowTreasureObtained(saveTreasure.GetComponent<TreasureInfo>().name);
    }

    public void SaveBalise(GameObject balise)
    {
        saveBalise = balise;
    }

    public void PutBalise(Vector3 position)
    {
        if (saveBalise == null)
        {
            Debug.Log(CabaneManager.instance.CanPutBalise());
            if (CabaneManager.instance.CanPutBalise())
            {
                foreach (GameObject g in balisePrefabs)
                {
                    if (!g.activeSelf)
                    {
                        g.SetActive(true);
                        g.transform.position = position;
                        CabaneManager.instance.UseBalise();
                        break;
                    }
                }
            }
        }
        else
        {
            CabaneManager.instance.AddBalise(1);
            saveBalise.SetActive(false);
            saveBalise = null;
        }
    }
}