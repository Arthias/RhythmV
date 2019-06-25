using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioController : MonoBehaviour
{
    public static AudioController aControllerInstance;
    
    [SerializeField]
    public AudioSource audioFile;

    public float[] samplesLeft, samplesRight = new float[512];

    [SerializeField]
    public static float[] freqBands = new float [8];

    [SerializeField]

    public int baseBandPower = 10;

    public bool pause;



    //Spectrum
    void GetSpectrumAudio(){
    audioFile.GetSpectrumData(samplesLeft, 0, FFTWindow.Blackman);
    audioFile.GetSpectrumData(samplesRight, 1, FFTWindow.Blackman);
    }


    void CreateFreqBands(){

        int c = 0;

        for (int i = 0; i < 8 ; i++){

            float average = 0;
            int sampleCount = (int)Mathf.Pow (2,i) * 2;

            if (i==7){
                sampleCount+=2;
            }
            
            for (int j = 0; j < sampleCount; j++)
            {
                average += samplesLeft[c] + samplesRight[c] * (c+1);
                c++;
            }
            
            average /= c;

            freqBands[i]=average * baseBandPower;

        }
        
    }



    //AudioControl
    public void PlayMusic(){
        if (pause){
            audioFile.Play(0);
            pause=false;
        }
    }

    public void PauseMusic(){
        if (!pause){
            audioFile.Pause();
            pause = true;
        }
    }




//
    void Start()
    {
        aControllerInstance = this;
        audioFile=GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudio();
        CreateFreqBands();
        if (audioFile.isPlaying && !pause){
            
        }
    }


}
