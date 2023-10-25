using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }
    public EnemySpawnPool spawnPool;
    public List<Transform> tilesParentList;
    public TileTransform[,] tileTransform;

    public int xSize;
    public int ySize;

    bool spawnTag;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        InstantiateTilesTransform();
    }

    private void Start()
    {
        BattleSystem.Instance.OnPlayerTurn += OnPlayerTurn;
        BattleSystem.Instance.OnUnitTurn += OnUnitTurn;
    }

    private void OnDestroy()
    {
        BattleSystem.Instance.OnPlayerTurn -= OnPlayerTurn;
        BattleSystem.Instance.OnUnitTurn -= OnUnitTurn;
    }

    public void RemoveUnit(UnitBase movedUnit)
    {
        foreach (var tile in tileTransform)
        {
            if (tile.unitOnTile == movedUnit)
            {
                tile.unitOnTile = null;
                break;
            }
        }
    }

    public void MoveUnit(UnitBase movedUnit)
    {
        foreach (var tile in tileTransform)
        {
            if (((Vector2)tile.transform.position - movedUnit.GetUnitPosition()).magnitude < 0.4)
            {
                tile.unitOnTile = movedUnit;
                break;
            }
        }
    }

    public UnitBase SpawnNewEnemy()
    {
        int randomX = UnityEngine.Random.Range(0, xSize);
        int randomY = UnityEngine.Random.Range(0, ySize);
        //Debug.Log("Jyo" + randomX + " " + randomY);
        while (tileTransform[randomX, randomY].unitOnTile != null)
        {
            if (tileTransform[(randomX >= xSize) ? 0: randomX + 1, randomY].unitOnTile == null)
            {
                randomX++;
            }
            else if (tileTransform[randomX, (randomY >= ySize)? 0: randomY + 1].unitOnTile == null)
            {
                randomY++;
            }
            else if (tileTransform[randomX, (randomY < 0)? ySize - 1: randomY - 1].unitOnTile == null)
            {
                randomY--;
            }
            else if (tileTransform[(randomX < 0)? xSize - 1:randomX - 1, randomY].unitOnTile == null)
            {
                randomX--;
            }
            if (randomX >= xSize) randomX = 0;
            else if (randomX < 0) randomX = xSize - 1;
            if (randomY >= ySize) randomY = 0;
            else if (randomY < 0) randomY = ySize - 1;
        }
        var newUnit = spawnPool.ActivateObject("burger", tileTransform[randomX, randomY].transform).GetComponent<UnitBase>();
        tileTransform[randomX, randomY].unitOnTile = newUnit;
        return newUnit;
    }

    void OnPlayerTurn(Action onActionComplete)
    {
        if (spawnTag)
        {
            UnitBattleHandler.Instance.AddNewUnit();
            spawnTag = false;
        }
    }

    void OnUnitTurn(Action onActionComplete)
    {
        spawnTag = true;
    }

    void InstantiateTilesTransform()
    {
        List<float> xPosition = new List<float>();
        List<float> yPosition = new List<float>();
        tileTransform = new TileTransform[xSize, ySize];
        foreach (var tileParent in tilesParentList)
        {
            foreach (Transform tile in tileParent)
            {
                int xValue = 0;
                for (var i = 0; i < xPosition.Count; i++)
                {
                    if (Mathf.Abs(tile.position.x - xPosition[i]) < 0.4f)
                    {
                        xValue = i;
                        break;
                    }
                    else
                        xValue++;
                }
                if (xValue >= xPosition.Count)
                    xPosition.Add(tile.position.x);
                int yValue = 0;
                for (var i = 0; i < yPosition.Count; i++)
                {
                    if (Mathf.Abs(tile.position.y - yPosition[i]) < 0.4f)
                    {
                        yValue = i;
                        break;
                    }
                    else
                        yValue++;
                }
                if (yValue >= yPosition.Count)
                    yPosition.Add(tile.position.y);
                TileTransform newTile = new TileTransform() { XID = xValue, YID = yValue, transform = tile };
                //Debug.Log("jyo" + xValue + " " + yValue + " " + tile.position);
                tileTransform[xValue, yValue] = newTile;
            }
        }
    }

}

public class TileTransform
{
    public int XID;
    public int YID;
    public Transform transform;
    public UnitBase unitOnTile;
}