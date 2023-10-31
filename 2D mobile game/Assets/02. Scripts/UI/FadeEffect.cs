using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    Image image;

    public float fadeInTime = 1.0f;
    public float fadeOutTime = 1.0f;
    public float waitTime = 0.2f;

    void Awake()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {

    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        image.color = Color.clear;
        image.fillAmount = 1;

        while (image.color.a < 1)
        {
            Color color = image.color;
            color.a += Time.deltaTime / fadeOutTime;

            image.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        StartFadeIn();
    }

    private IEnumerator FadeIn()
    {
        while (image.fillAmount > 0)
        {
            //Color color = image.color;
            //color.a -= Time.deltaTime / fadeInTime / 2.0f;

            //image.color = color;

            image.fillAmount -= Time.deltaTime / fadeInTime;

            yield return null;
        }
    }
}
