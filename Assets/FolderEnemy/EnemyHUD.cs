using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHUD : MonoBehaviour
{
    public StatusEnemie status;
    public Slider slider;
    

    void Start()
    {
        slider.maxValue = status.vida;
        slider.value = status.vida;
        
    }

    void Update()
    {
        slider.value = status.GetVidaAtual();
        
    }

    
}
