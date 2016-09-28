#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridEditor : Editor
{
    GridManager gridTarget;

    public override void OnInspectorGUI()
    {
        gridTarget = (GridManager)target;

        gridTarget.gridSize = EditorGUILayout.IntField("Grid Size:", gridTarget.gridSize);

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

    private int TranslateVector(int x, int y, int sideLength)
    {
        return x + (y * sideLength);
    }
}
#endif
