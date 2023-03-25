using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]

public class ConversationController : MonoBehaviour
{
    public Conversation conversation; //for SOLID: Dependency Inversion
    public Conversation defaultConversation;

    public RawImage fader;
    public Fader introFader;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUIController speakerUILeft;
    private SpeakerUIController speakerUIRight;

    [SerializeField] GameObject titleGameObject;

    private int activeLineIndex;
    private bool conversationStarted = false;

    public void ChangeConversation(Conversation nextConversation) {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }

    private void Start()
    {
        speakerUILeft  = speakerLeft.GetComponent<SpeakerUIController>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUIController>();
        introFader = new Fader(fader);
    }

    /*void FadeIn()
        {
        //NOTE: moved to its own standalone class to satisfy to SOLID principle: Open/Close requirement
        fader.gameObject.SetActive(true);
        fader.CrossFadeAlpha(0, 1.0f, true);
        }
    */

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (titleGameObject.activeInHierarchy)
            { titleGameObject.SetActive(false); }
            AdvanceLine();      
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            EndConversation();
    }

    private void EndConversation() {
        conversation = defaultConversation;
        conversationStarted = false;
        speakerUILeft.Hide();
        speakerUIRight.Hide();
        fader.gameObject.SetActive(false);
    }

    private void Initialize() {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }

    private void AdvanceLine() {
        if (conversation == null) return;
        if (!conversationStarted) Initialize();
        
        if (activeLineIndex < conversation.lines.Length)
            DisplayLine();
        else
            AdvanceConversation();            
    }

    private void DisplayLine() {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            speakerUILeft.portrait.transform.localScale = new Vector3(3.4f, 4.53f, 1.0f);
            speakerUIRight.portrait.transform.localScale = new Vector3(2.5f, 3.33f, 1.0f);
            SetDialogue(speakerUILeft, speakerUIRight, line);
        }
        else {
            speakerUIRight.portrait.transform.localScale = new Vector3(3.4f, 4.53f, 1.0f);
            speakerUILeft.portrait.transform.localScale = new Vector3(2.5f, 3.33f, 1.0f);
            SetDialogue(speakerUIRight, speakerUILeft, line);
        }

        activeLineIndex += 1;
    }

    private void AdvanceConversation() {
        if (conversation.nextConversation != null)
        {
            if (conversation.nextConversation.hasFadeIn)
            {
                introFader.FadeIn();
                ChangeConversation(conversation.nextConversation);
            }
            else
            {
                ChangeConversation(conversation.nextConversation);
            }
            
        }
        else
        {
            EndConversation();
        }
    }

    private void SetDialogue(
        SpeakerUIController activeSpeakerUI,
        SpeakerUIController inactiveSpeakerUI,
        Line line)
    {
        activeSpeakerUI.Show();
        activeSpeakerUI.dialoguePanel.SetActive(true);
        inactiveSpeakerUI.dialoguePanel.SetActive(false);
        
        activeSpeakerUI.Dialogue = "";
        inactiveSpeakerUI.Dialogue = "";
        activeSpeakerUI.Pose = line.pose;

        StopAllCoroutines();
        StartCoroutine(Typewriter(line.text, activeSpeakerUI));
    }

    private IEnumerator Typewriter(string text, SpeakerUIController controller) {
        foreach(char character in text.ToCharArray()) {
            controller.Dialogue += character;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
