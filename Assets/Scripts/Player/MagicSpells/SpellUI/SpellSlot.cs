using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    [Header("References")]
    public Image icon;
    public Image cooldownOverlay;
    public GameObject highlight;
    public TMP_Text text;
    public SpellSO Spell {  get; private set; }

    [Header("Slot Settings")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color higlightColor = Color.white;
    [SerializeField] private float popScale = 1.3f;
    [SerializeField] private float popDuration = 0.20f;
    private Vector3 normalScale = Vector3.one;
    private Vector3 higlightScale = Vector3.one * 1.2f;


    //asignar o borrar/desactivar hechizo, imagen de icono y activar dicho icono de spellSlot
    public void SetSpell(SpellSO spellSO)
    {
        if(spellSO != null)
        {
            Spell = spellSO;
            icon.sprite = spellSO.icon;
            cooldownOverlay.sprite = spellSO.icon;
            text.text = Spell.itemName;
            cooldownOverlay.fillAmount = 0;
            icon.gameObject.SetActive(true);
            cooldownOverlay.gameObject.SetActive(true);
        }
        else
        {
            Spell = null;
            icon.sprite = null;
            cooldownOverlay.sprite = null;
            text.text = "";
            icon.gameObject.SetActive(false);
            cooldownOverlay.gameObject.SetActive(false);
        }

        SetHighlight(false);
    }
    public void SetHighlight(bool active)
    {
        highlight.SetActive(active);
        icon.color = active ? higlightColor : normalColor;
        icon.rectTransform.localScale = active ? higlightScale : normalScale;
        cooldownOverlay.rectTransform.localScale = active ? higlightScale : normalScale;
    }
    public void StartCooldown(float time)
    {
        StartCoroutine(CooldownProgress(time));
    }
    //make a radial cooldown progress bar on spell icon
    private IEnumerator CooldownProgress(float time)
    {
        cooldownOverlay.fillAmount = 1;
        float remainingTime = time;
        float fillPercentage  = remainingTime / time;

        while(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            fillPercentage = remainingTime / time;
            cooldownOverlay.fillAmount = fillPercentage;
            yield return null;
        }
        cooldownOverlay.fillAmount = 0;
        yield return StartCoroutine(CooldownFinish());
    }
    //make a pop effect to spell icon changing scale
    private IEnumerator CooldownFinish()
    {
        float halfway = popDuration / 2;
        float timer = 0;
        while(timer < halfway)
        {
            float popTransision = Mathf.Lerp(1, popScale, timer/halfway);
            icon.rectTransform.localScale = normalScale * popTransision;
            timer += Time.deltaTime;
            yield return null;
        }
        icon.rectTransform.localScale = normalScale * popScale;
        timer = halfway;
        while(timer > 0)
        {
            float popTransision = Mathf.Lerp(1, popScale, timer / halfway);
            icon.rectTransform.localScale = normalScale * popTransision;
            timer -= Time.deltaTime;
            yield return null;
        }
        icon.rectTransform.localScale = normalScale;
    }
}
