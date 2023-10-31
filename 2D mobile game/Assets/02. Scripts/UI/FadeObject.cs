using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FadeObject : MonoBehaviour
{
    RectTransform rectTransform;

    public Vector2 rotationOffset = new Vector2(-100, -100);  // x, y만큼 떨어진 좌표
    public Vector2 initialPoint = new Vector2(-242, 74);
    public Vector2 targetPoint = new Vector2(-38, 93);

    public float fadeOutTime = 1.0f;
    public float fadeInTime = 1.0f;
    public float waitTime = 0.2f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        //RotateAroundLocalPoint(rotationOffset, rotationSpeed * Time.deltaTime);
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
        //transform.eulerAngles = new Vector3(0.0f, 0.0f, -45.0f);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 direction = targetPoint - initialPoint;
        float speed = direction.magnitude / fadeOutTime;
        direction = direction.normalized;

        float time = 0.0f;

        while (time <= fadeOutTime)
        {
            float step = speed * Time.deltaTime;
            rectTransform.anchoredPosition += (Vector2)(direction * step);
            time += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPoint;
        yield return new WaitForSeconds(waitTime);
        StartFadeIn();
    }

    private IEnumerator FadeIn()
    {
        float time = 0.0f;
        float rotateSpeed = 180.0f / fadeInTime;

        while (time < 180.0f)
        {         
            //if (time > 45.0f)
            //{
                Vector3 globalPoint = transform.TransformPoint(rotationOffset);
                transform.position = globalPoint;
                transform.Rotate(Vector3.forward, -Time.deltaTime * rotateSpeed);
                transform.position -= transform.TransformPoint(rotationOffset) - globalPoint;
            //}

            time += Time.deltaTime * rotateSpeed;
            yield return null;
        }

        rectTransform.anchoredPosition = initialPoint;
    }

    void RotateAroundLocalPoint(Vector2 localPoint, float angle)
    {
        Vector3 globalPoint = transform.TransformPoint(localPoint);

        // 1. 회전 중심 지점으로 이동
        transform.position = globalPoint;

        // 2. 원하는 각도로 회전
        transform.Rotate(Vector3.forward, angle);

        // 3. 원래의 위치로 되돌림
        transform.position -= transform.TransformPoint(localPoint) - globalPoint;
    }
}
