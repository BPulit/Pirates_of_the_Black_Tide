using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoNavios;
    public TextMeshProUGUI textoBoss;

    void Start()
    {
        int nivel = PlayerPrefs.GetInt("nivel", 1);
        int navios = PlayerPrefs.GetInt("naviosDestruidos", 0);
        int boss = PlayerPrefs.GetInt("bossDerrotados", 0);

        textoNivel.text = " " + nivel;
        textoNavios.text = " " + navios;
        textoBoss.text = " " + boss;
    }
    public void VoltarAoMenu()
    {
        PlayerPrefs.DeleteKey("naviosDestruidos");
        PlayerPrefs.DeleteKey("nivel");
        PlayerPrefs.DeleteKey("bossDerrotados");
        SceneManager.LoadScene("MenuInicial");
    }
    public void JogarNovamente()
    {
        PlayerPrefs.DeleteKey("naviosDestruidos");
        PlayerPrefs.DeleteKey("nivel");
        PlayerPrefs.DeleteKey("bossDerrotados");
        SceneManager.LoadScene("Fase1");
    }

}
