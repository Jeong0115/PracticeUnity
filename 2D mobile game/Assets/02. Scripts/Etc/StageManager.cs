using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public FadeEffect fadeEffect;
    public FadeObject fadeObject;

    public int StageNumber { get; private set; } = 1;
    public int NextStageKillScore { get; private set; } = 10;
    public int KillScore { get; private set; } = 0;

    public float CurTime { get; private set; } = 0;
    public float MaxTime { get; private set; } = 30.0f;

    private int maxMonsterCnt = 10;
    private int curMonsterCnt = 0;
    private int monsterIndex = 0;

    private IEnumerator spawnCoroutine;
    private float spawnInterval = 2.0f;
    
    private void Awake()
    {
        Instance = this;
        spawnCoroutine = SpawnMonsterCoroutine();
    }

    private void Start()
    {
        StartCoroutine(spawnCoroutine);
    }

    private void Update()
    {
        CurTime += Time.deltaTime;

        if (CurTime > MaxTime)
        {
            CurTime = 0.0f;
            StartCoroutine(LoadPrevStage());
        }
    }

    public void AddKillScore()
    {
        KillScore++;
        curMonsterCnt--;

        if (KillScore >= NextStageKillScore)
        {
            StartCoroutine(LoadNextStage());
        }
    }

    IEnumerator LoadNextStage()
    {
        StopCoroutine(spawnCoroutine);
        GameManager.Instance.PoolManager.ClearGameObject(monsterIndex);

        fadeEffect.StartFadeOut();
        fadeObject.StartFadeOut();

        KillScore = 0;
        curMonsterCnt = 0;

        yield return new WaitForSeconds(0.7f);

        StageNumber++;              
        KillScore = 0;
        curMonsterCnt = 0;
        NextStageKillScore = (int)(NextStageKillScore * 1.2f);

        GameManager.Instance.player.health.SetMaxHp();
        StartCoroutine(spawnCoroutine);
        CurTime = 0.0f;
    }

    IEnumerator LoadPrevStage()
    {
        StopCoroutine(spawnCoroutine);
        GameManager.Instance.PoolManager.ClearGameObject(monsterIndex);

        fadeEffect.StartFadeOut();
        fadeObject.StartFadeOut();

        KillScore = 0;
        curMonsterCnt = 0;

        yield return new WaitForSeconds(0.7f);

        StageNumber--;
        KillScore = 0;
        curMonsterCnt = 0;
        NextStageKillScore = (int)(NextStageKillScore / 1.2f);

        GameManager.Instance.player.health.SetMaxHp();
        StartCoroutine(spawnCoroutine);
        CurTime = 0.0f;
    }

    private void SpawnMonster()
    {
        if (curMonsterCnt < maxMonsterCnt)
        {
            for (int i = 0; i < maxMonsterCnt - curMonsterCnt; i++)
            {
                GameObject monster = GameManager.Instance.PoolManager.GetGameObject(monsterIndex);

                float randomX = Random.Range(-2.0f, 2.0f);
                float randomY = Random.Range(-4.0f, 4.0f);

                monster.transform.position = new Vector3(randomX, randomY, 0.0f);
            }
            curMonsterCnt = maxMonsterCnt;
        }
    }

    IEnumerator SpawnMonsterCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnMonster();
        }
    }
}
