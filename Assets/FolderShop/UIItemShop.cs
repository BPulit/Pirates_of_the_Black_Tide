using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIItemShop : MonoBehaviour
{
    public TextMeshProUGUI nomeTexto;
    public TextMeshProUGUI descTexto;
    public TextMeshProUGUI precoTexto;
    public Image iconeImagem;
    public Button botaoComprar;

    private ItemLoja item;
    private ShopManager loja;

    public void Configurar(ItemLoja novoItem, ShopManager manager)
    {
        item = novoItem;
        loja = manager;

        nomeTexto.text = item.nomeItem;
        descTexto.text = item.descricao;
        precoTexto.text = "Preço: " + item.preco;
        iconeImagem.sprite = item.icone;

        botaoComprar.onClick.AddListener(Comprar);
    }

    void Comprar()
    {
        loja.ComprarItem(item);
    }
}
