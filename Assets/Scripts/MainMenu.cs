using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource menuMusic;
    [SerializeField] Slider sensivitySlider, sfxSlider, musicSlider;
    [SerializeField] TextMeshProUGUI sensivityValueText, sfxVolumeText, musicVolumeText;
    [SerializeField] GameObject Menu, Settings, Controllers;

    ValueScript valueScript;

    private void Start()
    {
        valueScript = GetComponent<ValueScript>();

        sensivitySlider.value = valueScript.GetMouseSensivity();
        sfxSlider.value = valueScript.GetSFXVolume();
        musicSlider.value = valueScript.GetMusicVolume();

        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        Sensivity();
        SFX();
        Music();
    }

    void Sensivity()
    {
        sensivityValueText.text = (sensivitySlider.value / 1000).ToString("N2");
        valueScript.SetMouseSensivity(sensivitySlider.value);
    }

    void SFX()
    {
        sfxVolumeText.text = Mathf.Round(sfxSlider.value * 100).ToString();
        valueScript.SetSFXVolume(sfxSlider.value);
    }

    void Music()
    {
        menuMusic.volume = musicSlider.value;
        musicVolumeText.text = Mathf.Round(musicSlider.value * 100).ToString();
        valueScript.SetMusicVolume(musicSlider.value);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ControllersButton()
    {
        Menu.SetActive(false);
        Settings.SetActive(false);
        Controllers.SetActive(true);
    }

    public void SettingsButton()
    {
        Menu.SetActive(false);
        Controllers.SetActive(false);
        Settings.SetActive(true);
    }

    public void GoToMenuButton()
    {
        Controllers.SetActive(false);
        Settings.SetActive(false);
        Menu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    
}
