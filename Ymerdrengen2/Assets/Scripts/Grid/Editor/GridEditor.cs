#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(GridManager))]
public class GridEditor : Editor
{
    GridManager gridTarget;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        gridTarget = (GridManager)target;

        // Set the player position.
        gridTarget.PlayerPosition = EditorGUILayout.Vector2Field("Player Position", gridTarget.PlayerPosition);

        gridTarget.gridSize = EditorGUILayout.IntField("Grid Size:", gridTarget.gridSize);
        serializedObject.Update();

        CreateFloorDesigner();
        CreateYoghurtDesigner();
        CreateNewTileDesigner();

        Undo.RecordObject(target, "Changed scene's initial grid.");
    }

    private void CreateFloorDesigner()
    {
        if (gridTarget.FloorInitializer == null || gridTarget.FloorInitializer.Length != (gridTarget.gridSize * gridTarget.gridSize))
            gridTarget.FloorInitializer = new bool[gridTarget.gridSize * gridTarget.gridSize];

        // Setup Editor layout.
        EditorGUILayout.LabelField("Floor Designer:");
        for (int i = 0; i < gridTarget.gridSize; i++) {
            // Create row of toggle controls.
            EditorGUILayout.BeginHorizontal(GUILayout.MaxHeight(10), GUILayout.MaxWidth(7 * 12));
            for (int j = 0; j < gridTarget.gridSize; j++) {
                // Hook floorTiles[x,y] to the designer x,y.
                gridTarget.FloorInitializer[TranslateVector(i, j, gridTarget.gridSize)] = EditorGUILayout.Toggle(gridTarget.FloorInitializer[TranslateVector(i, j, gridTarget.gridSize)]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void CreateYoghurtDesigner()
    {
        if (gridTarget.YoghurtInitializer == null || gridTarget.YoghurtInitializer.Length != (gridTarget.gridSize * gridTarget.gridSize))
            gridTarget.YoghurtInitializer = new bool[gridTarget.gridSize * gridTarget.gridSize];

        // Setup Editor layout.
        EditorGUILayout.LabelField("Yoghurt Spawner:");
        for (int i = 0; i < gridTarget.gridSize; i++) {
            // Create row of toggle controls.
            EditorGUILayout.BeginHorizontal(GUILayout.MaxHeight(10), GUILayout.MaxWidth(7 * 12));
            for (int j = 0; j < gridTarget.gridSize; j++) {
                // Hook floorTiles[x,y] to the designer x,y.
                gridTarget.YoghurtInitializer[TranslateVector(i, j, gridTarget.gridSize)] = EditorGUILayout.Toggle(gridTarget.YoghurtInitializer[TranslateVector(i, j, gridTarget.gridSize)]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void CreateNewTileDesigner()
    {
        if (gridTarget.NewTileInitializer == null || gridTarget.NewTileInitializer.Length != (gridTarget.gridSize * gridTarget.gridSize))
            gridTarget.NewTileInitializer = new bool[gridTarget.gridSize * gridTarget.gridSize];

        // Setup Editor layout.
        EditorGUILayout.LabelField("New Tile Spawner:");
        for (int i = 0; i < gridTarget.gridSize; i++)
        {
            // Create row of toggle controls.
            EditorGUILayout.BeginHorizontal(GUILayout.MaxHeight(10), GUILayout.MaxWidth(7 * 12));
            for (int j = 0; j < gridTarget.gridSize; j++)
            {
                // Hook floorTiles[x,y] to the designer x,y.
                gridTarget.NewTileInitializer[TranslateVector(i, j, gridTarget.gridSize)] = EditorGUILayout.Toggle(gridTarget.NewTileInitializer[TranslateVector(i, j, gridTarget.gridSize)]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private int TranslateVector(int x, int y, int sideLength)
    {
        return x + (y * sideLength);
    }
}
#endif
