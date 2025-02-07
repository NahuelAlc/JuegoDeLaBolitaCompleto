using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Panel : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int idBoton = 0;
    [SerializeField] private float velocity = 20;
    [SerializeField] private Transform destination; 
    private bool opening = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(opening){
            transform.position = Vector3.MoveTowards(transform.position, destination.position,velocity * Time.deltaTime);
        }
    }
    public void Interactuar(){
        opening = true;
        gM.BotonPulsado(idBoton);
    }
}
