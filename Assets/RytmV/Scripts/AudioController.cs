﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public static AudioController aControllerInstance;
    [SerializeField] public AudioSource audioSourceFile;
    [SerializeField] public int baseBandPower = 100;
    bool pause;
    static float[] _samplesLeft = new float[512];
    static float[] _samplesRight = new float[512];
    
    public float[] _freqBand = new float[8];

    float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];

    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];

    public float Amplitude, AmplitudeBuffer;

    float _AmplitudeHighest;
    public float _audioProfile;

    public enum _channel
    {
        Stereo,
        Left,
        Right
    };

    /*Frequency bands for
     * Sub Bass: 20 to 60 Hz
     * Bass 60: to 250 Hz
     * Low-Mids: 250 to 500 Hz
     * Mids: 500 to 2kHz
     * Upper Mids: 2 to 4kHz
     * Presence 4kHz to 6kHz
     * Brilliance 6kHz to 20kHz
     */

    /*
        *** The Frequency Bands ***
     
     * [0]Sub Bass:       0   -   86Hz
     * [1]Bass:          87   -   258Hz
     * [2]Low-Mids:     259   -   602Hz
     * [3]Mids:         603   -  1290Hz
     * [4]Upper-Mids:   1291  -  2666Hz
     * [5]Presence:     2667  -  5418Hz
     * [6]Brilliance:   6419  -  10922Hz
     * [7]Dog Whistle: 10923  -  21930Hz
     
       This comes from Peer Play on YouTube @
       "Audio Visualization - Unity/C# Tutorial"
     */

    public _channel channel = new _channel();

    // Start is called before the first frame update
    void Start()
    {
        aControllerInstance = this;
        audioSourceFile = GetComponent<AudioSource>();
        AudioProfile(_audioProfile);
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    public void PlayMusic()
    {
        if (pause)
        {
            audioSourceFile.Play(0);
            pause = false;
        }
    }

    public void PauseMusic()
    {
        if (!pause)
        {
            audioSourceFile.Pause();
            pause = true;
        }
    }

    public bool IsPaused()
    {
        return pause;
    }

    public bool MusicEnded()
    {
        if (!this.audioSourceFile.isPlaying && !pause)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void AudioProfile(float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = audioProfile;
        }
    }

    void GetSpectrumAudioSource()
    {
        audioSourceFile.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        audioSourceFile.GetSpectrumData(_samplesRight, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int) Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == _channel.Stereo)
                {
                    average += _samplesLeft[count] + _samplesRight[count] * (count + 1);
                }

                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count] * (count + 1);
                }

                if (channel == _channel.Right)
                {
                    average += _samplesLeft[count] * (count + 1);
                }

                count++;
            }

            average /= count;
            _freqBand[i] = average * baseBandPower;
        }
    }

    //This creates a smooth downfall when the amplitude is lower than the previous value, this is the impression that
    //the audio signal is pushing up the blocks and there's almost like an air cushion inside of them as they ease down
    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }

            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }

            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }


    void GetAmplitude()
    {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }

        if (_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }

        Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }
}