using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Location")]
    //Location
    public GameObject locationGroup;
    private GameObject newLocationDiscovered;
    private Text locationName;
    private bool canCheckLocation = false;

    [Header("Treasure")]
    //Treasure
    public GameObject treasureGroup;
    private GameObject newTreasureObtained;
    private Text treasureName;

    //Tree
    public GameObject treeUI, removeBaliseUI, treasureUI;

    [HideInInspector]
    //Score
    public Text score;              //Obsolete



    private void Awake()
    {
        instance = this;

        //Location
        newLocationDiscovered = locationGroup.transform.GetChild(0).gameObject;
        locationName = locationGroup.transform.GetChild(1).GetComponent<Text>();

        //Treasure
        newTreasureObtained = treasureGroup.transform.GetChild(0).gameObject;
        treasureName = treasureGroup.transform.GetChild(1).GetComponent<Text>();
    }


    #region Location Point UI
    public void ShowLocation(string name)
    {
        locationName.text = name;
        locationGroup.SetActive(true);
        StartCoroutine(HideLocation());
    }

    IEnumerator HideLocation()
    {
        yield return new WaitForSeconds(1.5f);
        locationGroup.SetActive(false);
    }
    #endregion

    #region Treasure UI
    public void ShowTreasureInput()
    {
        treasureUI.SetActive(true);
    }

    public void HideTreasureInput()
    {
        treasureUI.SetActive(false);
    }

    public void ShowTreasureObtained(string name)
    {
        treasureName.text = name;
        treasureGroup.SetActive(true);
        treasureUI.SetActive(false);
        CabaneManager.instance.AddArgent(3);
        StartCoroutine(HideTreasure());
    }

    IEnumerator HideTreasure()
    {
        yield return new WaitForSeconds(2f);
        treasureGroup.SetActive(false);
    }
    #endregion

    #region Tree UI
    public void ShowCutTreeUI(Vector3 position)
    {
        //Tree location (Nan c'est nul)
        
        treeUI.SetActive(true);
    }

    public void HideCutTreeUI()
    {
        treeUI.SetActive(false);
    }
    #endregion

    #region Balise UI
    public void ShowRemoveBaliseUI(Vector3 position)
    {
        removeBaliseUI.SetActive(true);
    }

    public void HideRemoveBaliseUI()
    {
        removeBaliseUI.SetActive(false);
    }
    #endregion

    #region Scoring
    public void UpdateScore(int newScore)
    {
        score.text = newScore.ToString();
    }

    #endregion
}
