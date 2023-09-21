using UnityEngine;

struct ObjectComponent
{
    public GameObject ObGo;
    public SpriteRenderer ObSr;
    public Transform ObTransform;
    public StaticData.UnitType UnitType;
}

struct Health
{
    public float HP;
}

struct DamageRecieveMarker
{
    public float Damage;
}
