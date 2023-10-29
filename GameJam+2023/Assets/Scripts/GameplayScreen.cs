using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayScreen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameInput.Instance.UpdatePointerGameplay(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameInput.Instance.UpdatePointerGameplay(false);
    }
}