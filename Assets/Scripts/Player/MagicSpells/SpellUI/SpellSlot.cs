using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public Image icon;
    public GameObject highlight;
    public SpellSO Spell {  get; private set; }

    [SerializeField] private Color normalColor;
    [SerializeField] private Color higlightColor = Color.white;
    private Vector3 normalScale = Vector3.one;
    private Vector3 higlightScale = Vector3.one * 1.2f;


    //asignar o borrar/desactivar hechizo, imagen de icono y activar dicho icono de spellSlot
    public void SetSpell(SpellSO spellSO)
    {
        if(spellSO != null)
        {
            Spell = spellSO;
            icon.sprite = spellSO.icon;
            icon.gameObject.SetActive(true);
        }
        else
        {
            Spell = null;
            icon.sprite = null;
            icon.gameObject.SetActive(false);
        }

        SetHighlight(false);
    }
    public void SetHighlight(bool active)
    {
        highlight.SetActive(active);
        icon.color = active ? higlightColor : normalColor;
        icon.rectTransform.localScale = active ? higlightScale : normalScale;
    }
}
