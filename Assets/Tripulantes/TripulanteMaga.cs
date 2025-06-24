using UnityEngine;

[CreateAssetMenu(fileName = "TripulanteMaga", menuName = "Tripulantes/Maga do Clima")]
public class TripulanteMaga : Tripulante
{
    public GameObject prefabRaio;
    public int quantidadeDeRaios = 8;
    public float raioDoCirculo = 8f;
    public float duracaoRaio = 3f;
    public float danoPorSegundo = 2f;

    public override void AtivarHabilidade(GameObject jogador)
    {
        base.AtivarHabilidade(jogador);
        AudioManager.Instance.TocarSomEfeito(14);

        Vector3 centro = jogador.transform.position;

        for (int i = 0; i < quantidadeDeRaios; i++)
        {
            float angulo = i * Mathf.PI * 2 / quantidadeDeRaios;
            Vector3 pos = centro + new Vector3(Mathf.Cos(angulo), 0, Mathf.Sin(angulo)) * raioDoCirculo;
            pos.y += 10f; // Raio desce do céu

            GameObject raio = GameObject.Instantiate(prefabRaio, pos, Quaternion.identity);
            RaioTemporal r = raio.GetComponent<RaioTemporal>();
            if (r != null)
            {
                r.Configurar(danoPorSegundo, duracaoRaio);
            }
        }
    }
}
