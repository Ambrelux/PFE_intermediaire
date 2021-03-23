using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData : MonoBehaviour
{
    private int _frequency;
    private static SoundData _instance;
    public static SoundData Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
        _frequency = 250;
    }

    public void UpdateFrequency(int freq)
    {
        _frequency = freq;
    }
    
    public int Frequency => _frequency;
}
