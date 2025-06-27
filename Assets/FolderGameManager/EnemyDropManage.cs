using UnityEngine;

public class EnemyDropManager : MonoBehaviour
{
    [Header("Moedas")]
    public GameObject moedaPrefab;
    public int minMoedas = 1;
    public int maxMoedas = 5;

    [Header("Itens")]
    public GameObject[] itensPossiveis;  
    public float chanceDropItem = 0.3f;

    public void DroparItensEMoedas()
{
        // Dropar Moedas
        if (moedaPrefab != null)
        {
            int quantidadeMoedas = Random.Range(minMoedas, maxMoedas + 1);
            for (int i = 0; i < quantidadeMoedas; i++)
            {
                // Gera um deslocamento pequeno no chão
                Vector3 offset = new Vector3(Random.Range(-4, 4f), 0, Random.Range(-4f, 4f));
                Vector3 posicaoSpawn = transform.position + offset;

                Instantiate(moedaPrefab, posicaoSpawn, Quaternion.identity);
            }
    }

    // Dropar Item (cura, buff, etc.)
    if (itensPossiveis.Length > 0 && Random.value < chanceDropItem)
    {
        int index = Random.Range(0, itensPossiveis.Length);

        // Mesmo sistema de espalhar
        Vector3 offset = new Vector3(Random.Range(-0.9f, 0.9f), 0, Random.Range(-0.9f, 0.9f));
        Vector3 posicaoSpawn = transform.position + offset + Vector3.up * 0.5f; // Sobe um pouco no Y pra não atravessar o chão

        Instantiate(itensPossiveis[index], posicaoSpawn, Quaternion.identity);
    }
}
}
