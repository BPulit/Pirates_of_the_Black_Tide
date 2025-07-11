using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITripulanteCarta : MonoBehaviour
{
    public Image icone;
    public TextMeshProUGUI nomeTripulante;
    public TextMeshProUGUI teclaTexto;
    public Slider barraCooldown;
    public Tripulante tripulante; 

    public void Configurar(Tripulante tripulante, TeclaAtivacao tecla)
{
    this.tripulante = tripulante; 

    icone.sprite = tripulante.icone;
    teclaTexto.text = tecla.ToString();
    nomeTripulante.text = tripulante.nome.ToString();
    barraCooldown.value = 0f;
    barraCooldown.gameObject.SetActive(false);
}
    
}
