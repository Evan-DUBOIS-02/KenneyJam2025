using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public AudioMixer myMixer;
    public Slider musicSlider;

    public Slider soundSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void SetSoundVolume()
    {
        float sound = soundSlider.value;
        myMixer.SetFloat("sound", Mathf.Log10(sound) * 20);
    }
    
    
}
