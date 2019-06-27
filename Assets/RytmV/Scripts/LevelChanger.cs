
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public static LevelChanger levelChangerInstance;
    public Animator animator;
    private int levelToLoad;


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

    public void FadeToNextLevel(){
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel(){
        FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
