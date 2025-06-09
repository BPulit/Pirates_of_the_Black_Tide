using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxEnemies = 5;

    private float timer = 0f;
    private int currentEnemies = 0;
    public bool spawnAtivo = true;
    public bool estaNaSafe = false;
    public bool estaNoBoss = false;
    private List<GameObject> inimigosAtivos = new List<GameObject>();

    private Transform player;

    void Start()
    {
        // Pega o Transform do jogador pela tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
{
    timer += Time.deltaTime;

    if (timer >= spawnInterval && currentEnemies < maxEnemies && spawnAtivo && !estaNaSafe && !estaNoBoss)
    {
        SpawnEnemy();
        timer = 0f;
    }
}

    void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        inimigosAtivos.Add(enemy);


        currentEnemies++;

        // Passa o Transform do jogador para o inimigo
        EnemieMove moveScript = enemy.GetComponent<EnemieMove>();
        if (moveScript != null)
        {
            moveScript.player = player;
        }

        // Escuta o evento de morte (se existir)
        StatusEnemie status = enemy.GetComponent<StatusEnemie>();
        if (status != null)
        {
            status.OnDeath += () => { currentEnemies--; };
        }
    }
    
    public void DestruirInimigosAtivos()
    {
    foreach (var inimigo in inimigosAtivos)
    {
        if (inimigo != null)
            Destroy(inimigo);
    }
    inimigosAtivos.Clear();
    currentEnemies = 0;
    }
}