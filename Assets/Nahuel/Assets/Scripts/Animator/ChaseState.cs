using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State<EnemyController>
{
    [SerializeField] private float tiempoAntesDePatrullar = 2f;

    private Coroutine coroutine; 
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        controller.Agent.speed = controller.ChaseVelocity;
        Debug.Log("Estoy en estado de perseguir!!!");
        controller.Agent.stoppingDistance = controller.AtackDistance;
        controller.Agent.acceleration = 100000;
    }

    public override void OnExitState()
    {
        
    }

    public override void OnUpdateState()
    {   
        controller.Anim.SetFloat("velocity", controller.Agent.velocity.magnitude / controller.ChaseVelocity);
        if(!controller.Agent.pathPending && controller.Agent.CalculatePath(controller.Target.position, new NavMeshPath())){
            StopAllCoroutines();
            controller.Agent.SetDestination(controller.Target.position);
            if(!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance){
                controller.ChangeState(controller.AtackingState);
            }
        } else {
            coroutine ??= StartCoroutine(StopAndReturn());
        }
    }

    private IEnumerator StopAndReturn(){
        Debug.Log("Lo he perdido");
        yield return new WaitForSeconds(tiempoAntesDePatrullar);
        controller.ChangeState(controller.PatrolState);
    }

    private void StopMyCoroutine()
    {
        StopAllCoroutines();
        coroutine = null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
