using UnityEngine;

// Tipos de itens que podem existir na loja
public enum TipoItem
{
    Consumivel,
    Upgrade,
    Equipamento,
    AtivarCanhoesExtras
}

// Representa um item da loja
[System.Serializable]
public class ItemLoja
{
    [Header("Informações Básicas")]
    public string nomeItem;
    public int preco;
    [TextArea]
    public string descricao;
    public Sprite icone;
    public TipoItem tipoItem;

    [Header("Parâmetros do Efeito")]
    public int quantidadeCura;      // Para consumíveis
    public float aumentoVelocidade; // Para upgrades
    public int ataqueBonus;         // Para equipamentos

    // Aplica o efeito do item no jogador
    public void AplicarEfeito()
{
    switch (tipoItem)
    {
        case TipoItem.Consumivel:
            PlayerInventory.Instance.CurarJogador(quantidadeCura);
            break;
        case TipoItem.Upgrade:
            PlayerInventory.Instance.AumentarVelocidade(aumentoVelocidade);
            break;
        case TipoItem.Equipamento:
            PlayerInventory.Instance.AumentarAtaque(ataqueBonus);
            break;
        case TipoItem.AtivarCanhoesExtras:
            var scriptTiro = Object.FindFirstObjectByType<InstancieteBalaCanhao>();
            if (scriptTiro != null)
            {
                scriptTiro.AtivarCanhoesExtras();
            }
            else
            {
                Debug.LogWarning("Script de tiro não encontrado para ativar canhões extras.");
            }
            break;
        default:
            Debug.LogWarning("Tipo de item desconhecido!");
            break;
    }
}

}