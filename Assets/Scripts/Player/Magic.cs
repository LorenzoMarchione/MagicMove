using System.Collections.Generic;
using System;
using Unity.Mathematics;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [Header("References")]
    public Player player;
    public SpellUIManager spellUIManager;

    [Header("Spells")]
    [SerializeField]private List<SpellSO> availableSpells;
    public SpellSO CurrentSpell => availableSpells.Count > 0 ? availableSpells[index] : null;
    [SerializeField]private int index = 0;

    [SerializeField] private float castCooldown;
    private float castTimer;
    public bool timeCast = true;
    public bool canCast = false;
    public bool hasCast = false;


    private void Start()
    {
        player = GetComponent<Player>();

        spellUIManager.ShowSpellSlots(availableSpells);
        HighlightCurrentSpell();
    }
    private void Update()
    {
        hasCast = CurrentSpell != null;
        if(castTimer > 0)
        {
            castTimer -= Time.deltaTime;

            if (castTimer <= 0)
                timeCast = true;
        }
        canCast = hasCast && timeCast;
    }
    public void OnSpellAnimationFinished()
    {
        castTimer = castCooldown;
        CastSpell();
        player.AnimationFinished();
    }
    private void CastSpell()
    {
        if (!canCast)
            return;
        CurrentSpell.Cast(player);
        canCast = false;
    }
    public void NextSpell()
    {
        if (availableSpells.Count == 0)
            return;
        index++;
        if (index >= availableSpells.Count)
            index = 0;
        spellUIManager.HighlightSpell(CurrentSpell);
    }
    public void PreviousSpell()
    {
        if (availableSpells.Count == 0)
            return;
        index--;
        if (index < 0)
            index = availableSpells.Count - 1;
        spellUIManager.HighlightSpell(CurrentSpell);
    }
    private void HighlightCurrentSpell()
    {
        spellUIManager.HighlightSpell(CurrentSpell);
    }
}
