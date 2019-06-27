using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{

    public GameObject menuButton;
    public GameObject mainMenu;
    public GameObject scoreMenu;
    public GameObject levelSelect;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI currentTop;
    public TextMeshProUGUI highScoreMenuText;
    bool scorescreenActive;
    int score = 0;
    int highscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        scorescreenActive=false;
        menuButton.SetActive(true);
        mainMenu.SetActive(false);
        scoreMenu.SetActive(false);
        levelSelect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        score = ScoreController.scoreControllerInstance.CurrentTotalScore();
        if (AudioController.aControllerInstance.MusicEnded() && !scorescreenActive)
        {
            ShowScoreScreen();
        }
    }

    public void ShowScoreScreen()
    {
        scorescreenActive = true;
        highscore = ScoreController.scoreControllerInstance.CalculateHighScore();
        highScoreMenuText.text = ScoreMenuBuilder();
        menuButton.SetActive(false);
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        scoreMenu.SetActive(true);
    }

    public void MenuButton()
    {
        currentScore.text = "Current Score: " + score;
        currentTop.text = TopScoreBuilder();
        menuButton.SetActive(false);
        mainMenu.SetActive(true);
        Time.timeScale = 0;
        AudioController.aControllerInstance.PauseMusic();
    }

    public void CancelButton()
    {
        menuButton.SetActive(true);
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        Time.timeScale = 1;
        AudioController.aControllerInstance.PlayMusic();

    }

    public void OptionsButton()
    {

    }
    public void ResetButton()
    {
        Time.timeScale = 1;
        LevelChanger.levelChangerInstance.RestartLevel();
    }
    public void LvLSelectButton()
    {
        
        menuButton.SetActive(false);
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        scoreMenu.SetActive(false);

    }

    public string TopScoreBuilder()
    {
        return "Top Scores:\n" +
        "1 - " + ScoreController.scoreControllerInstance.hScore1 + "\n" +
        "2 - " + ScoreController.scoreControllerInstance.hScore2 + "\n" +
        "3 - " + ScoreController.scoreControllerInstance.hScore3 + "\n" +
        "4 - " + ScoreController.scoreControllerInstance.hScore4 + "\n" +
        "5 - " + ScoreController.scoreControllerInstance.hScore5 + "\n";
    }

    public string ScoreMenuBuilder()
    {
        string st = "";
        if (highscore > 0)
        {
            st += "<b>New High Score!</b>\n";
        }
        st += "Your Score: " + score + "\n" +
        "<align=\"left\">Current " + TopScoreBuilder();
        return st;
    }

    public void SelectLevel(string level){
        Time.timeScale = 1;
        LevelChanger.levelChangerInstance.FadeToLevel(level);
    }




}
