using UnityEngine;

public class MinaSpawner : MonoBehaviour
{
    public GameObject prefabMina;
    public int quantidadeMinas = 5;
    public float raioSpawn = 20f;

    void Start()
    {
        for (int i = 0; i < quantidadeMinas; i++)
        {
            Vector3 posicaoAleatoria = transform.position + Random.insideUnitSphere * raioSpawn;
            posicaoAleatoria.y = WaterMath.CalcularAlturaDaOnda(posicaoAleatoria); // ou altura fixa

            Instantiate(prefabMina, posicaoAleatoria, Quaternion.identity);
        }
    }
}
