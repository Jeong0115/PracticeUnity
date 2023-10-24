using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInRandomDirection : MonoBehaviour
{
    public float moveSpeed = 1.0f; // �̵� �ӵ�

    private Vector2 currentDirection = Vector2.zero;

    void Update()
    {
        // �����̽��ٸ� ������ ������ ���� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetRandomDirection();
        }

        // �� �����Ӹ��� ���� �������� �̵�
        MoveInDirection();
    }

    void SetRandomDirection()
    {
        float randomAngleInRadians = Random.Range(0f, 2f * Mathf.PI); // 0���� 2�� ������ ������ ��

        currentDirection = new Vector2(Mathf.Cos(randomAngleInRadians), Mathf.Sin(randomAngleInRadians));
    }

    void MoveInDirection()
    {
        transform.position += (Vector3)currentDirection * moveSpeed * Time.deltaTime;
    }
}