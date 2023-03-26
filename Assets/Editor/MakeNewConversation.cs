using UnityEditor;
using UnityEngine;

public class MakeNewConversation
{
	[MenuItem("GravyStack/Make New Conversation")]
	public static void CreateMyAsset()
	{
		Conversation asset = ScriptableObject.CreateInstance<Conversation>();

		AssetDatabase.CreateAsset(asset, "Assets/Conversations/NewConversationData.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;
	}
}
