using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    public static ScoreController ScoreController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteMiss(){
        Debug.Log("note hit!");

    }

    public void NoteHit(){

        Debug.Log("note Miss");

    }


}
