using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabaneManager : MonoBehaviour
{
    public static CabaneManager instance;

    [SerializeField]
    private int baliseInStock = 5;

    private void Awake()
    {
        instance = this;
    }

    public void AddBalise(int nbToAdd)
    {
        Debug.Log("Balise en plus");
        baliseInStock++;
    }

    public bool CanPutBalise()
    {
        return baliseInStock > 0;
    }

    public void UseBalise()
    {
        Debug.Log("Balise en moins");
        baliseInStock--;
    }
}
