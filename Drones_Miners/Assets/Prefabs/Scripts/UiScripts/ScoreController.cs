using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private GameObject redScoreObject;
    private TextMeshProUGUI redText;

    [SerializeField] private GameObject blueScoreObject;
    private TextMeshProUGUI blueText;

    protected int redScore = 0;
    protected int blueScore = 0;

    private void Start()
    {
        redScoreObject.TryGetComponent(out redText);
        blueScoreObject.TryGetComponent(out blueText);
    }

    public void AddScore(Side side)
    {
        if(side == Side.Blue) { blueScore++; blueText.text = blueScore.ToString(); }
        if(side == Side.Red) { redScore++; redText.text = redScore.ToString();}
    }
}
