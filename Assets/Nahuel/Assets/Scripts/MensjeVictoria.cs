using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MensjeVictoria : MonoBehaviour
{
    [SerializeField] private float segundos = 6f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Reset());
    }
    private IEnumerator Reset(){
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
