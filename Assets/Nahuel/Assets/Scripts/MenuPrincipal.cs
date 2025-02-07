using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsMenuPanel;
    [SerializeField] private AudioMixer aM;
    void Start()
    {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void SetNewVolumeToSfx(float volume){
        aM.SetFloat("SoundVolume", volume);
    }
    public void SetNewVolumeToMusic(float volume){
        aM.SetFloat("MusicVolume", volume);
    }
    public void OnBackButtonClick(){
        mainMenuPanel.SetActive(true);
        optionsMenuPanel.SetActive(false);
    }
    public void OnPlayButtonClick(){
        SceneManager.LoadScene(0);
    }
    public void OnOptionsButtonClick(){
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
        Debug.Log("Botón Options presionado");
    }
    public void OnQuitButtonClick(){
        Application.Quit();
    }
    public void SetNewFullscreenState(bool isfullscreen){
        Screen.fullScreen = isfullscreen;
    }
}
