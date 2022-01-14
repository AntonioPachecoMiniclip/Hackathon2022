using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    public Image avatar;
    public Image timer;
    public Image timerBg;

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
}
