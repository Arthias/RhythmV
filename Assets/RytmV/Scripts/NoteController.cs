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
    MeshRenderer meshRenderer;
    [SerializeField] private Material noteMaterial;

    [SerializeField] public float timeToArrive = 2;

    [SerializeField] public float moveSpeed;

    [SerializeField] public GameObject targetObject;

    [SerializeField] public int colorBand=1;

    [SerializeField] public float frenselValue1 = 3;
    
    [SerializeField] public float frenselValue2 = 3;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        targetPosition = targetObject.transform.position;
        distance = Vector3.Distance(startPosition, targetPosition);
        StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPosition);
        if (shouldMove)
        {
            transform.position = LerpMove(startPosition, targetPosition, spawnTime, timeToArrive);
        }

        ColorChange();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Target")
        {
            ScoreController.scoreControllerInstance.NoteMiss();
            Destroy(this.gameObject);
        }
    }


    public void DestroyNote()
    {
        if (!AudioController.aControllerInstance.IsPaused())
        {
            ScoreController.scoreControllerInstance.NoteHit();
            Destroy(this.gameObject);
        }
    }

    void StartMoving()
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

    void ColorChange()
    {
        if (AudioController._audioBandBuffer.Length >= colorBand)
        {
            Vector3 colorVector = new Vector3(AudioController._audioBandBuffer[colorBand],frenselValue1,frenselValue2);
            noteMaterial.SetVector("_Color",colorVector);
        }
    }
}