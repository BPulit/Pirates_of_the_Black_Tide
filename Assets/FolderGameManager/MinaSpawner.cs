using System.Collections.Generic;
using UnityEngine;

public class MinaSpawner : MonoBehaviour
{
    public GameObject prefabMina;
    public int quantidadeMinas = 5;
    public float raioSpawn = 20f;

    private List<GameObject> minasAtivas = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < quantidadeMinas; i++)
        {
            SpawnarMina();
        }
    }

    void Update()
    {
        // Mantém a quantidade mínima de minas
        while (minasAtivas.Count < quantidadeMinas)
        {
            SpawnarMina();
        }
    }

    void SpawnarMina()
    {
        Vector3 posicaoAleatoria = transform.position + Random.insideUnitSphere * raioSpawn;
        posicaoAleatoria.y = WaterMath.CalcularAlturaDaOnda(posicaoAleatoria);

        GameObject mina = Instantiate(prefabMina, posicaoAleatoria, Quaternion.identity);
        minasAtivas.Add(mina);

        // Informa a mina quem é o spawner
        MinaAquatica minaScript = mina.GetComponent<MinaAquatica>();
        if (minaScript != null)
        {
            minaScript.spawnerOrigem = this;
        }
    }

    public void NotificarDestruicao(GameObject mina)
    {
        minasAtivas.Remove(mina);
    }
}
