using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25.0f;
    [SerializeField] private float destroyTime = 2.0f;
    [SerializeField] private float alphaTime = 2.0f;

    Color alpha;
    public Canvas canvas;
    
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }


    void Start()
    {
        canvas = GameManager.Instance.uiCanvas;
        transform.SetParent(canvas.transform);

        UnityEngine.Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPoint, canvas.worldCamera, out UnityEngine.Vector2 localPoint);

        transform.localPosition = localPoint; // 로컬 좌표로 설정

        alpha = text.color;
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); 

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaTime);
        text.color = alpha;

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Print(float damage, Color? color = null)
    {
        text.text = Function.FloatToString(damage);
        text.color = color ?? Color.white;
    }
}
