using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitorsFeedback : MonoBehaviour
{
    //Scripts Feedback Visitors (Dialogue + Memory Interest Point)
    public LocationInfo lastLandmarkVisited;                          //Last Interest Point                                      //Spawn Time
    public int nbLandmarksVisited = 0;

    private float stayTime = 300f;

    public TextMesh dialogueBulle;
    private string dialogue;
    private int chanceDialogue;                                       //Random

    void Start()
    {
        StartCoroutine(TimeOnValley());
    }

    IEnumerator TimeOnValley()
    {
        yield return new WaitForSeconds(stayTime);
        //Reset Memory
        ResetMemory();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player") && nbLandmarksVisited > 0)
        {
            chanceDialogue = UnityEngine.Random.Range(1,11);
            Debug.Log(chanceDialogue);
            if (chanceDialogue > 5)
            {
                Dialogue();
            }
        }
    }

    public void LandmarksPoint(LocationInfo landmark)
    {
        lastLandmarkVisited = landmark;
        nbLandmarksVisited++;
    }

    public void ResetMemory()
    {
        lastLandmarkVisited = null;
        nbLandmarksVisited = 0;
    }

    #region Dialogue
    public void Dialogue()
    {
        if (nbLandmarksVisited < 3) { dialogue = lastLandmarkVisited.dialoguePhrase; }
        else { dialogue = "Woah trop de truc bien dans cette putain de vallée"; }

        dialogueBulle.gameObject.SetActive(true);
        dialogueBulle.text = dialogue;

        //CabaneManager.instance.AddArgent(1);                            //Add 1 balise

        StartCoroutine(EndDialogue());
    }

    IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(2f);
        dialogueBulle.gameObject.SetActive(false);
    }
    #endregion
}
