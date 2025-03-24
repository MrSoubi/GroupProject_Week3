using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TilemapData[] tilemapsData;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private GameObject pickablePrefab;

    public int pickableCount = 4;

    [Space(10)]
    [SerializeField] Tilemap fogOfWarTilemap;
    [SerializeField] Tile fogOfWarTile;

    [Space(10)]
    [SerializeField] private RSF_AccessNextTile rsfAccessNextTile;
    
    private readonly Dictionary<Vector2Int, List<TilemapData.TileType>> _tiles = new ();
    Dictionary<Vector2Int, bool> fogOfWarHasVisitedTile = new();

    [Header("Input")]
    [SerializeField] RSE_OnPlayerMove rseOnPlayerMove;

    private void OnEnable()
    {
        rsfAccessNextTile.Action += CheckAccessNextTile;
        rseOnPlayerMove.Action += CheckFogOfWar;
    }
    private void OnDisable()
    {
        rsfAccessNextTile.Action -= CheckAccessNextTile;
        rseOnPlayerMove.Action -= CheckFogOfWar;
    }

    private void Awake()
    {
        GenerateLevel();
    }

    private TilemapData.TileType CheckAccessNextTile(Vector2 nextTile)
    {
        Vector2Int nextTileInt = new Vector2Int(Mathf.FloorToInt(nextTile.x), Mathf.FloorToInt(nextTile.y));
        TilemapData.TileType currentType = TilemapData.TileType.None;

        if (_tiles.TryGetValue(nextTileInt, out var tileTypes))
        {
            foreach (var tileType in tileTypes)
            {
                if (tileType == TilemapData.TileType.Wall && currentType != TilemapData.TileType.Wall)
                    currentType = TilemapData.TileType.Wall;

                else if (tileType != TilemapData.TileType.Wall && currentType == TilemapData.TileType.Wall)
                    continue;

                else currentType = tileType;
            }
        }

        return currentType;
    }

    private void GenerateLevel()
    {
        if (tilemapsData.Length == 0)
        {
            Debug.LogError("No tilemaps data provided.");
            return;
        }
        
        _tiles.Clear();

        foreach (var tilemapData in tilemapsData)
        {
            if (!tilemapData.tilemap)
            {
                Debug.LogError($"Tilemap not assign for {tilemapData.tilemapTile}");
                continue;
            }

            foreach (var position in tilemapData.tilemap.cellBounds.allPositionsWithin)
            {
                Vector2Int posInt = (Vector2Int)position;

                if (tilemapData.tilemap.HasTile(position))
                {
                    if (!_tiles.ContainsKey(posInt))
                    {
                        _tiles[posInt] = new List<TilemapData.TileType>(){Capacity = 2};
                    }
                    _tiles[posInt].Add(tilemapData.tilemapTile);
                }

                if (!fogOfWarHasVisitedTile.ContainsKey(posInt))
                {
                    fogOfWarHasVisitedTile.Add(posInt, false);
                    fogOfWarTilemap.SetTile(position, fogOfWarTile);
                }
            }
        }

        GeneratePickables();
    }

    void CheckFogOfWar(Vector3Int posToCheck)
    {
        Vector2Int pos = (Vector2Int)posToCheck;
        if (fogOfWarHasVisitedTile.ContainsKey(pos))
        {
            fogOfWarTilemap.SetTile(posToCheck, null);
            fogOfWarHasVisitedTile[pos] = true;
        }
    }

    private void GeneratePickables()
    {
        foreach (var _ in new int[pickableCount])
        {
            GeneratePickable();
        }
    }

    private void GeneratePickable()
    {
        Vector3Int randomPosition = GenerateRandomVector3Int(floorTilemap.cellBounds.min, floorTilemap.cellBounds.max);

        while (!IsCellEmpty(randomPosition))
        {
            randomPosition = GenerateRandomVector3Int(floorTilemap.cellBounds.min, floorTilemap.cellBounds.max);
        }

        Vector3 position = (Vector3)randomPosition;
        pickablePositions.Add((Vector2Int)randomPosition);
        Instantiate(pickablePrefab, randomPosition, Quaternion.identity);
    }

    List<Vector2Int> pickablePositions = new List<Vector2Int>();

    private bool IsCellEmpty(Vector3Int position)
    {
        return (!wallTilemap.HasTile(position) && !pickablePositions.Contains((Vector2Int)position));
    }

    private Vector3Int GenerateRandomVector3Int(Vector3Int min, Vector3Int max)
    {
        int randomX = UnityEngine.Random.Range(min.x, max.x);
        int randomY = UnityEngine.Random.Range(min.y, max.y);

        return new Vector3Int(randomX, randomY, 0);
    }
}

[Serializable]
public struct TilemapData
{
    public TileType tilemapTile;
    public Tilemap tilemap;
    
    public enum TileType : byte
    {
        None,
        Wall,
        Floor,
        Interactible
    }
}