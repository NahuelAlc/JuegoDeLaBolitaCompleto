using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] private Controls Controls;
    [SerializeField] private GameObject Winner;
    [SerializeField] private GameObject UI;
    [SerializeField] private int win;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Winner.SetActive(false);
        UI.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Time.timeScale = 0.0f;
            SceneManager.LoadScene(3);
        }
    }
}
