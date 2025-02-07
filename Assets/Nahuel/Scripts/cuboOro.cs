using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuboOro : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime, Space.World);
    }
}
