using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : MonoBehaviour
{
    [SerializeField] private GameManagerSO gM;
    [SerializeField] private int idTrampa = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activado(){
        gM.BotonPulsado(idTrampa);
    }
    public void Desactivado(){
        gM.TrapExit(idTrampa);
    }
}
