using UnityEngine;

public class ColetaItens : MonoBehaviour
{

public enum tipoItem{Cura, Bolha, Velocidade}
public tipoItem tipoDoItem = tipoItem.Cura;
public string colisionTag;

void OnTriggerEnter(Collider other)
{
    if (!other.gameObject.CompareTag(colisionTag))
        return;

    switch (tipoDoItem)
    {
        case tipoItem.Cura:
            // tentar acessar StatusPlayer e curar
            StatusPlayer player = other.gameObject.GetComponent<StatusPlayer>();
            if (player != null)
            {
                player.Curar(1); // ou a quantidade que você quiser
            }
            break;

        case tipoItem.Bolha:
            // efeito futuro
            break;

        case tipoItem.Velocidade:
            // efeito futuro
            break;
    }

    Destroy(gameObject); // item some após coleta
}
}
