using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngle : MonoBehaviour
{
    private Vector3 Direccionin;
    public float Speedrot;
    public float Angle;
    // Start is called before the first frame update
    void Start()
    {
        Direccionin = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float VInput = Input.GetAxisRaw("Vertical");
        float HInput = Input.GetAxisRaw("Horizontal");
        Direccionin = new Vector3 (HInput, Angle, VInput) * -1;
    }

    private void FixedUpdate()
    {
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, Direccionin);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Speedrot);
    }
}
