using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInRandomDirection : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 이동 속도

    private Vector2 currentDirection = Vector2.zero;

    void Update()
    {
        // 스페이스바를 누르면 랜덤한 방향 설정
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetRandomDirection();
        }

        // 매 프레임마다 현재 방향으로 이동
        MoveInDirection();
    }

    void SetRandomDirection()
    {
        float randomAngleInRadians = Random.Range(0f, 2f * Mathf.PI); // 0부터 2π 사이의 랜덤한 값

        currentDirection = new Vector2(Mathf.Cos(randomAngleInRadians), Mathf.Sin(randomAngleInRadians));
    }

    void MoveInDirection()
    {
        transform.position += (Vector3)currentDirection * moveSpeed * Time.deltaTime;
    }
}
