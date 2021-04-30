//using System;
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
    [SerializeField] private List<string> possibleRandomDialogue;
    [SerializeField] private List<string> possibleLandmarkDialogue;
    private string dialogue;
    private int chanceDialogue;                                       //Random
    private bool isTalking;

    private float timeBeforeDialogue, currentTimeBeforeDialogue;

    /*void Start()
    {
        StartCoroutine(TimeOnValley());
    }*/

    private void Update()
    {
        currentTimeBeforeDialogue += Time.deltaTime;
        if (currentTimeBeforeDialogue >= timeBeforeDialogue && !isTalking)
        {
            timeBeforeDialogue = UnityEngine.Random.Range(25f, 60f);
            currentTimeBeforeDialogue = 0;

            dialogue = possibleRandomDialogue[Random.Range(0, possibleRandomDialogue.Count)];
        }
    }

    /*IEnumerator TimeOnValley()
    {
        yield return new WaitForSeconds(stayTime);
        //Reset Memory
        ResetMemory();
    }*/

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Player") && nbLandmarksVisited > 0)
        {
            chanceDialogue = Random.Range(1,11);
            Debug.Log(chanceDialogue);
            if (chanceDialogue > 5)
            {
                ChooseDialogue();
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
    public void ChooseDialogue()
    {
        if (nbLandmarksVisited <= 0) { dialogue = possibleLandmarkDialogue[Random.Range(0, possibleLandmarkDialogue.Count)]; }
        else if (nbLandmarksVisited < 3) { dialogue = lastLandmarkVisited.dialoguePhrase; }
        else { dialogue = "Il y a tellement de choses à voir ici !"; }
    }

    public void Dialogue()
    {
        isTalking = true;

        dialogueBulle.gameObject.SetActive(true);
        dialogueBulle.text = dialogue;

        //CabaneManager.instance.AddArgent(1);                            //Add 1 balise

        StartCoroutine(EndDialogue());
    }

    IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(2f);
        isTalking = false;
        dialogueBulle.gameObject.SetActive(false);
    }
    #endregion
}
