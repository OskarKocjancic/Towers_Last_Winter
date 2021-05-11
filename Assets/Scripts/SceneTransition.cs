using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneTransition : MonoBehaviour
{
    [SerializeField] private bool fadeInOnAwake;
    [SerializeField] private float timeToFadeIn;
    [SerializeField] private float timeToFadeOut;
    [SerializeField] private float timeDelayed;
    [SerializeField] private LeanTweenType leanTweenType;
    [SerializeField] private GameObject transitionFrame;

    private void Awake()
    {
        if (fadeInOnAwake)
            FadeFromBlack();
    }
    public void FadeFromBlack()
    {
        Color color = transitionFrame.GetComponent<Image>().color;
        color.a = 1f;
        transitionFrame.GetComponent<Image>().color = color;
        LeanTween.delayedCall(transitionFrame, timeDelayed, () =>
            LeanTween.alpha(transitionFrame.GetComponent<RectTransform>(), 0f, timeToFadeIn).setEase(leanTweenType));
    }

    public void FadeToBlack()
    {
        Color color = transitionFrame.GetComponent<Image>().color;
        color.a = 0f;
        transitionFrame.GetComponent<Image>().color = color;
        LeanTween.delayedCall(transitionFrame, timeDelayed, () =>
            LeanTween.alpha(transitionFrame.GetComponent<RectTransform>(), 1f, timeToFadeOut).setEase(leanTweenType));
    }

}
