using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData", order = 0)]
public class UnitData : ScriptableObject
{
    public float HP;
    public float Damage;
    public float AttackRange;
    public float RechargeTime;
    public float Speed;
    public bool Ranged;
    public float ProjectilesSpeed;
}
