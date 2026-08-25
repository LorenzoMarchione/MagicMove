using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy attacker) : base(attacker)
    {
        animName = "isAttacking1";
    }
}
