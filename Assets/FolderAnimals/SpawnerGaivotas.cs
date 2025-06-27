using UnityEngine;
using System.Collections.Generic;

public class SpawnerGaivotas : MonoBehaviour
{
    [Header("Prefabs de Gaivotas")]
    public List<GameObject> prefabsGaivota;

    [Header("Configuração dos Bandos")]
    public int quantidadeBandos = 5;
    public float distanciaEntreGaivotas = 3f;
    public Vector3 centroSpawn = Vector3.zero;
    public float areaSpawn = 60f;

    void Start()
    {
        for (int i = 0; i < quantidadeBandos; i++)
        {
            Vector3 centroBando = GerarPosicaoAleatoria();

            int quantidadePorBando = Random.Range(3, 6); // 3 a 5 por bando
            for (int j = 0; j < quantidadePorBando; j++)
            {
                Vector3 offset = new Vector3(
                    Random.Range(-distanciaEntreGaivotas, distanciaEntreGaivotas),
                    0,
                    Random.Range(-distanciaEntreGaivotas, distanciaEntreGaivotas)
                );

                Vector3 pos = centroBando + offset;
                GameObject prefab = ObterPrefabAleatorio();

                if (prefab != null)
                {
                    GameObject gaivota = Instantiate(prefab, pos, Quaternion.identity);

                    // Informa à gaivota qual é o centro do voo (mesmo usado no peixe)
                    MovimentoGaivota mv = gaivota.GetComponent<MovimentoGaivota>();
                    if (mv != null)
                        mv.DefinirCentro(centroSpawn);
                }
            }
        }
    }

    GameObject ObterPrefabAleatorio()
    {
        if (prefabsGaivota == null || prefabsGaivota.Count == 0)
            return null;

        return prefabsGaivota[Random.Range(0, prefabsGaivota.Count)];
    }

    Vector3 GerarPosicaoAleatoria()
    {
        return centroSpawn + new Vector3(
            Random.Range(-areaSpawn, areaSpawn),
            0,
            Random.Range(-areaSpawn, areaSpawn)
        );
    }
}
