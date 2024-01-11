using System;
using System.Collections.Generic;
using UnityEngine;
public class CampaignTilesScript : MonoBehaviour
{
    public Transform unitParent;
    public List<Transform> tilesParentList;
    public TileTransform[,] tileTransform;

    public int xTileCount;
    public int yTileCount;

    private void Awake()
    {
        InstantiateTilesTransform();
    }

    public TileTransform GetNearestTile(Vector2 position)
    {
        TileTransform nearestTile = null;
        float nearestRange = float.MaxValue;
        foreach (var tile in tileTransform)
        {
            float currentRange = ((Vector2)tile.transform.position - position).magnitude;
            if (currentRange < nearestRange)
            {
                nearestRange = currentRange;
                nearestTile = tile;
            }
        }
        return nearestTile;
    }

    public void RemoveUnit(UnitBase removedUnit)
    {
        int tileSize = removedUnit.xSize * removedUnit.ySize;
        foreach (var tile in tileTransform)
        {
            if (tile.unitOnTile == removedUnit)
            {
                tile.unitOnTile = null;
                tileSize--;
                if (tileSize <= 0) break;
            }
        }
    }

    public void RemoveObject(GameObject removedObject)
    {
        foreach (var tile in tileTransform)
        {
            if (tile.objectOnTile == removedObject)
            {
                tile.objectOnTile = null;
                break;
            }
        }
    }

    public void MoveUnit(UnitBase movedUnit)
    {
        int tileSize = movedUnit.xSize * movedUnit.ySize;
        foreach (var tile in tileTransform)
        {
            /*((Vector2)tile.transform.position - movedUnit.GetUnitPosition()).magnitude < 0.4*/
            if (movedUnit.collider2D.bounds.Contains(tile.transform.position))
            {
                tile.unitOnTile = movedUnit;
                tileSize--;
                if (tileSize <= 0) break;
            }
        }
    }

    public void MoveObject(GameObject movedObject)
    {
        foreach (var tile in tileTransform)
        {
            if ((movedObject.transform.position - tile.transform.position).magnitude < 0.4f)
            {
                tile.objectOnTile = movedObject;
                break;
            }
        }
    }

    public UnitBase SpawnNewEnemy(string unitID, int xPos, int yPos)
    {
        var newUnit = EnemySpawnPool.instance.ActivateObject(unitID, tileTransform[xPos, yPos].transform, unitParent).GetComponent<UnitBase>();
        newUnit.InstantiateUnit(this);

        tileTransform[xPos, yPos].unitOnTile = newUnit;
        newUnit.transform.position = GetCenterPosition(xPos, yPos, newUnit);
        newUnit.transform.rotation = tileTransform[xPos, yPos].transform.rotation;
        newUnit.transform.localScale = tileTransform[xPos, yPos].transform.localScale;
        return newUnit;
    }

    public UnitBase SpawnNewEnemyRandomPos(string unitID)
    {
        var newUnit = EnemySpawnPool.instance.ActivateObject(unitID, tileTransform[0, 0].transform, unitParent).GetComponent<UnitBase>();
        newUnit.InstantiateUnit(this);
        int xLimit = xTileCount - (newUnit.xSize - 1);
        int yLimit = yTileCount - (newUnit.ySize - 1);
        int randomX = UnityEngine.Random.Range(6, xLimit);
        int randomY = UnityEngine.Random.Range(0, yLimit);
        //Debug.Log("Jyo" + randomX + " " + randomY);
        //tileTransform[randomX, randomY].unitOnTile != null
        int xStart = randomX;
        while (IsTilesFilled(randomX, randomY, newUnit))
        {
            if (IsTilesFilled(randomX, (randomY >= yLimit) ? 0 : randomY + 1, newUnit))
            {
                randomY++;
            }
            else if (IsTilesFilled(randomX, (randomY < 0) ? yTileCount - 1 : randomY - 1, newUnit))
            {
                randomY--;
            }
            else if (IsTilesFilled((randomX < 0) ? xTileCount - 1 : randomX - 1, randomY, newUnit))
            {
                randomX--;
            }
            else if (IsTilesFilled((randomX >= xLimit) ? 0 : randomX + 1, randomY, newUnit))
            {
                randomX++;
            }
            else
            {
                randomX++;
                if (randomX == xStart || (randomX >= xTileCount && xStart == 0)) randomY++;
            }
            if (randomX >= xTileCount) randomX = 0;
            else if (randomX < 0) randomX = xTileCount - 1;
            if (randomY >= yTileCount) randomY = 0;
            else if (randomY < 0) randomY = yTileCount - 1;

        }

        tileTransform[randomX, randomY].unitOnTile = newUnit;
        newUnit.transform.position = GetCenterPosition(randomX, randomY, newUnit);
        newUnit.transform.rotation = tileTransform[randomX, randomY].transform.rotation;
        newUnit.transform.localScale = tileTransform[randomX, randomY].transform.localScale;
        return newUnit;
    }

