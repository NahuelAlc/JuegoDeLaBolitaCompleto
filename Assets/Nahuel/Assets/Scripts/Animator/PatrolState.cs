using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State<EnemyController>
{
    [SerializeField] private Transform ruta;
    [SerializeField] private float tiempoDeEspera = 2f;
    [SerializeField] private float patrolVelocity = 5f;
    [SerializeField] private LayerMask queEsTarget;
    [SerializeField] private LayerMask queEsObstaculo;
    private List<Vector3> puntosDeRuta = new List<Vector3>();
    private int indicePuntoActual = 0;
    private Vector3 destinoActual;

    public override void OnEnterState(EnemyController controller){
        base.OnEnterState(controller);
        foreach (Transform punto in ruta){
            puntosDeRuta.Add(punto.position);
        } 
        Debug.Log("New route: " + puntosDeRuta);
        controller.Agent.stoppingDistance = controller.AtackDistance;
        controller.Agent.acceleration = 100000;
        controller.Agent.speed = patrolVelocity;
        destinoActual = puntosDeRuta[indicePuntoActual];
        StartCoroutine(PatrullarYEsperar());
    }    

    public override void OnExitState()
    {
         StopAllCoroutines();
    }

    public override void OnUpdateState()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(transform.position, controller.RangoVision, queEsTarget);
        if(collsDetectados.Length > 0){
            Vector3 direccionATarget=(collsDetectados[0].transform.position - transform.position).normalized;    
            if (!Physics.Raycast(transform.position, direccionATarget, controller.RangoVision, queEsObstaculo)){
                if(Vector3.Angle(transform.forward, direccionATarget) <= controller.AnguloVision / 2){
                    controller.Target = collsDetectados[0].transform;
                    controller.ChangeState(controller.ChaseState);
                }
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PatrullarYEsperar(){
        while(true){
            controller.Agent.SetDestination(destinoActual);
            yield return new WaitUntil(()=> !controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance);
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
        
    }
}
