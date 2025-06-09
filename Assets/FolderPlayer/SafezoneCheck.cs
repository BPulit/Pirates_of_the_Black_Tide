using UnityEngine;

public class SafezoneCheck : MonoBehaviour
{
    [Header("Spawner de Inimigos")]
    public EnemySpawner spawner;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Para o spawn dos inimigos
        if (spawner != null)
        {
            spawner.estaNaSafe = true;
            spawner.spawnAtivo = false;
            spawner.DestruirInimigosAtivos();
        }

    }

    void OnTriggerExit(Collider other)
    {
         if (!other.CompareTag("Player")) return;

        
        if (spawner != null)
        {
            spawner.estaNaSafe = false;
            spawner.spawnAtivo = true;
            
        }
    }
}
