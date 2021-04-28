using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInfo : MonoBehaviour
{
    public string name;
    public bool discovered = false;                 //Pour le joueur

    public string dialoguePhrase;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<VisitorsFeedback>())
        {
            other.gameObject.GetComponent<VisitorsFeedback>().LandmarksPoint(this.GetComponent<LocationInfo>());
        }
    }
}
