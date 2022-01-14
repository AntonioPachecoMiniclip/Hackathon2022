using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SceneBoundSingletonBehaviour<SoundManager>
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
                playAudioClip(soundbank.playPlasticCap);
                break;
            case CapType.Metal:
                playAudioClip(soundbank.playMetalCap);
                break;
        }
    }

    public void playCheer() {
        playAudioClip(soundbank.cheering);
    }
    
    public void PlayRespawnSound() 
    {
        playAudioClip(soundbank.puff);
    }

    public void playTickingSound() 
    {
        playAudioClip(soundbank.ticking);
    }
    public void playStartTimerSound() 
    {
        playAudioClip(soundbank.startTicking);
    }

    public void playEndTimerSound() 
    {
        playAudioClip(soundbank.endTicking);
    }

    void playAudioClip(AudioClip clip) {
        sources[sourceIndex].clip = clip;
        sources[sourceIndex].Play();
        incrementSourceIndex();
    }

    void incrementSourceIndex() {
        sourceIndex = (sourceIndex + 1) % sources.Length;
    }
}
