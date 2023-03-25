using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeakerUIController : MonoBehaviour
{
    public Image portrait;

    public GameObject dialoguePanel;
    public GameObject dialoguePanelDouble;

    public Text dialogue;
    public Pose pose;

    Image chatBubbleImage;
    public Sprite singleBubble;
    public Sprite doubleBubble;

    private Character speaker;
    public Character Speaker
    {
        get { return speaker; }
        set {
            speaker = value;
        }
    }

    void Start()
    {
        chatBubbleImage = dialoguePanel.GetComponent<Image>();
    }

    void UpdateChatBubble()
    {
        if (dialogue.text.Length >= 18)
        {
            chatBubbleImage.sprite = singleBubble;
        }
        else
        {
            chatBubbleImage.sprite = doubleBubble;
        }
    }

    private void LateUpdate()
    {
        //requirements state to avoid Update loop whenever possible
        UpdateChatBubble();
    }

    public string Dialogue
    {
        get { return dialogue.text; }
        set { dialogue.text = value; }
    }

    public Pose Pose
    {
        set {
            Sprite sprite;
            if (value == Pose.Second) {
                sprite = speaker.portraitTwo;
            }
            else if(value == Pose.Third) {
                sprite = speaker.portraitThree;
            }
            else {
                sprite = speaker.portraitFirst;
            }

            portrait.sprite = sprite;
        }
    }

    public bool HasSpeaker()
    {
        return speaker != null;
    }

    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
