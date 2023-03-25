using UnityEngine.UI;

public class Fader
{
    //NOTE: moved here to satisfy SOLID principles: Open/Close and Single Resp

    private RawImage fader;

    public Fader(RawImage fader)
    {
        this.fader = fader;
    }

    public void FadeIn()
    {
        fader.gameObject.SetActive(true);
        fader.CrossFadeAlpha(0, 1.0f, true);
    }
}
