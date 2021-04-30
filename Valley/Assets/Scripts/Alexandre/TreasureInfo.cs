using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureInfo : MonoBehaviour
{
    public string name;
    public int money;
    public Animator anim;

    public bool unlockAxe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            SandBoxManager.instance.SaveTreasure(this.gameObject);
            anim.SetBool("isNear", true);
            UIManager.instance.ShowTreasureInput();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isNear", false);
        UIManager.instance.HideTreasureInput();
    }
}
