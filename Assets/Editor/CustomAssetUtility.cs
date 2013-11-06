using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class CustomAssetUtility : MonoBehaviour {

	// MADE BY http://www.jacobpennock.com/
	// REFER TO http://www.jacobpennock.com/Blog/?p=670
	public static void CreateAsset<T> () where T : ScriptableObject 
	{
		T asset = ScriptableObject.CreateInstance<T> ();

        string path = AssetDatabase.GetAssetPath (Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension (path) != "")
        {
            path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");
        AssetDatabase.CreateAsset (asset, assetPathAndName);
        AssetDatabase.SaveAssets ();
        EditorUtility.FocusProjectWindow ();
        Selection.activeObject = asset;
	}
	
}
