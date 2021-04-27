using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringManager : MonoBehaviour
{
    public static ScoringManager instance;

    public int scoreNumber;                //Score actuel

    private void Awake()
    {
        instance = this;
    }

    public void GainScore(int addScore)
    {
        //Gain lorsque point d'interet atteint (Score différent selon le point d'interet atteint)
        scoreNumber += addScore;
        UIManager.instance.UpdateScore(scoreNumber);
    }
}
