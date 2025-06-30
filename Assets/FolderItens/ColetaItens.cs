using UnityEngine;

public class ColetaItens : MonoBehaviour
{

public enum tipoItem{Cura, Bolha, Velocidade}
public tipoItem tipoDoItem = tipoItem.Cura;
public string colisionTag;

        private void Start()
    {

        Destroy(gameObject, 40f);
    
    }

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
                    AudioManager.Instance.TocarSomEfeito(16);
                }
                break;

            case tipoItem.Bolha:
                AudioManager.Instance.TocarSomEfeito(8);
                BolhaUtil.AtivarBolha(other.gameObject, StatusPlayer.Instance.prefabBolha, 5f); // Duração do item
                break;

        }
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Destroy(gameObject); // item some após coleta
    }


}
