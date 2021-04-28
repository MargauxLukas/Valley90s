using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFloatingText : MonoBehaviour
{
    private CabaneManager cabManager;


    void Start()
    {
        cabManager = CabaneManager.instance;
    }

    public void Action()
    {
        cabManager.floatingText.SetActive(false);
    }
}
