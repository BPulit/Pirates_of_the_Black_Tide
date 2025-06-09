using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("Itens Possuídos")]
    public List<ItemLoja> ownedItems = new List<ItemLoja>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Adiciona o item comprado ao inventário
    public void AddItem(ItemLoja item)
    {
        ownedItems.Add(item);
        item.AplicarEfeito(); // Chama o método que aplica o efeito diretamente no jogador
    }

    // Aplicações de efeitos de item
    public void CurarJogador(int quantidade)
    {
        StatusPlayer.Instance.Curar(quantidade);
        MensagemUI.instance.MostrarMensagem($"Curado em {quantidade} pontos.");
    }

    public void AumentarVelocidade(float valor)
    {
        StatusPlayer.Instance.velocidade += valor;
        MensagemUI.instance.MostrarMensagem($"Velocidade aumentada em {valor}. Nova velocidade: {StatusPlayer.Instance.velocidade}");
    }

    public void AumentarAtaque(int valor)
    {
        StatusPlayer.Instance.ataque += valor;
        MensagemUI.instance.MostrarMensagem($"Ataque aumentado em {valor}. Novo ataque: {StatusPlayer.Instance.ataque}");
    }

    public int GetGold()
    {
        return CurrencyManager.instance.GetMoedas();
    }
}
