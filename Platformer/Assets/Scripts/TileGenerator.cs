using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileGenerator : MonoBehaviour
{
    public GameObject grid;

    [Range(0, 100)]
    public int probability;
    [Range(1, 8)]
    public int birthLimit;
    [Range(1, 8)]
    public int deathLimit;
    [Range(1, 10)]
    public int generationCount;
    private int count = 0;

    private int[,] intMap;
    public Vector3Int tileMapSize;
    public Tilemap topMap;
    public Tilemap botMap;
    public Tile topTile;
    public Tile botTile;

    int width;
    int height;

    public void Generate(int genCount)
    {
        Clear(false);
        width = tileMapSize.x;
        height = tileMapSize.y;

        if (intMap == null)
        {
            intMap = new int[width, height];
            InitializeIntMap();
        }

        for (int i = 0; i < genCount; i++)
        {
            intMap = GenerateIntMap(intMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (intMap[x, y] == 1)
                topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
            }
        }
    }

    public void InitializeIntMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                intMap[x, y] = Random.Range(1, 101) < probability ? 1 : 0;
            }
        }
    }

    public int[,] GenerateIntMap(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighborCount;
        BoundsInt boundsInt = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighborCount = 0;
                foreach (var bound in boundsInt.allPositionsWithin)
                {
                    if (bound.x == 0 && bound.y == 0) continue;
                    if (x + bound.x >= 0 && x + bound.x < width && y + bound.y >= 0 && y + bound.y < height)
                    {
                        neighborCount += oldMap[x + bound.x, y + bound.y];
                    }
                    else
                    {
                        neighborCount++;
                    }
                }

                if (oldMap[x, y] == 1)
                {
                    newMap[x, y] = neighborCount < deathLimit ? 0 : 1;
                }

                if (oldMap[x, y] == 0)
                {
                    newMap[x, y] = neighborCount > birthLimit ? 1 : 0;
                }
            }
        }

        return newMap;
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Generate(generationCount);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Clear(true);
        }

        if (Input.GetMouseButton(2))
        {
            SaveAssetMap();
            count++;
        }
    }

    public void SaveAssetMap()
    {
        PrefabUtility.SaveAsPrefabAsset(grid, "Assets/Prefabs", out bool success);
        if (success)
            EditorUtility.DisplayDialog("Tilemap сохранен", "Файл находится в Assets/Prefabs", "ОК");
        else
            EditorUtility.DisplayDialog("Tilemap не сохранен", "Произошла ошбика", "ОК");
    }

    public void Clear(bool complete)
    {
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
        if (complete)
        {
            intMap = null;
        }
    }
}