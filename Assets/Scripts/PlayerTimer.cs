using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    public Image avatar;
    public Image timer;
    public Image timerBg;

    public Image medal;

    static string[] medalSpriteNames = {"medal_1","medal_2","medal_3","medal_4"};
    static int nextMedalSprite = 0;

    Coroutine timerCoroutine;

    void Awake() {
        endTimer();
    }

    public void startTimer(float duration) {
        avatar.rectTransform.localScale = Vector3.one;
        timerCoroutine = StartCoroutine(fillTimer(duration));
    }

    public void endTimer() {
        if(timerCoroutine != null) {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        avatar.rectTransform.localScale = Vector3.one * 0.7f;
        timer.fillAmount = 1.0f;
    }

    IEnumerator fillTimer(float duration) {
        float normalizedTime = 0;
        while(normalizedTime <= 1f)
        {
            timer.fillAmount = 1.0f - normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        endTimer();
    }

    public void OnEndedTrack() {
        medal.enabled = true;
        medal.overrideSprite = Resources.Load<Sprite>(medalSpriteNames[nextMedalSprite++]);

        nextMedalSprite = nextMedalSprite % medalSpriteNames.Length;
    }
}
