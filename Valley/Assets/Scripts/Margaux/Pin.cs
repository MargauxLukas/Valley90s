using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private Transform FOVMain;
    [SerializeField] private Transform FOVSecondary;

    private float baseSprite = .3f;
    private float baseRange = .77f;


    private void OnDrawGizmos()
    {
        var ca = Color.green;
        UnityEditor.Handles.color = ca;
        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, PinManager.instance.viewRange);

        var cb = Color.red;
        UnityEditor.Handles.color = cb;
        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, PinManager.instance.blockingRange);
    }

    private void Start()
    {
        FOVUpdate();
    }

    public void FOVUpdate()
    {
        var scale = (PinManager.instance.viewRange * baseSprite) / baseRange;
        var sclVec = new Vector3(scale, scale, scale);

        FOVMain.localScale = sclVec;
        FOVSecondary.localScale = sclVec;
    }
}
