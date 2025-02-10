using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDynamics : MonoBehaviour
{

    [SerializeField] private float fuerza = 5, fuerzaSalto = 5 ; //Newtons
    [SerializeField] private canvasManager canvas;
    [SerializeField] private GameObject mP;
    [SerializeField] private float vidaInicial = 100;
    [SerializeField] private Transform spawn;
    [SerializeField] private float distanciaDeteccionSuelo = 1.1f;
    [SerializeField] private float distanciaPulsacion = 7f;
    [SerializeField] private AudioClip audioSalto;
    [SerializeField] private AudioClip audioGolpe;
    [SerializeField] private AudioClip audioRolling;
    private AudioSource audioSource;
    private float vidaActual;
    private float score = 0;
    private Rigidbody rb;
    private float hInput, vInput;
    public GameObject Camera;
    private float Y;

    public float VidaActual { get => vidaActual; set => vidaActual = value; }
    public canvasManager Canvas { get => canvas; set => canvas = value; }
    public float VidaInicial { get => vidaInicial; }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        VidaActual = VidaInicial;
        Application.targetFrameRate = 100000;
        rb = GetComponent<Rigidbody>(); 
        Canvas.gameObject.SetActive(true);
        mP.SetActive(false);
        Canvas.ScoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");  // -1, 0, 1
        vInput = Input.GetAxisRaw("Vertical"); //-1, 0, 1
        Y = Camera.transform.eulerAngles.y;
        transform.localRotation = Quaternion.Euler(0,Y,0);

        if (!audioSource.isPlaying && Physics.Raycast(transform.position, Vector3.down, distanciaDeteccionSuelo)){
            audioSource.PlayOneShot(audioRolling);
        }
        if(Physics.Raycast(transform.position, Vector3.down, distanciaDeteccionSuelo)){
            if(Input.GetKey(KeyCode.Space)){
                fuerzaSalto += 3;
            }
            if(Input.GetKeyUp(KeyCode.Space)){
                if(fuerzaSalto > 10) fuerzaSalto = 10;
                rb.AddForce(Vector3.up.normalized * fuerzaSalto, ForceMode.Impulse);
                audioSource.PlayOneShot(audioSalto);
                fuerzaSalto = 5;
            }
        }
        if(Input.GetKeyDown(KeyCode.E)){
            if(Physics.Raycast(transform.position, Vector3.right, out RaycastHit hit, distanciaPulsacion))
            {
                if(hit.transform.TryGetComponent(out Panel panel))
                {
                    panel.Interactuar();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
                if(mP.gameObject.activeInHierarchy){
                    canvas.gameObject.SetActive(true);
                    mP.SetActive(false);
                    Debug.Log("Canvas activado.");
                }
                else {
                    canvas.gameObject.SetActive(false);
                    mP.SetActive(true);
                    foreach (Transform child in mP.transform)
                    {
                        child.gameObject.SetActive(true); // Activa todos los hijos
                    }
                    Debug.Log("Menu activado.");
                }
            }
        if(VidaActual <= 0){
            transform.position = spawn.position;
            VidaActual = 100;
            rb.linearVelocity = Vector3.zero;
            SceneManager.LoadScene(0);
        }
        if(score == 25){
            SceneManager.LoadScene(2);
        }
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3(hInput, 0 , vInput).normalized * fuerza, ForceMode.Force);
    }
    
    private void OnTriggerEnter (Collider Other){
        if(Other.gameObject.CompareTag("cuboOro")){
            Destroy(Other.gameObject);
            score++;
            Canvas.ScoreText.text = "Score: " + score.ToString();
            audioSource.Play();
        }
        if(Other.gameObject.TryGetComponent(out Trampa trampa)){
            trampa.Activado();
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("cilindrogolpeador")){
            VidaActual -= 20;
            Canvas.HealthBar.fillAmount = VidaActual / VidaInicial;
            audioSource.PlayOneShot(audioGolpe);
        }
        if(other.gameObject.TryGetComponent(out TechoTrampa trampa)){
            transform.position = spawn.position;
            Canvas.HealthBar.fillAmount = VidaInicial;
            trampa.Cayendo = false;
            SceneManager.LoadScene(0);
        }
    }
}
