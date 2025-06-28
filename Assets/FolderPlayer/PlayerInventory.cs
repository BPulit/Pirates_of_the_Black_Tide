using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("Itens Possuídos")]
    public List<ItemLoja> ownedItems = new List<ItemLoja>();
    public List<GameObject> velasVisuais = new(); // Atribuir no Inspetor
    private int nivelVelocidade = 0;

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
        // Só aumenta se ainda houver velas para mostrar
        if (nivelVelocidade < velasVisuais.Count)
        {
            StatusPlayer.Instance.velocidade += valor;
            nivelVelocidade++; // Aumenta nível
            AtualizarVelasVisuais();
        }
        else
        {
            Debug.Log("Velocidade já está no nível máximo de velas.");
        }
    }

    public void AumentarAtaque(int valor)
    {
        StatusPlayer.Instance.ataque += valor;
        MensagemUI.instance.MostrarMensagem($"Ataque aumentado em {valor}. Novo ataque: {StatusPlayer.Instance.ataque}");
    }
   private void AtualizarVelasVisuais()
    {
        for (int i = 0; i < velasVisuais.Count; i++)
        {
            velasVisuais[i].SetActive(i < nivelVelocidade);
        }
    }


    public int GetGold()
    {
        return CurrencyManager.instance.GetMoedas();
    }
}
