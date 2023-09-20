using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpPathFinding.cs;

public class GameMapGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform obstacles;

    private const int ObsPercent = 5;

    [SerializeField]
    private Vector2Int fieldSize;

    [SerializeField]
    private GameObject obstaclePref;

    [SerializeField]
    private Sprite[] obstaclesSprites;

    public void GenerateMap(StaticData staticData)
    {
        BaseGrid field;

        field = new StaticGrid(fieldSize.x, fieldSize.y);

        CreateObstacles(field);

        DiagonalMovement iDiagonalMovement = DiagonalMovement.OnlyWhenNoObstacles;
        EndNodeUnWalkableTreatment iAllowEndNodeUnWalkable = EndNodeUnWalkableTreatment.ALLOW;
        JumpPointParam pathSearcher = new JumpPointParam(
            field,
            iAllowEndNodeUnWalkable,
            iDiagonalMovement
        );

        staticData.fieldSize = fieldSize;
        staticData.field = field;
        staticData.pathSearchingParams = pathSearcher;
    }

    private void CreateObstacles(BaseGrid field)
    {
        int spawnChanse;
        GridPos point = new GridPos();
        for (int x = 0; x < fieldSize.x; x++)
        {
            for (int y = 0; y < fieldSize.y; y++)
            {
                spawnChanse = Random.Range(0, 100);
                point.x = x;
                point.y = y;
                if (spawnChanse < ObsPercent)
                {
                    Debug.Log(spawnChanse);
                    Instantiate(obstaclePref, point.ConvertToWorld(), new Quaternion(), obstacles)
                        .GetComponent<SpriteRenderer>()
                        .sprite = obstaclesSprites[Random.Range(0, obstaclesSprites.Length)];
                }
                else
                {
                    field.SetWalkableAt(point, true);
                }
            }
        }
    }
}
