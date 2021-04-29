using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawner : MonoBehaviour
{
    public static VisitorSpawner instance;

    public Transform visitorSpawner;
    [SerializeField]
    private Transform visitorParent;
    [SerializeField]
    private List<GameObject> visitorPrefabs;

    [ContextMenu("Set visitor prefab")]
    void SetVisitorsPrefabs()
    {
        visitorPrefabs = new List<GameObject>();
        foreach (Transform tr in visitorParent)
        {
            visitorPrefabs.Add(tr.gameObject);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnNewVisitors(3);
    }

    public void SpawnNewVisitors(int numberToSpawn)
    {
        for(int i = 0; i < numberToSpawn; i++)
        {
            //visitorPrefabs[0].transform.position = visitorSpawner.position;
            visitorPrefabs[0].SetActive(true);
            visitorPrefabs.RemoveAt(0);
        }
    }
}
