using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TweenScript : MonoBehaviour
{
    [SerializeField] private float timeToPopIn;
    [SerializeField] private float timeDelayed;
    [SerializeField] private LeanTweenType leanTweenType;
    // Start is called before the first frame update
        private void Start()
    {
        Image img = gameObject.GetComponent<Image>();
        if (img != null)
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        gameObject.transform.localScale = Vector3.zero;
        
        LeanTween.delayedCall(gameObject, timeDelayed, () => {
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), timeToPopIn).setEase(leanTweenType);
            //LeanTween.alpha(gameObject.GetComponent<RectTransform>(), .4f, timeToPopIn).setEase(leanTweenType);
        });
    }
}
