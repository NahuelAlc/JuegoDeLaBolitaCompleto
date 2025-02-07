using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechoTrampa : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int idTrampa = 0;
    [SerializeField] private float velocidadTrampa = 10;
    [SerializeField] private Transform posicionInicial;
    private bool cayendo = false;

    public int IdTrampa { get => idTrampa; set => idTrampa = value; }
    public bool Cayendo { get => cayendo; set => cayendo = value; }

    private void OnEnable(){
        gM.OnBotonPulsado += Caer;
        gM.OnTrapExit += Subir;
    }
    void Start()
    {
        
    }
    private void OnDisable(){
        gM.OnBotonPulsado -= Caer;
        gM.OnTrapExit -= Subir;
    }
    
    public void Caer(int idZonaEntrada){
        if(idZonaEntrada == IdTrampa) Cayendo = true;
    }
    public void Subir(int idZonaSalida){
        if(idZonaSalida == IdTrampa) Cayendo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Cayendo){
            transform.Translate(Vector3.down * velocidadTrampa * Time.deltaTime, Space.World);
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, posicionInicial.position, velocidadTrampa * Time.deltaTime);
        }
    }
}
