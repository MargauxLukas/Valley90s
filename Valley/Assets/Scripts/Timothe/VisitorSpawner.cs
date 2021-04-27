using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawner : MonoBehaviour
{
    public static VisitorSpawner instance;

    public Transform visitorSpawner;
    [SerializeField]
    private List<GameObject> visitorPrefabs;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnNewVisitors()
    {
        for(int i = 0; i < 5; i++)
        {
            //visitorPrefabs[0].transform.position = visitorSpawner.position;
            visitorPrefabs[0].SetActive(true);
            visitorPrefabs.RemoveAt(0);
        }
    }
}
