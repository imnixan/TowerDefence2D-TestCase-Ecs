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

    private List<GridPos> walkableList = new List<GridPos>();
    private StaticData staticData;

    public void GenerateMap(StaticData staticData)
    {
        this.staticData = staticData;
        SetAllWalkable();
        CreateObstacles(staticData);
        SetUnwalkable();
        BaseGrid field = new DynamicGrid(walkableList);
        staticData.Field = field;

        DiagonalMovement iDiagonalMovement = DiagonalMovement.OnlyWhenNoObstacles;
        EndNodeUnWalkableTreatment iAllowEndNodeUnWalkable = EndNodeUnWalkableTreatment.ALLOW;
        JumpPointParam pathSearcher = new JumpPointParam(
            field,
            iAllowEndNodeUnWalkable,
            iDiagonalMovement
        );

        staticData.PathSearchingParams = pathSearcher;
        staticData.towers = towers;
    }

    private void SetAllWalkable()
    {
        for (int x = 0; x < staticData.FieldSize.x; x++)
        {
            for (int y = 0; y < staticData.FieldSize.y; y++)
            {
                walkableList.Add(new GridPos(x, y));
            }
        }
    }

    private void SetUnwalkable()
    {
        foreach (Transform obstacle in obstacles)
        {
            walkableList.Remove(obstacle.position.ConvertToNav());
        }
    }

    private void CreateObstacles(StaticData staticData)
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
                }
            }
        }
    }
}
