using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameScene GameScene;
    public Player Player;
    public bool gameStarted = false;
    public int startEnemyHealth = 1;

    public float timeToNextWave = 3f;
    float waveTimeStamp = 0;
    public int enemyInWave = 3;

    int waveNumber = 0;

    public List<GameObject> EnemyPrefabs = new List<GameObject>();
    public List<Transform> SpawnPoints = new List<Transform>();
    // Use this for initialization
    void Start () {
        waveTimeStamp = timeToNextWave;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameScene.gamePaused)
        {
            return;
        }
        if (gameStarted)
        {
            waveTimeStamp += Time.deltaTime;
            if (waveTimeStamp >= timeToNextWave)
            {
                waveTimeStamp = 0;
                ++waveNumber;
                for (int i = 0; i < enemyInWave + waveNumber / 20; ++i)
                {
                    SpawnEnemy();
                }
            }
        }
	}

    public void SpawnEnemy()
    {
        Enemy enemy = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)]).GetComponent<Enemy>();
        enemy.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Count)].position;
        enemy.Player = Player;
        enemy.GameScene = GameScene;
        enemy.Setup(startEnemyHealth + (int)(waveNumber / 10f));
    }
}
