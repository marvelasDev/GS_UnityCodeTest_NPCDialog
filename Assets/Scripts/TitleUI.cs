using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class TitleUI : MonoBehaviour
{
    public TextMeshProUGUI title1;
    public TextMeshProUGUI title2;

    TitleData titleData;

    private void OnGUI()
    {
        List<string> charsList = new List<string>() { titleData.Char1, titleData.Char2, titleData.Char3 };

        string format = @"{0}, {1}, and {2}";
        string expectedString = string.Format(format, charsList.ToArray());

        title2.text = expectedString;
    }

    void Start()
    {
        string json = File.ReadAllText(Application.dataPath + "/TitleDataFile.json");
        titleData = JsonUtility.FromJson<TitleData>(json);
    }
}
