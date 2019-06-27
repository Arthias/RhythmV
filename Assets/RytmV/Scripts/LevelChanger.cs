
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public static LevelChanger levelChangerInstance;
    public Animator animator;
    private int levelToLoad = -1;
    private string levelName;


    void Start()
    {
        levelChangerInstance=this;        
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad=levelIndex;
        animator.SetTrigger("FadeOut");
    }
        public void FadeToLevel(string lvlName)
    {
        levelName=lvlName;
        animator.SetTrigger("FadeOut");
    }

    public void FadeToNextLevel(){
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel(){
        FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnFadeComplete()
    {
        if(levelToLoad > -1){
            SceneManager.LoadScene(levelToLoad);
        }else {
            SceneManager.LoadScene(levelName);
        }
        
    }
}
