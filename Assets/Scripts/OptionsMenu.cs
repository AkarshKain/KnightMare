using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Text MusicText;
    public Slider MusicSlider;
    public Image MusicColor;

    public Color MinColor;
    public Color MaxColor;

    public MenuScene Overmind;

    private void OnEnable()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("Music", 1f);
        SoundsSlider.value = PlayerPrefs.GetFloat("Sounds", 1f);
        RefreshMusic();
        RefreshSounds();
    }

    public void RefreshMusic()
    {
        MusicText.text = Mathf.Floor(MusicSlider.value * 100) + "%";
        MusicColor.color = Color.Lerp(MinColor, MaxColor, MusicSlider.value);
        PlayerPrefs.SetFloat("Music", MusicSlider.value);
        PlayerPrefs.Save();
        Overmind.mixer.SetFloat("Music", -80+PlayerPrefs.GetFloat("Music", 1f)*80);
    }

    public Text SoundsText;
    public Slider SoundsSlider;
    public Image SoundsColor;

    public void RefreshSounds()
    {
        SoundsText.text = Mathf.Floor(SoundsSlider.value * 100) + "%";
        SoundsColor.color = Color.Lerp(MinColor, MaxColor, SoundsSlider.value);
        PlayerPrefs.SetFloat("Sounds", SoundsSlider.value);
        PlayerPrefs.Save();
        Overmind.mixer.SetFloat("Sounds", -80+PlayerPrefs.GetFloat("Sounds", 1f) * 80);
    }
}
