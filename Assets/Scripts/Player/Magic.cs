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

    private Dictionary<SpellSO, float> spellCooldowns = new Dictionary<SpellSO, float>();

    [SerializeField]private int index = 0;



    private void Start()
    {
        player = GetComponent<Player>();

        spellUIManager.ShowSpellSlots(availableSpells);
        HighlightCurrentSpell();
    }
    private void Update()
    {
    }
    public void OnSpellAnimationFinished()
    {
        CastSpell();
        player.AnimationFinished();
    }
    private void CastSpell()
    {
        if (!CanCast(CurrentSpell) || CurrentSpell == null)
            return;
        CurrentSpell.Cast(player);
        spellCooldowns[CurrentSpell] = Time.time + CurrentSpell.cooldown;
        spellUIManager.StartSlotCooldoown(CurrentSpell, CurrentSpell.cooldown);
    }
    public bool CanCast(SpellSO spell)
    {
        return Time.time >= spellCooldowns[spell];
    }
    public void NextSpell()
    {
        if (availableSpells.Count == 0)
            return;
        index++;
        if (index >= availableSpells.Count)
            index = 0;
        HighlightCurrentSpell();
    }
    public void PreviousSpell()
    {
        if (availableSpells.Count == 0)
            return;
        index--;
        if (index < 0)
            index = availableSpells.Count - 1;
        HighlightCurrentSpell();
    }
    private void HighlightCurrentSpell()
    {
        spellUIManager.HighlightSpell(CurrentSpell);
    }
    public void LearnSpell(SpellSO spell)
    {
        if(!availableSpells.Contains(spell))
            availableSpells.Add(spell);
        spellUIManager.ShowSpellSlots(availableSpells);
        if(!spellCooldowns.ContainsKey(spell))
            spellCooldowns.Add(spell, 0);
        HighlightCurrentSpell();
    }
}
