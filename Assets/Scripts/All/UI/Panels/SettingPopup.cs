
using Chrono.UI;
using UnityEngine;
using UnityEngine.Audio;

public class SettingPopup : Popup
{
    [SerializeField] OnOffButton _sound;
    [SerializeField] AudioMixer Mixer;

    bool isSoundOn;

    public override void Init()
    {
        base.Init();

        isSoundOn = SaveHandler.LoadValue("soundOnOff", true);

        SetSound(isSoundOn);
    }

    public override void OpenPopup()
    {
        base.OpenPopup();
    }

    public override void ClosePopup()
    {
        base.ClosePopup();
    }

    public void SwitchSound()
    {
        isSoundOn = !isSoundOn;
        SetSound(isSoundOn);
    }

    void SetSound(bool OnOff)
    {
        if (OnOff)
        {
            _sound.SetOn();
            Mixer.SetFloat("SoundVolume", 0);
            SaveHandler.SaveValue("soundOnOff", true);
        }
        else
        {
            _sound.SetOff();
            Mixer.SetFloat("SoundVolume", -80);
            SaveHandler.SaveValue("soundOnOff", false);
        }
    }

    public void ResetGame()
    {
        ClosePopup();

        GameManager.Instance.UpdateStateToEnd();
    }
}
