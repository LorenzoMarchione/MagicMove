using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float meleeRange = 1f;
    [SerializeField] private float rangedRange;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private int rangedDamage = 10;
    public float MeleeRange { get => meleeRange; }
    public float RangedRange { get => rangedRange; }

}