    public LavaTile SpawnNewLava(UnitBase summonerUnit, int xPos, int yPos)
    {
        // check if tile is empty, then get and instantiate lava , or return null
        var newTile = EnemySpawnPool.instance.ActivateObject(summonerUnit.UnitId, tileTransform[xPos, yPos].transform, unitParent).GetComponent<LavaTile>();
        newTile.InstantiateTile(this);
        return null;
    }

    void InstantiateTilesTransform()
    {
        List<float> xPosition = new List<float>();
        List<float> yPosition = new List<float>();
        tileTransform = new TileTransform[xTileCount, yTileCount];
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
                TileTransform newTile = new TileTransform() { XID = xValue, YID = yValue, transform = tile, tilesScript = this };
                //Debug.Log("jyo" + xValue + " " + yValue + " " + tile.position);
                tileTransform[xValue, yValue] = newTile;
            }
        }
        var n = xPosition.Count;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (xPosition[j] > xPosition[j + 1])
                {
                    var tempVar = xPosition[j];
                    xPosition[j] = xPosition[j + 1];
                    xPosition[j + 1] = tempVar;
                    SwapXpos(j, j + 1);
                }
        n = yPosition.Count;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (yPosition[j] > yPosition[j + 1])
                {
                    var tempVar = yPosition[j];
                    yPosition[j] = yPosition[j + 1];
                    yPosition[j + 1] = tempVar;
                    SwapYpos(j, j + 1);
                }
    }

    void SwapXpos(int xPos1, int xPos2)
    {
        for (var i = 0; i < yTileCount; i++)
        {
            var tempVar = tileTransform[xPos1, i].transform;
            tileTransform[xPos1, i].transform = tileTransform[xPos2, i].transform;
            tileTransform[xPos2, i].transform = tempVar;
        }
    }

    void SwapYpos(int yPos1, int yPos2)
    {
        for (var i = 0; i < xTileCount; i++)
        {
            var tempVar = tileTransform[i, yPos1].transform;
            tileTransform[i, yPos1].transform = tileTransform[i, yPos2].transform;
            tileTransform[i, yPos2].transform = tempVar;
        }
    }

    bool IsTilesFilled(int xPos, int yPos, UnitBase unit)
    {
        for (var i = 0; i < unit.xSize; i++)
        {
            for (var j = 0; j < unit.ySize; j++)
            {
                int xValue = xPos + i;
                int yValue = yPos + j;
                if (xValue >= xTileCount) xValue = 0;
                if (yValue >= yTileCount) yValue = 0;
                if (tileTransform[xValue, yValue].unitOnTile != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    Vector2 GetCenterPosition(int xPos, int yPos, UnitBase unit)
    {
        var totalX = 0f;
        var totalY = 0f;
        for (var i = 0; i < unit.xSize; i++)
        {
            for (var j = 0; j < unit.ySize; j++)
            {
                int xValue = xPos + i;
                int yValue = yPos + j;
                if (xValue >= xTileCount) xValue = 0;
                if (yValue >= yTileCount) yValue = 0;
                totalX += tileTransform[xValue, yValue].transform.position.x;
                totalY += tileTransform[xValue, yValue].transform.position.y;
            }
        }
        int tilesCount = unit.xSize * unit.ySize;
        var centerX = totalX / tilesCount;
        var centerY = totalY / tilesCount;
        return new Vector2(centerX, centerY);
    }
}

public class TileTransform
{
    public CampaignTilesScript tilesScript;
    public int XID;
    public int YID;
    public Transform transform;
    public UnitBase unitOnTile;
    public GameObject objectOnTile;

    public int GetXLeft()
    {
        return (XID <= 0) ? 0 : XID - 1;
    }

    public int GetXRight()
    {
        return (XID + 1 >= tilesScript.xTileCount) ? XID : XID + 1;
    }

    public int GetYDown()
    {
        return (YID <= 0) ? 0 : YID - 1;
    }

    public int GetYUp()
    {
        return (YID + 1 >= tilesScript.yTileCount) ? YID : YID + 1;
    }
}