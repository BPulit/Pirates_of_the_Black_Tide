using UnityEngine;
using System.Collections.Generic;

public class SpawnerPeixes : MonoBehaviour
{
    [Header("Prefabs")]
    public List<GameObject> prefabsCardume;
    public List<GameObject> prefabsSoltos;

    [Header("Configuração de Spawns")]
    public int quantidadeCardumes = 6;
    public int quantidadePeixesSoltos = 8;
    public Vector3 centroSpawn = Vector3.zero;
    public float areaSpawn = 50f;
    public float distanciaCardume = 2f;


    void Start()
    {
        // Spawn Peixes Soltos
        for (int i = 0; i < quantidadePeixesSoltos; i++)
        {
            GameObject prefab = ObterPrefabAleatorio(prefabsSoltos);
            if (prefab != null)
            {
                Vector3 pos = GerarPosicaoAleatoria();
                SpawnarPeixe(prefab, pos);
            }
        }

        // Spawn de Cardumes
        for (int i = 0; i < quantidadeCardumes; i++)
        {
            Vector3 centroCardume = GerarPosicaoAleatoria();
            int quantidade = Random.Range(3, 6); // de 3 a 5 peixes por cardume

            for (int j = 0; j < quantidade; j++)
            {
                GameObject prefab = ObterPrefabAleatorio(prefabsCardume);
                if (prefab != null)
                {
                    Vector3 offset = new Vector3(
                        Random.Range(-distanciaCardume, distanciaCardume),
                        0,
                        Random.Range(-distanciaCardume, distanciaCardume)
                    );
                    Vector3 pos = centroCardume + offset;
                    SpawnarPeixe(prefab, pos);
                }
            }
        }
    }

    GameObject ObterPrefabAleatorio(List<GameObject> lista)
    {
        if (lista == null || lista.Count == 0) return null;
        return lista[Random.Range(0, lista.Count)];
    }

    Vector3 GerarPosicaoAleatoria()
    {
        return centroSpawn + new Vector3(
            Random.Range(-areaSpawn, areaSpawn),
            0,
            Random.Range(-areaSpawn, areaSpawn)
        );
    }

    void SpawnarPeixe(GameObject prefab, Vector3 posicao)
    {
        GameObject peixe = Instantiate(prefab, posicao, Quaternion.identity);
    }
}
