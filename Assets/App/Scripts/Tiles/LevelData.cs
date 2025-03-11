using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "SSO/_/LevelData")]
public class LevelData : ScriptableObject
{
    public Vector2Int sizeLevel;
    public TileType[,] Tiles;
    public Vector2Int[] objects;
    public Vector2Int exit;
    
    public enum TileType : byte
    {
        None,
        Wall,
        Floor,
        Object,
        Exit
    }
}