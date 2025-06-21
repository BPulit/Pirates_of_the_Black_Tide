using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "TripulanteBomba", menuName = "Tripulantes/Bomba")]
public class TripulanteBomba : Tripulante
{
    public GameObject missilPrefab;

    public override void AtivarHabilidade(GameObject jogador)
    {
        base.AtivarHabilidade(jogador);
        Debug.Log("Tripulante Bomba ativado");

        Transform[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo")
            .Concat(GameObject.FindGameObjectsWithTag("Boss"))
            .Select(go => go.transform)
            .OrderBy(t => Vector3.Distance(jogador.transform.position, t.position))
            .ToArray();

        int quantidadeAlvos = Mathf.Min(2, inimigos.Length);

        for (int i = 0; i < quantidadeAlvos; i++)
        {
            Vector3 pos = jogador.transform.position + Vector3.up * (2 + i);
            GameObject missil = Instantiate(missilPrefab, pos, Quaternion.identity);
            AudioManager.Instance.TocarSomDirecional(7, missil.transform.position);

            MissilTeleguiado missilScript = missil.GetComponent<MissilTeleguiado>();
            if (missilScript != null)
            {
                missilScript.DefinirAlvo(inimigos[i]);
            }
        }
    }
}
