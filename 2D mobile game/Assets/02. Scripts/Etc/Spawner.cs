using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] point;

    private void Awake()
    {
        point = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject monster = GameManager.Instance.PoolManager.GetGameObject(0);
        monster.transform.position = point[Random.Range(1, point.Length)].transform.position;
    }
}
