using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureInfo : MonoBehaviour
{
    public string name;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            SandBoxManager.instance.SaveTreasure(this.gameObject);
            UIManager.instance.ShowTreasureInput();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.instance.HideTreasureInput();
    }
}
