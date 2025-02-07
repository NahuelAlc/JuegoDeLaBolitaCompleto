using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDynamics : MonoBehaviour
{

    [SerializeField] private float fuerza = 5, fuerzaSalto = 5 ; //Newtons
    [SerializeField] private canvasManager canvas;
    [SerializeField] private MenuPrincipal mP;
    [SerializeField] private float vidaInicial = 100;
    [SerializeField] private Transform spawn;
    [SerializeField] private float distanciaDeteccionSuelo = 1.1f;
    [SerializeField] private float distanciaPulsacion = 7f;
    private float vidaActual;
    private float score = 0;
    private Rigidbody rb;
    private float hInput, vInput; 

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaInicial;
        Application.targetFrameRate = 100000;
        rb = GetComponent<Rigidbody>();
        canvas.gameObject.SetActive(true);
        mP.gameObject.SetActive(false);
        canvas.ScoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");  // -1, 0, 1
        vInput = Input.GetAxisRaw("Vertical"); //-1, 0, 1
        
        if(Physics.Raycast(transform.position, Vector3.down, distanciaDeteccionSuelo)){
            if(Input.GetKey(KeyCode.Space)){
                fuerzaSalto += 3;
            }
            if(Input.GetKeyUp(KeyCode.Space)){
                if(fuerzaSalto > 10) fuerzaSalto = 10;
                rb.AddForce(Vector3.up.normalized * fuerzaSalto, ForceMode.Impulse);
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
                    mP.gameObject.SetActive(false);
                }
                else {
                    canvas.gameObject.SetActive(false);
                    mP.gameObject.SetActive(true);
                }
            }
        if(vidaActual <= 0){
            transform.position = spawn.position;
            vidaActual = 100;
            rb.linearVelocity = Vector3.zero;
            SceneManager.LoadScene(0);
        }
        if(score == 15){
            SceneManager.LoadScene(2);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(hInput, 0 , vInput).normalized * fuerza, ForceMode.Force);
    }
    
    private void OnTriggerEnter (Collider Other){
        if(Other.gameObject.CompareTag("cuboOro")){
            Destroy(Other.gameObject);
            score++;
            canvas.ScoreText.text = "Score: " + score.ToString();
        }
        if(Other.gameObject.TryGetComponent(out Trampa trampa)){
            trampa.Activado();
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("cilindrogolpeador")){
            vidaActual -= 20;
            canvas.HealthBar.fillAmount = vidaActual / vidaInicial;
        }
        if(other.gameObject.TryGetComponent(out TechoTrampa trampa)){
            transform.position = spawn.position;
            canvas.HealthBar.fillAmount = vidaInicial;
            trampa.Cayendo = false;
            SceneManager.LoadScene(0);
        }
    }
}
