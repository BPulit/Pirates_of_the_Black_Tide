using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TripulanteSelector : MonoBehaviour
{
    public static TripulanteSelector instance;

    [Header("Painel Principal")]
    public GameObject painelSelecao;
    public Button botaoTripulante1;
    public Button botaoTripulante2;

    [Header("Info Visual dos Botões")]
    public Image icone1;
    public TextMeshProUGUI nome1;
    public Image icone2;
    public TextMeshProUGUI nome2;
    public TextMeshProUGUI desc1;
    public TextMeshProUGUI desc2;

    [Header("Painel de Teclas")]
    public GameObject painelTeclas;
    public Button botaoQ, botaoE, botaoR, botaoF, botaoC;

    private Tripulante escolhido;
    private GameObject jogador;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        painelSelecao.SetActive(false);
        painelTeclas.SetActive(false);
    }

    public void MostrarEscolhaTripulantes(GameObject player)
    {
        jogador = player;
       Tripulante[] todos = Resources.LoadAll<Tripulante>("Tripulantes");

        List<Tripulante> lista = new List<Tripulante>();

        foreach (Tripulante t in todos)
        {
            if (!TripulanteManager.instance.PossuiTripulante(t))
            {
                lista.Add(t);
            }
        }

        // Garante que existam ao menos dois para escolher
        if (lista.Count < 2)
        {
            Debug.Log("Menos de 2 tripulantes disponíveis para escolha. Pulando seleção.");
            Time.timeScale = 1f;
            return;
        }
        Tripulante t1 = lista[Random.Range(0, lista.Count)];
        lista.Remove(t1);
        Tripulante t2 = lista[Random.Range(0, lista.Count)];

        // Configura botão 1
        icone1.sprite = t1.icone;
        nome1.text = t1.nome;
        desc1.text = t1.descricao;
        botaoTripulante1.onClick.RemoveAllListeners();
        botaoTripulante1.onClick.AddListener(() => EscolherTripulante(t1));

        // Configura botão 2
        icone2.sprite = t2.icone;
        nome2.text = t2.nome;
        desc2.text = t2.descricao;
        botaoTripulante2.onClick.RemoveAllListeners();
        botaoTripulante2.onClick.AddListener(() => EscolherTripulante(t2));

        painelSelecao.SetActive(true);
        Time.timeScale = 0f;
    }

    void EscolherTripulante(Tripulante t)
    {
        escolhido = t;
        painelSelecao.SetActive(false);
        painelTeclas.SetActive(true);

        
        ConfigurarBotaoTecla(botaoQ, TeclaAtivacao.Q);
        ConfigurarBotaoTecla(botaoE, TeclaAtivacao.E);
        ConfigurarBotaoTecla(botaoR, TeclaAtivacao.R);
        ConfigurarBotaoTecla(botaoF, TeclaAtivacao.F);
        ConfigurarBotaoTecla(botaoC, TeclaAtivacao.C);
    }

    void ConfigurarBotaoTecla(Button botao, TeclaAtivacao tecla)
    {
        botao.onClick.RemoveAllListeners();

        bool teclaDisponivel = !TripulanteManager.instance.TeclaOcupada(tecla);
        botao.interactable = teclaDisponivel;

        if (teclaDisponivel)
        {
            botao.onClick.AddListener(() => AtribuirTecla(tecla));
        }
    }


    void AtribuirTecla(TeclaAtivacao tecla)
    {
        TripulanteManager.instance.RegistrarTripulante(escolhido, tecla, jogador);
        Time.timeScale = 1f;
        painelTeclas.SetActive(false);
    }
}