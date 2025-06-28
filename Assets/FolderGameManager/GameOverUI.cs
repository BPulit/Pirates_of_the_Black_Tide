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

        textoNivel.text = nivel.ToString();
        textoNavios.text = navios.ToString();
        textoBoss.text = boss.ToString();
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
    string cenaParaCarregar = PlayerPrefs.GetString("ultimaCenaJogada", "Fase1"); // fallback para "Fase1"
    
    PlayerPrefs.DeleteKey("naviosDestruidos");
    PlayerPrefs.DeleteKey("nivel");
    PlayerPrefs.DeleteKey("bossDerrotados");
    PlayerPrefs.DeleteKey("ultimaCenaJogada");

    SceneManager.LoadScene(cenaParaCarregar);
}


}
