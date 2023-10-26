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
            gameObject = Instantiate(objects[index], transform); // ���̾��Ű ���� �뵵�� Ǯ �Ŵ����� �ڽ����� ����;
            pools[index].Add(gameObject);
        }

        return gameObject;
    }
}