using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuboRotatorio : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float fuerzaRotacion = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(Vector3.up * fuerzaRotacion, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * fuerzaRotacion * Time.deltaTime, Space.World);
    }
}
