using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LevelData levelData;

    [Header("References")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase wallTileSkin;
    [SerializeField] private TileBase floorTileSkin;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GameObject exitPrefab;
    
    private void Awake()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        if (levelData == null)
        {
            Debug.LogError("LevelData is not assigned.");
            return;
        }

        tilemap.ClearAllTiles();

        for (int x = 0; x < levelData.sizeLevel.x; x++)
        {
            for (int y = 0; y < levelData.sizeLevel.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                LevelData.TileType tileType = levelData.Tiles[x, y];

                switch (tileType)
                {
                    case LevelData.TileType.Wall:
                        tilemap.SetTile(tilePosition, wallTileSkin);
                        break;
                    case LevelData.TileType.Floor:
                        tilemap.SetTile(tilePosition, floorTileSkin);
                        break;
                    case LevelData.TileType.Object:
                        tilemap.SetTile(tilePosition, floorTileSkin);
                        Instantiate(objectPrefab, tilemap.GetCellCenterWorld(tilePosition), Quaternion.identity);
                        break;
                    case LevelData.TileType.Exit:
                        tilemap.SetTile(tilePosition, floorTileSkin);
                        Instantiate(exitPrefab, tilemap.GetCellCenterWorld(tilePosition), Quaternion.identity);
                        break;
                }
            }
        }
    }
}