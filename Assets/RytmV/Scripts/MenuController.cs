using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject menuButton;
    public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        menuButton.SetActive(true);
        mainMenu.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuButton(){
        menuButton.SetActive(false);
        mainMenu.SetActive(true);
        Time.timeScale=0;
        AudioController.aControllerInstance.PauseMusic();


    }

    public void CancelButton(){
        menuButton.SetActive(true);
        mainMenu.SetActive(false);
        Time.timeScale=1;
        AudioController.aControllerInstance.PlayMusic();

    }

    public void OptionsButton(){

    }
    public void ResetButton(){

    }
    public void LvLSelectButton(){

    }


}
