using System;
using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
    private LevelData currentLevel;
    private Vector2 cameraOffset = Vector2.zero;
    private float zoomFactor = 1f;
    private float minZoom = 0.5f;
    private float maxZoom = 3f;
    
    private int selectedTile = (int)LevelData.TileType.Floor;
    private float tileSize = 32f;

    private Vector2Int _tilemapSizeEditor;

    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    private void OnGUI()
    {
        Event e = Event.current;

        currentLevel = (LevelData)EditorGUILayout.ObjectField("Level Data", currentLevel, typeof(LevelData), false);
        if (currentLevel && currentLevel.sizeLevel != _tilemapSizeEditor)
        {
            _tilemapSizeEditor = currentLevel.sizeLevel;
        }
        
        if (!currentLevel)
        {
            EditorGUILayout.HelpBox("Sélectionnez un LevelData pour l'éditer.", MessageType.Warning);
            return;
        }
        GUILayout.Space(5);
        
        GUILayout.Label("Redefine size level", EditorStyles.boldLabel);
        _tilemapSizeEditor = EditorGUILayout.Vector2IntField("Size Level", _tilemapSizeEditor);
        if (_tilemapSizeEditor != currentLevel.sizeLevel)
        {
            ResizeLevel(_tilemapSizeEditor);
        }
        
        GUILayout.Space(5);
        
        selectedTile = EditorGUILayout.Popup(selectedTile, Enum.GetNames(typeof(LevelData.TileType)));
        
        GUILayout.Space(10);

        HandleCameraMovement(e);

        // Dessiner la zone de jeu
        Rect drawArea = GUILayoutUtility.GetRect(position.width, position.height - 150);
        GUI.Box(drawArea, "");  // Délimite la zone
        GUI.BeginClip(drawArea);

        Matrix4x4 originalMatrix = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(cameraOffset, Quaternion.identity, new Vector3(zoomFactor, zoomFactor, 1));

        DrawTileGrid();
        
        GUI.matrix = originalMatrix;
        GUI.EndClip();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(currentLevel);
        }
    }

    private void HandleCameraMovement(Event e)
    {
        if (e.type == EventType.ScrollWheel)
        {
            float zoomChange = -e.delta.y * 0.05f;
            zoomFactor = Mathf.Clamp(zoomFactor + zoomChange, minZoom, maxZoom);
            Repaint();
        }

        if (e.type == EventType.MouseDrag && e.button == 2)
        {
            cameraOffset += e.delta / zoomFactor;
            Repaint();
        }
    }

    private void ResizeLevel(Vector2Int sizeLevel)
    {
        LevelData.TileType[,] oldTiles = currentLevel.Tiles;
        currentLevel.Tiles = new LevelData.TileType[sizeLevel.x, sizeLevel.y];

        if (oldTiles != null)
        {
            for (int y = 0; y < Mathf.Min(sizeLevel.y, oldTiles.GetLength(1)); y++)
            {
                for (int x = 0; x < Mathf.Min(sizeLevel.x, oldTiles.GetLength(0)); x++)
                {
                    currentLevel.Tiles[x, y] = oldTiles[x, y];
                }
            }
        }

        currentLevel.sizeLevel = new Vector2Int(sizeLevel.x, sizeLevel.y);
        Repaint();
    }

    private void DrawTileGrid()
    {
        if (currentLevel.Tiles == null) return;

        for (int y = 0; y < currentLevel.sizeLevel.y; y++)
        {
            for (int x = 0; x < currentLevel.sizeLevel.x; x++)
            {
                Rect tileRect = new Rect(x * tileSize, y * tileSize, tileSize, tileSize);
                DrawTile(tileRect, currentLevel.Tiles[x, y]);

                if (tileRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {
                    if (Event.current.button == 0) // Clic gauche
                    {
                        currentLevel.Tiles[x, y] = (LevelData.TileType)selectedTile;
                        Repaint();
                    }
                }
            }
        }
    }

    private void DrawTile(Rect rect, LevelData.TileType tileType)
    {
        Color tileColor = tileType switch
        {
            LevelData.TileType.None => Color.gray,
            LevelData.TileType.Wall => Color.black,
            LevelData.TileType.Floor => Color.white,
            LevelData.TileType.Object => Color.blue,
            LevelData.TileType.Exit => Color.red,
            _ => Color.gray
        };

        EditorGUI.DrawRect(rect, tileColor);
    }
}
