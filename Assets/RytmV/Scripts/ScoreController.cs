using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController scoreControllerInstance;

    public int currentScore;

    public int scoreByNote = 100;
    public int multiplier = 1;
    public int[] multThresholds;
    public int hitCounter;



    public Text scoreText;
    public Text multiplierText;
    

    // Start is called before the first frame update
    void Start()
    {
        scoreControllerInstance = this;
        scoreText.text = "S: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteMiss()
    {
        hitCounter = 0;
        multiplier = 1;
        multiplierText.text = "M: x" + multiplier;
    }

    public void NoteHit()
    {

        if (multiplier - 1 < multThresholds.Length)
        {

            hitCounter++;
            if (multThresholds[multiplier - 1] <= hitCounter)
            {
                hitCounter = 0;
                multiplier++;
                multiplierText.text = "M: x" + multiplier;
            }

        }
        currentScore += scoreByNote * multiplier;
        scoreText.text = "S: " + currentScore;


    }


}
