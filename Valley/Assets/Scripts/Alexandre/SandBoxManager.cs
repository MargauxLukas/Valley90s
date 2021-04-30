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
    private Transform baliseParent;
    [SerializeField]
    private List<GameObject> balisePrefabs;

    [SerializeField]
    private AudioSource audioSource;

    public GameObject UIAxe;

    [ContextMenu("Set balise prefab")]
    void SetBalisePrefabs()
    {
        balisePrefabs = new List<GameObject>();
        foreach(Transform tr in baliseParent)
        {
            balisePrefabs.Add(tr.gameObject);
        }
    }

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
        audioSource.transform.position = saveTree.transform.position;
        audioSource.Play();
        saveTree.transform.parent.gameObject.SetActive(false);           //Oui c'est con pour le moment 
    }

    public void SaveTreasure(GameObject treasure)
    {
        saveTreasure = treasure;
    }

    public void OpenChest(out bool canCutTree)
    {
        saveTreasure.SetActive(false);
        UIManager.instance.ShowTreasureObtained(saveTreasure.GetComponent<TreasureInfo>().name);
        CabaneManager.instance.AddArgent(saveTreasure.GetComponent<TreasureInfo>().money);
        if(saveTreasure.GetComponent<TreasureInfo>().unlockAxe)
        {
            UIAxe.SetActive(true);
            canCutTree = true;
        }
        else
        {
            canCutTree = false;
        }
    }

    public void SaveBalise(GameObject balise)
    {
        saveBalise = balise;
    }

    public void PutBalise(Vector3 position)
    {
        position = new Vector3(position.x, 2, position.z);
        if (saveBalise == null)
        {
            if (CabaneManager.instance.CanPutBalise())
            {
                foreach (GameObject g in balisePrefabs)
                {
                    if (!g.activeSelf)
                    {
                        g.SetActive(true);
                        g.transform.position = position;
                        CabaneManager.instance.AddArgent(-5);
                        g.GetComponent<Balise>().mapPoint = PinManager.instance.PutBalise(new Vector2(position.x, position.z));
                        g.GetComponent<Balise>().audioSource.Play();
                        break;
                    }
                }
            }
        }
        else
        {
            CabaneManager.instance.AddArgent(-3);
            saveBalise.SetActive(false);
            PinManager.instance.RemoveBalise(saveBalise.GetComponent<Balise>().mapPoint);
            saveBalise = null;
        }
    }
}