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

        // Embaralha e pega 2
        List<Tripulante> lista = new List<Tripulante>(todos);
        Tripulante t1 = lista[Random.Range(0, lista.Count)];
        lista.Remove(t1);
        Tripulante t2 = lista[Random.Range(0, lista.Count)];

        // Configura botão 1
        icone1.sprite = t1.icone;
        nome1.text = t1.nome;
        botaoTripulante1.onClick.RemoveAllListeners();
        botaoTripulante1.onClick.AddListener(() => EscolherTripulante(t1));

        // Configura botão 2
        icone2.sprite = t2.icone;
        nome2.text = t2.nome;
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

        // Configura cada botão de tecla
        botaoQ.onClick.AddListener(() => AtribuirTecla(TeclaAtivacao.Q));
        botaoE.onClick.AddListener(() => AtribuirTecla(TeclaAtivacao.E));
        botaoR.onClick.AddListener(() => AtribuirTecla(TeclaAtivacao.R));
        botaoF.onClick.AddListener(() => AtribuirTecla(TeclaAtivacao.F));
        botaoC.onClick.AddListener(() => AtribuirTecla(TeclaAtivacao.C));
    }

    void AtribuirTecla(TeclaAtivacao tecla)
    {
        TripulanteManager.instance.RegistrarTripulante(escolhido, tecla, jogador);
        Time.timeScale = 1f;
        painelTeclas.SetActive(false);
    }
}