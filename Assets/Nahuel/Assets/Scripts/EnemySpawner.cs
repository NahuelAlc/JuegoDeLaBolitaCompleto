using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemigo;
    [SerializeField] private bool encendido = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Spawner(){
        while(encendido){
            Instantiate(enemigo, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }        
    }
}
