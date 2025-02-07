using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiGameManager")]
public class GameManagerSO : ScriptableObject
{
    public event Action<int> OnBotonPulsado; 
    public event Action<int> OnTrapExit;
    // Start is called before the first frame update
    public void BotonPulsado(int idBoton)
    {
        //lanza el evento!!
        OnBotonPulsado?.Invoke(idBoton);
    }
    public void TrapExit(int idTrampa){
        OnTrapExit?.Invoke(idTrampa);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
