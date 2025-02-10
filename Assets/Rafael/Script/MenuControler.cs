using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControler : MonoBehaviour
{

    [SerializeField] private AudioMixer MainMixer;

    [SerializeField] private GameObject MainMenu;

    [SerializeField] private GameObject MenuOptions;

    [SerializeField] private GameObject MenuLevel;

    [SerializeField] private string Nahuel;

    [SerializeField] private string Rafael;

    [SerializeField] private string Salva;

    [SerializeField] private TMP_Dropdown QualityDropdown;

    [SerializeField] private TMP_Dropdown ResolutionsDropdown;

    [SerializeField] private Toggle FullScreenToggle;

    [SerializeField] private Slider Music;

    [SerializeField] private Slider SFX;

    private Resolution[] resolutions;

    private List<string> resolutionsoptions = new List<string>();



    // Start is called before the first frame update
    void Start()
    {
        ResolutionInit();
        
        QualityDropdown.value = QualitySettings.GetQualityLevel();
        QualityDropdown.RefreshShownValue();

        ResolutionsDropdown.value = GetResolution();
        ResolutionsDropdown.RefreshShownValue();

        FullScreenToggle.isOn = Screen.fullScreen;

        Music.value = GetMusic();

        SFX.value = GetSFX();
    }

    private void ResolutionInit()
    {
        resolutions = Screen.resolutions;
                foreach (var resolution in resolutions) 
                {
                    resolutionsoptions.Add(resolution.width + "x" + resolution.height);
                }
                ResolutionsDropdown.AddOptions(resolutionsoptions);
    }

    private int GetResolution()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                return i;
            }
        }
        return 0;
    }

    private float GetMusic()
    {
        float Musicvalue;
        if (MainMixer.GetFloat("Musicvol", out Musicvalue))
        {
            return Musicvalue;
        }
        return 0;
    }

    private float GetSFX()
    {
        float SFXvalue;
        if (MainMixer.GetFloat("SFXvol", out SFXvalue))
        {
            return SFXvalue;
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OptionsMenu()
    {
        Time.timeScale = 0.0f;
        MainMenu.SetActive(false);
        MenuOptions.SetActive(true);
        
        ResolutionInit();

        QualityDropdown.value = QualitySettings.GetQualityLevel();
        QualityDropdown.RefreshShownValue();

        ResolutionsDropdown.value = GetResolution();
        ResolutionsDropdown.RefreshShownValue();

        FullScreenToggle.isOn = Screen.fullScreen;

        Music.value = GetMusic();

        SFX.value = GetSFX();
    }

    public void MMenu() { 
        
        Time.timeScale = 1.0f;
        MainMenu.SetActive(true);
        MenuOptions.SetActive(false);
        
        ResolutionInit();

        QualityDropdown.value = QualitySettings.GetQualityLevel();
        QualityDropdown.RefreshShownValue();

        ResolutionsDropdown.value = GetResolution();
        ResolutionsDropdown.RefreshShownValue();

        FullScreenToggle.isOn = Screen.fullScreen;

        Music.value = GetMusic();

        SFX.value = GetSFX();
    }

    public void Play()
    {
        MainMenu.SetActive(false);
        MenuLevel.SetActive(true);
    }

    public void NahuelJuego()
    {
        SceneManager.LoadScene(Nahuel);
    }

    public void RafaelJuego()
    {
        SceneManager.LoadScene(Rafael);
    }

    public void SalvaJuego()
    {
        SceneManager.LoadScene(Salva);
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackMenulevel()
    {
        MainMenu.SetActive(true);
        MenuLevel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetNewVolumeMusic(float volume)
    {
        MainMixer.SetFloat("Musicvol", volume);
    }

    public void SetNewVolumeSFX(float volume)
    {
        MainMixer.SetFloat("SFXvol", volume);
    }

    public void FullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }

    public void Quality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void OnDropdown(int resolutionindex)
    {
        Resolution ChosenResolution = resolutions[resolutionindex];
        Screen.SetResolution(ChosenResolution.width, ChosenResolution.height, Screen.fullScreen);
    }
}
