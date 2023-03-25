using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JsonCharacterNamer : MonoBehaviour
{
    // SOLID: Single Resp

    public InputField char1InputField;
    public InputField char2InputField;
    public InputField char3InputField;

    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = new SceneLoader();
        LoadFromJson();
    }

    public void SaveToJson()
    {
        TitleData data = new TitleData();
        data.Char1 = char1InputField.text;
        data.Char2 = char2InputField.text;
        data.Char3 = char3InputField.text;

        string json = JsonUtility.ToJson(data, true); //second param is Pretty Print
        File.WriteAllText(Application.dataPath + "/TitleDataFile.json", json);

        // SOLID: Open/Close
        sceneLoader.LoadNextScene();
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/TitleDataFile.json");
        TitleData data = JsonUtility.FromJson<TitleData>(json);

        char1InputField.text = data.Char1;
        char2InputField.text = data.Char2;
        char3InputField.text = data.Char3;
    }
}
