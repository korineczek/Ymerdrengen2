#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    [CustomEditor(typeof(GridManager))]
    public class GridEditor : Editor
    {
        SerializedProperty gridSideLength;
        SerializedProperty gridInitializer;

        private bool[] floorTiles;
        private int sideLength { get { return gridSideLength.intValue; } }

        void OnEnable()
        {
            gridSideLength = serializedObject.FindProperty("gridSize");
            gridInitializer = serializedObject.FindProperty("FloorInitializer");
            if (floorTiles == null) {
                floorTiles = new bool[sideLength * sideLength];
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.LabelField("Grid Side Length:", sideLength.ToString());

            EditorGUILayout.LabelField("Floor Designer:");
            for (int i = 0; i < sideLength; i++) {
                EditorGUILayout.BeginHorizontal(GUILayout.MaxHeight(10), GUILayout.MaxWidth(7 * 12));
                for (int j = 0; j < sideLength; j++) {
                    floorTiles[TranslateVector(i, j)] = EditorGUILayout.Toggle(floorTiles[TranslateVector(i, j)]);
                }
                EditorGUILayout.EndHorizontal();
            }

            for (int i = 0; i < gridInitializer.arraySize; i++) {
                gridInitializer.GetArrayElementAtIndex(i).boolValue = floorTiles[i];
            }

            serializedObject.ApplyModifiedProperties();
        }

        private int TranslateVector(int x, int y)
        {
            return x + (y * sideLength);
        }
    }
}
#endif