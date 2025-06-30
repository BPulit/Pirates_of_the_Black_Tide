using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudioFeedback : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public int somHoverID = 19;  // ID do som ao passar o mouse
    public int somClickID = 20;  // ID do som ao clicar

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.TocarSomEfeito(somHoverID);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.TocarSomEfeito(somClickID);
    }
}
