#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

public class EnemyEditor {

    [MenuItem("Assets/Create/Spawn Pattern")]
    public static void CreateMyAsset()
    {
        SpawnPattern asset = ScriptableObject.CreateInstance<SpawnPattern>();  //scriptable object

        string path = AssetDatabase.GetAssetPath( Selection.activeObject );
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + asset.GetType().ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        Selection.activeObject = asset;        
    }

}
#endif
