using UnityEngine;

public enum Pose {
    First,
    Second,
    Third
}

[System.Serializable]
public struct Line
{
    public Character character;

    [TextArea(4, 5)]
    public string text;
    public Pose pose;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public bool hasFadeIn;
    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;
    public Conversation nextConversation;  
}
