using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Puerta : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int idBoton; 
    [SerializeField] private float velocidadPuerta = 20;

    private bool abrir = false;
    // Start is called before the first frame update
    void Start()
    {
        gM.OnBotonPulsado += Abrir;
    }

    private void Abrir(int idBotonPulsado)
    {
        if(idBotonPulsado == idBoton) abrir = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(abrir){
            transform.Translate(Vector3.down * velocidadPuerta * Time.deltaTime, Space.World);
        }
    }
}
