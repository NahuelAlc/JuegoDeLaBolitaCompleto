using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State<EnemyController>
{
    [SerializeField] private float chaseVelocity = 5f;
    [SerializeField] private float tiempoAntesDePatrullar = 2f;

    private Coroutine coroutine; 
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        controller.Agent.speed = chaseVelocity;
        Debug.Log("Estoy en estado de perseguir!!!");
        controller.Agent.stoppingDistance = controller.AtackDistance;
        controller.Agent.acceleration = 100000;
    }

    public override void OnExitState()
    {
        
    }

    public override void OnUpdateState()
    {   
        if(!controller.Agent.pathPending && controller.Agent.CalculatePath(controller.Target.position, new NavMeshPath())){
            StopMyCoroutine();
            controller.Agent.SetDestination(controller.Target.position);
            if(!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance){
                controller.ChangeState(controller.AtackingState);
            }
        } else {
            coroutine ??= StartCoroutine(StopAndReturn());
        }
    }

    private IEnumerator StopAndReturn(){
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
