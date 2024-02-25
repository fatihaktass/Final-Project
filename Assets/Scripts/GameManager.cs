using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject objectInteract;
    [SerializeField] GameObject door;
    [SerializeField] GameObject messagePanel;
    [SerializeField] TextMeshProUGUI messageTMP;
    [SerializeField] TextMeshProUGUI interactTMP;
    [SerializeField] GameObject[] cameras;
    [SerializeField] GameObject escMenu, escMenuButtons, settings;

    [SerializeField] AudioSource[] paperSFX;
    [SerializeField] AudioSource[] musics;
    [SerializeField] AudioSource[] sfxs;

    [SerializeField] Slider sensivitySlider, sfxSlider, musicSlider;
    [SerializeField] Slider bossHealthSlider, playerHealthSlider;
    [SerializeField] TextMeshProUGUI sensivityValueText, sfxVolumeText, musicVolumeText;

    bool escMenuOpen = false;

    PlayerController playerController;
    MouseInput mouseInput;
    ValueScript valueScript;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        mouseInput = FindAnyObjectByType<MouseInput>();
        valueScript = GetComponent<ValueScript>();

        sensivitySlider.value = valueScript.GetMouseSensivity();
        sfxSlider.value = valueScript.GetSFXVolume();
        musicSlider.value = valueScript.GetMusicVolume();

        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscMenu();
        }

        Sensivity();
        SFX();
        Music();
    }

    void Sensivity()
    {
        mouseInput.mouseSensivity = sensivitySlider.value;
        sensivityValueText.text = (sensivitySlider.value / 1000).ToString("N2");
        valueScript.SetMouseSensivity(sensivitySlider.value);
    }

    void SFX()
    {
        foreach(AudioSource sfx in sfxs) { sfx.volume = sfxSlider.value; }
        sfxVolumeText.text = Mathf.Round(sfxSlider.value * 100).ToString();
        valueScript.SetSFXVolume(sfxSlider.value);
    }

    void Music()
    {
        foreach (AudioSource music in musics) { music.volume = musicSlider.value; }
        musicVolumeText.text = Mathf.Round(musicSlider.value * 100).ToString();
        valueScript.SetMusicVolume(musicSlider.value);
    }

    public void ShowMessage(string Messages)
    {
        messagePanel.SetActive(true);
        messageTMP.text = Messages;
    }

    public void PlayerActions(bool actionPerm)
    {
        playerController.actionPermission = actionPerm;
        mouseInput.actionPermission = actionPerm;
        if (actionPerm)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ObjectInteract(string text, bool inInteraction)
    {
        if (inInteraction)
        {
            objectInteract.SetActive(true);
            interactTMP.text = text;
        }
        else
        {
            objectInteract.SetActive(false);
        }
    }

    public void CameraChanger(bool triggeredDoor)
    {
        if (!triggeredDoor)
        {
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
            PlayerActions(true);
            door.GetComponent<Animator>().SetTrigger("Triggered");
        }
        if (triggeredDoor)
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
            PlayerActions(false);
        }
    }

    public void PaperSFX(bool isOpening)
    {
        if (isOpening)
        {
            paperSFX[0].Play();
            messagePanel.SetActive(true);
        }
        else
        {
            paperSFX[1].Play();
            messagePanel.SetActive(false);
        }
    }

    public void EscMenu()
    {
        escMenuOpen = !escMenuOpen;
        if (escMenuOpen)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            escMenu.SetActive(true);
        }
        if (!escMenuOpen)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            escMenu.SetActive(false);
            settings.SetActive(false);
            escMenuButtons.SetActive(true);

        }
    }

    public void SettingsButton(bool open)
    {
        if (open)
        {
            escMenuButtons.SetActive(false);
            settings.SetActive(true);
        }
        else
        {
            settings.SetActive(false);
            escMenuButtons.SetActive(true);
        }
    }

    public void GoToMenu()
    {
        if (escMenuOpen)
        {
            EscMenu();
        }
        SceneManager.LoadScene("Menu");
    }

    public void ChangeMusic(int musicIndex)
    {
        switch (musicIndex)
        {
            case 0:
                musics[0].Play();
                break;
            case 1:
                musics[0].Stop();
                musics[1].Play();
                break;
            case 2:
                musics[0].Stop();
                musics[1].Stop();
                musics[2].Play();
                break;
            case 3:
                musics[0].Stop();
                musics[1].Stop();
                musics[2].Stop();
                musics[3].Play();
                break;

        }
    }


    public void PlayerHealthUpdater(float playerHealth)
    {
        playerHealthSlider.value = playerHealth;
    }

    public void BossHealthUpdater(float bossHealth)
    {
        bossHealthSlider.value = bossHealth;
    }

    public void BossHealthSliderActive()
    {
        bossHealthSlider.gameObject.SetActive(true);
    }
}
