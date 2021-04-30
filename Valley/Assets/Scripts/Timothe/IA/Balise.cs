using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balise : MonoBehaviour
{
    public GameObject mapPoint;

    public AudioSource audioSource;

    private void OnDrawGizmos()
    {
        Color newCol = Color.yellow;
        newCol.a = 0.2f;
        UnityEditor.Handles.color = newCol;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, 11);
    }
}
