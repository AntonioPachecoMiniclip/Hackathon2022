using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    Soundbank soundbank;
    AudioSource source;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    public void playCapSound(CapType type) {
        switch(type) {
            case CapType.Plastic:
                source.clip = soundbank.playPlasticCap;
                break;
            case CapType.Metal:
                source.clip = soundbank.playMetalCap;
                break;
        }   
        source.Play();
    }
}
