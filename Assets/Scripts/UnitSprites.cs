using UnityEngine;

[CreateAssetMenu(fileName = "UnitSprites", menuName = "UnitSprites", order = 0)]
public class UnitSprites : ScriptableObject
{
    public Sprite[] IdleSprites;
    public Sprite[] MoveSprites;
    public Sprite[] AttackSprites;
}
