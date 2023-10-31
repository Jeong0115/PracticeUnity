using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject[] objects;

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[objects.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetGameObject(int index)
    {
        GameObject gameObject = null;

        foreach (GameObject obj in pools[index])
        {
            if(!obj.activeSelf)
            {
                gameObject = obj;
                gameObject.SetActive(true);
                break;
            }
        }

        if(gameObject == null)
        {
            gameObject = Instantiate(objects[index], transform); // 하이어라키 정리 용도로 풀 매니저의 자식으로 설정;
            pools[index].Add(gameObject);
        }

        return gameObject;
    }

    public void ClearGameObject(int index)
    {
        foreach (GameObject obj in pools[index])
        {
            if (obj.activeSelf)
            {
                obj.SetActive(false);
            }
        }
    }
}
