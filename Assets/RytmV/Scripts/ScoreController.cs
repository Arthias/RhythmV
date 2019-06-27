using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static ScoreController scoreControllerInstance;

    public int currentScore = 0;

    public int scoreByNote = 100;
    public int multiplier = 1;
    public int[] multThresholds;
    public int hitCounter;
    public int hScore1;
    public int hScore2;
    public int hScore3;
    public int hScore4;
    public int hScore5;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;


    // Start is called before the first frame update
    void Start()
    {
        scoreControllerInstance = this;
        scoreText.text = "S: 0";
        LoadHighScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int CurrentTotalScore()
    {
        return currentScore;
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

    public void LoadHighScore()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        hScore1 = PlayerPrefs.GetInt(i + "hScore1", 0);
        hScore2 = PlayerPrefs.GetInt(i + "hScore2", 0);
        hScore3 = PlayerPrefs.GetInt(i + "hScore3", 0);
        hScore4 = PlayerPrefs.GetInt(i + "hScore4", 0);
        hScore5 = PlayerPrefs.GetInt(i + "hScore5", 0);
    }
    public void LoadHighScore(int lvl)
    {
        hScore1 = PlayerPrefs.GetInt(lvl + "hScore1", 0);
        hScore2 = PlayerPrefs.GetInt(lvl + "hScore2", 0);
        hScore3 = PlayerPrefs.GetInt(lvl + "hScore3", 0);
        hScore4 = PlayerPrefs.GetInt(lvl + "hScore4", 0);
        hScore5 = PlayerPrefs.GetInt(lvl + "hScore5", 0);
    }

    public void SaveHighScore()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(i + "hScore1", hScore1);
        PlayerPrefs.SetInt(i + "hScore2", hScore2);
        PlayerPrefs.SetInt(i + "hScore3", hScore3);
        PlayerPrefs.SetInt(i + "hScore4", hScore4);
        PlayerPrefs.SetInt(i + "hScore5", hScore5);

    }

    public int CalculateHighScore()
    {
        int a = hScore1;
        int b = hScore2;
        int c = hScore3;
        int d = hScore4;
        int e = hScore5;
        int i = 0;
        if (currentScore > hScore5)
        {
            if (currentScore > hScore4)
            {
                if (currentScore > hScore3)
                {
                    if (currentScore > hScore2)
                    {
                        if (currentScore > hScore1)
                        {
                            hScore5 = d;
                            hScore4 = c;
                            hScore3 = b;
                            hScore2 = a;
                            hScore1 = currentScore;
                            i = 1;
                        }
                        else
                        {
                            hScore5 = d;
                            hScore4 = c;
                            hScore3 = b;
                            hScore2 = currentScore;
                            i = 2;
                        }
                    }
                    else
                    {
                        hScore5 = d;
                        hScore4 = c;
                        hScore3 = currentScore;
                        i = 3;
                    }
                }
                else
                {
                    hScore5 = d;
                    hScore4 = currentScore;
                    i = 4;
                }
            }
            else
            {
                hScore5 = currentScore;
                i = 5;
            }
        }
        SaveHighScore();
        return i;
    }
}
