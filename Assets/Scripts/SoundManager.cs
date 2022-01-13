using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    Soundbank soundbank;
    [SerializeField]
    AudioSource[] sources;
    int sourceIndex = 0;

    void Awake() {
        sources = GetComponents<AudioSource>();
    }

    public void playCapSound(CapType type) {
        switch(type) {
            case CapType.Plastic:
                sources[sourceIndex].clip = soundbank.playPlasticCap;
                break;
            case CapType.Metal:
                sources[sourceIndex].clip = soundbank.playMetalCap;
                break;
        }   
        sources[sourceIndex].Play();
        incrementSourceIndex();
    }

    void incrementSourceIndex() {
        sourceIndex = (sourceIndex + 1) % sources.Length;
    }
}
