using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Location
    public GameObject locationGroup;
    private GameObject newLocationDiscovered;
    private Text locationName;
    private bool canCheckLocation = false;

    //Tree
    public GameObject treeUI;

    private void Awake()
    {
        instance = this;
        newLocationDiscovered = locationGroup.transform.GetChild(0).gameObject;
        locationName = locationGroup.transform.GetChild(1).GetComponent<Text>();
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
}
