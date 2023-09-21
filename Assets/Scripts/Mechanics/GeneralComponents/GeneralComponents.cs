using UnityEngine;

struct ObjectComponent
{
    public GameObject ObGo;
    public SpriteRenderer ObSr;
    public Transform ObTransform;
}

struct Health
{
    public float HP;
}

struct DamageRecieveMarker
{
    public float Damage;
}
