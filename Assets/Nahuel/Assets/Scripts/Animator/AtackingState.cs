using System.Threading;
using UnityEngine;

public class AtackingState : State<EnemyController>
{
    [SerializeField] private float timeBetweenAtacks = 1.4f; 
    [SerializeField] private float baseAtackDmg = 10f;
    private float timer;
    
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        Debug.Log("Estoy en estado de ataque");
        controller.Agent.stoppingDistance = controller.AtackDistance;
        timer = timeBetweenAtacks;
    }

    public override void OnExitState()
    {
        
    }

    public override void OnUpdateState()
    {   
        controller.Agent.SetDestination(controller.Target.position);
        //Si tengo el palyer en rango de ataque
        if(!controller.Agent.pathPending && controller.Agent.remainingDistance < controller.Agent.stoppingDistance){
            timer += Time.deltaTime;
                if(timer >= timeBetweenAtacks){
                    timer = 0;
                    Debug.Log("Hago daño"); 
                }
        }
        else {
            controller.ChangeState(controller.PatrolState);
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
}
