using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasdeRafa : MonoBehaviour
{
    [SerializeField] private Controls Controls;
    [SerializeField] private int idPuerta;
    private bool loop;

    private bool abrir;
    // Start is called before the first frame update
    void Start()
    {
        Controls.OnLlaveObtenida += Abrir;
        loop = false;
    }

    private void Abrir(int idLlave)
    {
        if (idPuerta == idLlave)
        {
            abrir = true;
            loop = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (abrir)
        {
            transform.Translate(Vector3.down * 5 * Time.deltaTime);
            if (loop)
            {
                this.gameObject.GetComponent<AudioSource>().Play();
                loop = false;
            }

        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
