using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] public GameObject noteSpawnerPrefab;
    GameObject[] noteSpawner = new GameObject[8];

    [SerializeField] public GameObject notePrefab;
    public GameObject noteTarget;
    public int scaleBand = 1;
    public float startScale, scaleMult;

    //Beats per minute for track
    [SerializeField] public float beatsPM = 1f;

    [SerializeField] public float spawnerDistance = 20f;

    //Frequency of note spawn in fraction of seconds
    float bFrequency = 1f;

    //Wait time before track start in seconds
    public float waitTime;

    public string partitureString = "5,1,2,3,4,5,6,7,8";

    List<int> partitureList = new List<int>();
    //int[] partitureArray = new int[0];


    int posCounter = 0;


    //


    // Start is called before the first frame update
    void Start()
    {
        if (beatsPM > 0)
        {
            bFrequency = (60f / beatsPM);
        }

        SpawnerInstantiator();
        partitureListBuilder();
        setWaitTime();
        InvokeRepeating("SpawnNote", waitTime, bFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        if (scaleBand >= AudioController._audioBandBuffer.Length)
        {
            BeatScale();
        }
    }

    public void BeatScale()
    {
        foreach (GameObject i in noteSpawner)
        {
            i.transform.localScale = new Vector3(i.transform.localScale.x,
                (AudioController._audioBand[scaleBand] * scaleMult) + startScale,
                i.transform.localScale.z);
        }
    }

    void setWaitTime()
    {
        waitTime = partitureList[posCounter];
        posCounter++;
    }

    void SpawnerInstantiator()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject noteSpawnerInstance = (GameObject) Instantiate(noteSpawnerPrefab);
            noteSpawnerInstance.transform.position = noteTarget.transform.position;
            noteSpawnerInstance.transform.parent = this.transform;
            noteSpawnerInstance.transform.position = Vector3.up * spawnerDistance;
            noteSpawnerInstance.name = "NoteSpawner" + (i + 1);
            noteSpawnerInstance.transform.RotateAround(gameObject.transform.position, Vector3.forward, -45 * i);
            noteSpawner[i] = noteSpawnerInstance;
        }
    }


    public void partitureListBuilder()
    {
        string[] stringArray = partitureString.Split(',');

        for (int e = 0; e < stringArray.Length; e++)
        {
            partitureList.Add(int.Parse(stringArray[e]));
        }
    }


    void SpawnNote()
    {
        if (posCounter < partitureList.Count)
        {
            int i = partitureList[posCounter] - 1;
            if (i >= 0)
            {
                GameObject noteInstance = (GameObject) Instantiate(notePrefab);
                noteInstance.GetComponent<NoteController>().targetObject = noteTarget;
                noteInstance.transform.position = noteSpawner[i].transform.position;
            }

            posCounter++;
        }
    }
}