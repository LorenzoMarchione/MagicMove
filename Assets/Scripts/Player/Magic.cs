using System;
using Unity.Mathematics;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public Player player;

    [SerializeField] private float castCooldown;
    private float castTimer;
    public bool canCast = true;
    public SpellSO[] spells;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        if(castTimer > 0)
        {
            castTimer -= Time.deltaTime;

            if (castTimer <= 0)
                canCast = true;
        }
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
        spells[0].Cast(player);
        canCast = false;
    }
}
