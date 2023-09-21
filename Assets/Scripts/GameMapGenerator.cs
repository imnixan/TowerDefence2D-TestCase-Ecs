using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpPathFinding.cs;
using Leopotam.Ecs;

public class GameMapGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform obstacles,
        towers;

    private const int ObsPercent = 5;

    [SerializeField]
    private GameObject obstaclePref;

    [SerializeField]
    private Sprite[] obstaclesSprites;

    private StaticData staticData;

    public void GenerateMap(StaticData staticData)
    {
        this.staticData = staticData;
        BaseGrid field = new StaticGrid(staticData.FieldSize.x, staticData.FieldSize.y);
        SetAllWalkable(field);
        staticData.Field = field;

        DiagonalMovement iDiagonalMovement = DiagonalMovement.OnlyWhenNoObstacles;
        EndNodeUnWalkableTreatment iAllowEndNodeUnWalkable = EndNodeUnWalkableTreatment.ALLOW;
        JumpPointParam pathSearcher = new JumpPointParam(
            field,
            iAllowEndNodeUnWalkable,
            iDiagonalMovement
        );

        CreateObstacles(staticData, field);
        staticData.PathSearchingParams = pathSearcher;
        staticData.towers = towers;
    }

    private void SetAllWalkable(BaseGrid field)
    {
        for (int x = 0; x < staticData.FieldSize.x; x++)
        {
            for (int y = 0; y < staticData.FieldSize.y; y++)
            {
                field.SetWalkableAt(new GridPos(x, y), true);
            }
        }
    }

    private void CreateObstacles(StaticData staticData, BaseGrid field)
    {
        int spawnChanse;
        GridPos point = new GridPos();
        for (int x = 0; x < staticData.FieldSize.x; x++)
        {
            for (int y = 0; y < staticData.FieldSize.y; y++)
            {
                spawnChanse = Random.Range(0, 100);
                point.x = x;
                point.y = y;
                if (spawnChanse < ObsPercent)
                {
                    Instantiate(obstaclePref, point.ConvertToWorld(), new Quaternion(), obstacles)
                        .GetComponent<SpriteRenderer>()
                        .sprite = obstaclesSprites[Random.Range(0, obstaclesSprites.Length)];
                    field.SetWalkableAt(new GridPos(x, y), false);
                }
            }
        }
    }
}
