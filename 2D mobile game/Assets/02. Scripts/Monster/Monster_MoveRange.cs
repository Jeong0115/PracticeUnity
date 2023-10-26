using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_MoveRange : MonoBehaviour
{
    [SerializeField] private Monster monster;

    private void Update()
    {


    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            monster.PlayerInMoveRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            monster.PlayerInMoveRange(false);
        }
    }
}
