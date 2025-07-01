using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "TripulanteBomba", menuName = "Tripulantes/Bomba")]
public class TripulanteBomba : Tripulante
{
    public GameObject missilPrefab;

    public override void AtivarHabilidade(GameObject jogador)
    {
        base.AtivarHabilidade(jogador);
        
        Transform[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo")
            .Concat(GameObject.FindGameObjectsWithTag("Boss"))
            .Select(go => go.transform)
            .OrderBy(t => Vector3.Distance(jogador.transform.position, t.position))
            .ToArray();

        if (inimigos.Length == 0)
        {
            MensagemUI.instance?.MostrarMensagem("No enemy or boss found to target with missiles.");
            return;
        }

        int quantidadeAlvos = Mathf.Min(2, inimigos.Length);

        for (int i = 0; i < quantidadeAlvos; i++)
        {
            Vector3 pos = jogador.transform.position + Vector3.up * (2 + i);
            GameObject missil = Instantiate(missilPrefab, pos, Quaternion.identity);

            if (AudioManager.Instance != null)
                AudioManager.Instance.TocarSomDirecional(7, missil.transform.position);

            MissilTeleguiado missilScript = missil.GetComponent<MissilTeleguiado>();
            if (missilScript != null)
            {
                missilScript.DefinirAlvo(inimigos[i]);
            }
            else
            {
                Debug.LogError("Missil instanciado não possui o script MissilTeleguiado!");
            }
        }
    }
}
