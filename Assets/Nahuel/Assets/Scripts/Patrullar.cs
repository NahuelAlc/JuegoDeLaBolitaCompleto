using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private Transform ruta;
    [SerializeField] private float tiempoDeEspera = 2f;
    [SerializeField] private float rangoVision = 15f;
    [SerializeField] private float anguloVision = 160f;
    [SerializeField] private LayerMask queEsTarget;
    [SerializeField] private LayerMask queEsObstaculo;
    private List<Vector3> puntosDeRuta = new List<Vector3>();
    private NavMeshAgent agent;
    private int indicePuntoActual = 0;
    private Vector3 destinoActual;

    private void Awake(){
        //ruta = GameObject.FindAnyObjectByType<Ruta>().transform;
        foreach (Transform punto in ruta){
            puntosDeRuta.Add(punto.position);
        }
        agent = GetComponent<NavMeshAgent>();
        destinoActual = puntosDeRuta[indicePuntoActual];
        StartCoroutine(PatrullarYEsperar());
    }

    private IEnumerator PatrullarYEsperar(){
        while(true){
            agent.SetDestination(destinoActual);
            yield return new WaitUntil(()=> !agent.pathPending && agent.remainingDistance <= 0.2f);
            yield return new WaitForSeconds(tiempoDeEspera);
            CalcularNuevoDestino();
        }
    }

    private void CalcularNuevoDestino(){
        indicePuntoActual++;
        indicePuntoActual %= puntosDeRuta.Count; //Modulo
        destinoActual = puntosDeRuta[indicePuntoActual];
    }

    private void FixedUpdate(){
        Collider[] collsDetectados = Physics.OverlapSphere(transform.position, rangoVision, queEsTarget);
        if(collsDetectados.Length > 0){
            Vector3 direccionATarget=(collsDetectados[0].transform.position - transform.position).normalized;    
            if (!Physics.Raycast(transform.position, direccionATarget, rangoVision, queEsObstaculo)){
                if(Vector3.Angle(transform.forward, direccionATarget) <= anguloVision / 2){
                    enabled = false;
                }
            }
        }
    }
}
