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

    [Header("VFX")]
    public GameObject vfxItemDropPrefab;

    public void DroparItensEMoedas()
    {
        // Dropar Moedas
        if (moedaPrefab != null)
        {
            int quantidadeMoedas = Random.Range(minMoedas, maxMoedas + 1);
            for (int i = 0; i < quantidadeMoedas; i++)
            {
                Vector3 offset = new Vector3(Random.Range(-4, 4f), 0, Random.Range(-4f, 4f));
                Vector3 posicaoSpawn = transform.position + offset;

                Instantiate(moedaPrefab, posicaoSpawn, Quaternion.identity);
            }
        }

        // Dropar Item
        if (itensPossiveis.Length > 0 && Random.value < chanceDropItem)
        {
            int index = Random.Range(0, itensPossiveis.Length);

            Vector3 offset = new Vector3(Random.Range(-0.9f, 0.9f), 0, Random.Range(-0.9f, 0.9f));
            Vector3 posicaoSpawn = transform.position + offset + Vector3.up * 0.5f;

            GameObject item = Instantiate(itensPossiveis[index], posicaoSpawn, Quaternion.identity);

            // Instancia o VFX e o prende ao item
            if (vfxItemDropPrefab != null)
            {
                GameObject vfx = Instantiate(vfxItemDropPrefab, item.transform.position, Quaternion.identity);
                vfx.transform.SetParent(item.transform);
            }
        }
    }
}
