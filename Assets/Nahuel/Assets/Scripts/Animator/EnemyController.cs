using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Controller
{
    [SerializeField] private float rangoVision = 15f;
    [SerializeField] private float anguloVision = 160f;
    [SerializeField] private float atackDistance = 2f;
    
    [SerializeField] private float MaxVelocity = 5f;
    
    private Animator anim;
    private State<EnemyController> currentState;
    private NavMeshAgent agent;
    private PatrolState patrolState;
    private ChaseState chaseState;
    private AtackingState atackingState;
    private Transform target;

    #region Getters & setters
    public float AnguloVision { get => anguloVision; }
    public float RangoVision { get => rangoVision;}
    public PatrolState PatrolState { get => patrolState; set => patrolState = value; }
    public ChaseState ChaseState { get => chaseState; set => chaseState = value; }
    public AtackingState AtackingState { get => atackingState; set => atackingState = value; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public State<EnemyController> CurrentState { get => currentState; set => currentState = value; }
    public Transform Target { get => target; set => target = value; }
    public float AtackDistance { get => atackDistance; set => atackDistance = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public float ChaseVelocity { get => MaxVelocity; set => MaxVelocity = value; }
    #endregion

    private void Awake(){
        PatrolState = GetComponent<PatrolState>();
        AtackingState = GetComponent<AtackingState>();
        ChaseState = GetComponent<ChaseState>();
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        ChangeState(PatrolState);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState != null){
            CurrentState.OnUpdateState();
        }
    }
    public void ChangeState(State<EnemyController> newState)
    {
        if (CurrentState != null && CurrentState != newState){
            CurrentState.OnExitState();
        }
        CurrentState = newState;
        CurrentState.OnEnterState(this);
    }
}
