using UnityEngine;
using UnityEngine.Rendering;

public class ValueScript : MonoBehaviour
{
    float sfxVolume;
    float musicVolume;
    float sensivityValue;

    void Start()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("SFXVolume", 0.3f);
            sfxVolume = 0.3f;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.3f);
            musicVolume = 0.3f;
        }

        if (PlayerPrefs.HasKey("MouseSensivity"))
        {
            sensivityValue = PlayerPrefs.GetFloat("MouseSensivity");
        }
        else
        {
            PlayerPrefs.SetFloat("MouseSensivity", 500f);
            sensivityValue = 500f;
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SetMouseSensivity(float value)
    {
        sensivityValue = value; 
        PlayerPrefs.SetFloat("MouseSensivity", sensivityValue);
    }

    public float GetMouseSensivity()
    {
        return PlayerPrefs.GetFloat("MouseSensivity");
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume");
    }

    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume");
    }
}
