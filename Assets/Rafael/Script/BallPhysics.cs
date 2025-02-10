using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallPhysics : MonoBehaviour
{
    [SerializeField] private Controls Controls;
    [SerializeField] private float fuerza = 5, fuerzaSalto = 5;
    [SerializeField] private float distanciaDeteccionSuelo = 1.1f;
    [SerializeField] private float distanciaPulsacion = 7f;
    [SerializeField] private float vidaInicial = 100;
    [SerializeField] private GameObject Winner;
    [SerializeField] private GameObject UI;
    [SerializeField] public Image Vida;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Score2;
    public int score = 0;
    private float vidaActual;
    public float VidaActual { get => vidaActual; set => vidaActual = value; }
    public float VidaInicial { get => vidaInicial; }

    private float X;
    private float Z;
    private Rigidbody rb;
    public float Fuerza;
    private int loop;
    public GameObject Camera;
    private float Y;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        VidaActual = VidaInicial;
        Vida.fillAmount =  VidaActual / VidaInicial;
        Time.timeScale = 1;
        Winner.SetActive(false);
        UI.SetActive(true);
        Score.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(VidaActual);
        X = Input.GetAxisRaw("Horizontal");
        Z = Input.GetAxisRaw("Vertical");
        Y = Camera.transform.eulerAngles.y;
        transform.localRotation = Quaternion.Euler (0,Y,0);
        if (rb.linearVelocity.magnitude > 0.3)
        {
            if (loop == 0)
            {
                gameObject.GetComponent<AudioSource>().Play();
                gameObject.GetComponent<AudioSource>().UnPause();
            }
            loop = 1;
        }
        else
        {
            loop = 0;
            gameObject.GetComponent<AudioSource>().Pause();
        }
        if (Physics.Raycast(transform.position, Vector3.down, distanciaDeteccionSuelo))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up.normalized * fuerzaSalto, ForceMode.Impulse);
            }
        }
        if (VidaActual <= 0) 
        {
            Winner.SetActive(true);
            UI.SetActive(false);
            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3 (X, 0, Z).normalized * Fuerza, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bouncer"))
        {
            Fuerza = 0;
            Invoke("Fuerzavalue", 1f); // Llamamos a la corrutina
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
        if (collision.gameObject.CompareTag("cilindrogolpeador"))
        {
            VidaActual -= 20;
           Vida.fillAmount = VidaActual / VidaInicial;
        }
    }
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.CompareTag("cuboOro"))
        {
            Destroy(Other.gameObject);
            score++;
            Score.text = "Score: " + score.ToString();
            Score2.text = "Score: " + score.ToString();
        }
        if (Other.gameObject.TryGetComponent(out Trampa trampa))
        {
            trampa.Activado();
        }
    }

    private void Fuerzavalue()
    {
        Fuerza = 10; // Restauramos el valor original
    }
}
