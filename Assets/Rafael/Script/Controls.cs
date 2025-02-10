using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Contolador")]
    public class Controls : ScriptableObject
    {
    //Para abrir la puerta
    public event Action<int> OnLlaveObtenida;
    //Cuando obtienes la llave
    public void LlaveObtenida(int idLlave)
        {
            OnLlaveObtenida?.Invoke(idLlave);
        }
}