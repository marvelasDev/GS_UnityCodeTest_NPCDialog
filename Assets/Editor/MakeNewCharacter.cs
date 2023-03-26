using UnityEditor;
using UnityEngine;

public class MakeNewCharacter
{
	[MenuItem("GravyStack/Make New Character")]
	public static void CreateMyAsset()
	{
		Character asset = ScriptableObject.CreateInstance<Character>();

		AssetDatabase.CreateAsset(asset, "Assets/Characters/NewCharacterData.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;
	}
}
