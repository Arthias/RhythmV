using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{

    Vector3 startPosition;
    Vector3 targetPosition;
    float distance;
    float spawnTime;

    bool shouldMove = false;


    [SerializeField]
    public float timeToArrive = 2;

    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    public GameObject targetObject;
    


    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        targetPosition = targetObject.transform.position;
        distance = Vector3.Distance(startPosition, targetPosition);
        startMoving();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPosition);  
        if (shouldMove)
        {
            transform.position = LerpMove(startPosition, targetPosition, spawnTime, timeToArrive);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        ScoreController.scoreControllerInstance.NoteMiss();
        
    }


    public void DestroyNote()
    {
        ScoreController.scoreControllerInstance.NoteHit();
        Destroy(this.gameObject);
    }

    void startMoving()
    {
        spawnTime = Time.time;
        shouldMove = true;
    }


    public Vector3 LerpMove(Vector3 start, Vector3 end, float spawnTime, float timeToArrive = 1)
    {
        float timeSinceSpawn = Time.time - spawnTime;
        float percentageComplete = timeSinceSpawn / timeToArrive;

        var result = Vector3.Lerp(start, end, percentageComplete);
        return result;
    }



}
