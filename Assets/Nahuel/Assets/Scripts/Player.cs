using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float velocidad = 5;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 100000;
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");  // -1, 0, 1
        float vInput = Input.GetAxisRaw("Vertical"); //-1, 0, 1
        Vector3 direccionMovimiento = new Vector3(hInput, 0 , vInput).normalized;
        transform.Translate(direccionMovimiento * velocidad * Time.deltaTime);

        
    }
    
    private void OnTriggerEnter (Collider Other){
        Destroy(Other.gameObject);
    }  
}
