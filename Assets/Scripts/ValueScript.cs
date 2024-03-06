using UnityEngine;

public class ValueScript : MonoBehaviour
{
    float sfxVolume = 0.5f;
    float musicVolume = 0.5f;
    float sensivityValue = 1000f;

    void Awake()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }

        if (PlayerPrefs.HasKey("MouseSensivity"))
        {
            sensivityValue = PlayerPrefs.GetFloat("MouseSensivity");
        }
        else
        {
            PlayerPrefs.SetFloat("MouseSensivity", sensivityValue);
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
