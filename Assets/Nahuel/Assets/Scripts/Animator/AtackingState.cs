using System;
using System.Threading;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class AtackingState : State<EnemyController>
{
    [SerializeField] private PlayerDynamics player;
    [SerializeField] private float baseAtackDmg;
    private AudioSource sonidoAtaque;
    //[SerializeField] private float baseAtackDmg = 10f;
    
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        controller.Anim.SetBool("atacking", true);
        Debug.Log("Estoy en estado de ataque");
        controller.Agent.stoppingDistance = controller.AtackDistance;
    }

    public override void OnExitState()
    {
        
    }

    public override void OnUpdateState()
    {   
        FaceTarget(); //Asegurarme que el enemigo enfoca al player 
    }

    private void FaceTarget()
    {
        Vector3 directionToTarget = (controller.Target.transform.position - transform.position).normalized;
        directionToTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sonidoAtaque = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Attack(){
        player.VidaActual -= 20f;
        player.Canvas.HealthBar.fillAmount = player.VidaActual / player.VidaInicial;
        sonidoAtaque.Play();
        Debug.Log("Hago daño");
    }

    private void OnFinishAttackAnimation(){
        if(Vector3.Distance(transform.position, controller.Target.transform.position) > controller.AtackDistance){
            controller.Anim.SetBool("atacking", false);
            controller.ChangeState(controller.PatrolState);
        } 
    }
}
