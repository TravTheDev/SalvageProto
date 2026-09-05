using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    private class WaveDefinition
    {
        [Min(1)]
        public int enemyCount = 3;

        [Min(0f)]
        public float spawnInterval = 0.75f;
    }

    [Header("Enemies")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Waves")]
    [SerializeField] private WaveDefinition[] waves;

    [SerializeField]
    private float timeBetweenWaves = 4f;

    private int currentWaveIndex = -1;
    private int livingEnemies;

    private bool isSpawningWave;
    private bool gameComplete;

    public int CurrentWaveNumber => currentWaveIndex + 1;
    public int LivingEnemies => livingEnemies;

    public event Action<int> WaveStarted;
    public event Action AllWavesCompleted;

    public event Action<int> LivingEnemiesChanged;

    private void Start()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        if (gameComplete)
        {
            return;
        }

        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            CompleteAllWaves();
            return;
        }

        StartCoroutine(
            SpawnWaveRoutine(waves[currentWaveIndex])
        );
    }

    private IEnumerator SpawnWaveRoutine(
        WaveDefinition wave)
    {
        isSpawningWave = true;

        WaveStarted?.Invoke(CurrentWaveNumber);

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(
                wave.spawnInterval
            );
        }

        isSpawningWave = false;

        CheckWaveComplete();
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab =
            enemyPrefabs[
                UnityEngine.Random.Range(
                    0,
                    enemyPrefabs.Length
                )
            ];

        Transform spawnPoint =
            spawnPoints[
                UnityEngine.Random.Range(
                    0,
                    spawnPoints.Length
                )
            ];

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Health enemyHealth =
            enemy.GetComponent<Health>();

        if (enemyHealth == null)
        {
            Destroy(enemy);
            return;
        }

        livingEnemies++;
        LivingEnemiesChanged?.Invoke(livingEnemies);

        enemyHealth.Died += HandleEnemyDied;
    }

    private void HandleEnemyDied()
    {
        livingEnemies =
            Mathf.Max(livingEnemies - 1, 0);
        LivingEnemiesChanged?.Invoke(livingEnemies);

        CheckWaveComplete();
    }

    private void CheckWaveComplete()
    {
        if (isSpawningWave || livingEnemies > 0)
        {
            return;
        }

        StartCoroutine(
            BeginNextWaveAfterDelay()
        );
    }

    private IEnumerator BeginNextWaveAfterDelay()
    {
        yield return new WaitForSeconds(
            timeBetweenWaves
        );

        StartNextWave();
    }

    private void CompleteAllWaves()
    {
        gameComplete = true;

        AllWavesCompleted?.Invoke();
    }
}