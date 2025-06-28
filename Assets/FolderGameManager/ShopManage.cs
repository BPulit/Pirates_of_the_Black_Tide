using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Itens Disponíveis na Loja")]
    public List<ItemLoja> itensDisponiveis = new List<ItemLoja>();

    public Transform painelItensUI; // onde os botões vão aparecer
    public GameObject prefabUIItem;

    void Start()
    {
        MostrarItensNaUI();
    }

    // Método para tentar comprar um item
   public void ComprarItem(ItemLoja item)
{
    if (item == null)
    {
        Debug.LogWarning("Tentando comprar um item nulo!");
        return;
    }

   if (PlayerInventory.Instance.GetGold() >= item.preco)
    {
        CurrencyManager.instance.GastarMoedas(item.preco);
        PlayerInventory.Instance.AddItem(item); // Já aplica o efeito dentro desse método

        itensDisponiveis.Remove(item);
        MostrarItensNaUI(); // Atualiza os botões da loja
    }

    else
    {
        MensagemUI.instance.MostrarMensagem("Ouro insuficiente para comprar: " + item.nomeItem);
    }
}


    // Exibe todos os itens disponíveis no console (para testes)
    public void ListarItens()
    {
        Debug.Log("Itens disponíveis na loja:");
        foreach (ItemLoja item in itensDisponiveis)
        {
            Debug.Log($"- {item.nomeItem} | Preço: {item.preco} | Tipo: {item.tipoItem}");
        }
    }
    
    void MostrarItensNaUI()
{
    foreach (Transform filho in painelItensUI)
    {
        Destroy(filho.gameObject); // limpa UI antiga
    }

    foreach (ItemLoja item in itensDisponiveis)
    {
        GameObject novoBotao = Instantiate(prefabUIItem, painelItensUI);
        UIItemShop ui = novoBotao.GetComponent<UIItemShop>();
        if (ui != null)
        {
            ui.Configurar(item, this);
        }
    }
}
}
