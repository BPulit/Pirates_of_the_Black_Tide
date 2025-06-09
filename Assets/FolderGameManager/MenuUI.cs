using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject painelMenuPrincipal;
    public GameObject painelOpcoes;
    public GameObject painelCreditos;
    public GameObject painelComoJogar;

    public void JogarPrincipal()
    {
        PlayerPrefs.DeleteKey("naviosDestruidos");
        PlayerPrefs.DeleteKey("nivel");
        PlayerPrefs.DeleteKey("bossDerrotados");
        SceneManager.LoadScene("Fase1");
    }

     public void JogarInfinito()
    {
        PlayerPrefs.DeleteKey("naviosDestruidos");
        PlayerPrefs.DeleteKey("nivel");
        PlayerPrefs.DeleteKey("bossDerrotados");
        SceneManager.LoadScene("Infinito");
    }

     public void ComoJogar()
    {
        painelMenuPrincipal.SetActive(false);
        painelComoJogar.SetActive(true);   
    }

    public void AbrirOpcoes()
    {
        painelMenuPrincipal.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void AbrirCreditos()
    {
        painelMenuPrincipal.SetActive(false);
        painelCreditos.SetActive(true);
    }

    public void VoltarMenu()
    {
        painelCreditos.SetActive(false);
        painelComoJogar.SetActive(false);
        painelOpcoes.SetActive(false);
        painelMenuPrincipal.SetActive(true);
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
