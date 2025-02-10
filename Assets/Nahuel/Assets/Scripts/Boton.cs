using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boton : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int idBoton = 0; 

    private void OnCollisionEnter(Collision collision){
        if(collision.transform.TryGetComponent(out PlayerDynamics player)){
            gM.BotonPulsado(idBoton);
            Debug.Log("Boton pulsado");
        }
    }
}