using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector2(2.2f , 2.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector2(2, 2);
    }
}
