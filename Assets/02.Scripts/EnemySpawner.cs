using NUnit.Framework;
using System.Collections.Generic;
using Unity.FPS.AI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public GameObject EnemyPrefab;

    private float _currentTime;
    private float responTime = 5f;
    private int maxCount = 10;

    public StageLevelManager StageLevelManager;

    private void Awake()
    {
        StageLevelManager.OnLevelChanged += SetSpawner;
    }



    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= responTime)
        {
            _currentTime = 0f;

            int enemyCount = GameObject.FindObjectsByType<EnemyController>(FindObjectsSortMode.None).Length;
            if (enemyCount >= maxCount)
            {
                return;
            }

            var randomIndex = Random.Range(0, SpawnPoints.Count);
            Instantiate(EnemyPrefab, SpawnPoints[randomIndex].position, Quaternion.identity);
        }
    }

    private void SetSpawner()
    {
        StageData stageData = StageLevelManager.StageDataSetter.GetStageData(StageLevelManager.Level);
        responTime = stageData.respawnTime;
        maxCount = stageData.maxEnemyCount;
    }
}

