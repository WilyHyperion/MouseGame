using UnityEngine;
using UnityEngine.EventSystems;

public enum OvenUIMessage
{
    Right,
    Left,
    Confirm
}
public class RecipeUIButton : MonoBehaviour, IPointerClickHandler
{
    public OvenUIMessage OnClick;
    SelectRecipe parent;
    void Start()
    {
        parent = GetComponentInParent<SelectRecipe>();
    } 
    public void OnPointerClick(PointerEventData eventData)
    {
        parent?.OnEvent(this.OnClick);
    }
}
