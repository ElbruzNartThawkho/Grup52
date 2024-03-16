using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyClass type;
    public IState currentState;
    public float rangeAttackWaitTime;
    public int damage;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public FieldOfView fieldOfView;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool stillRangeAttack = false;

    private void Awake()
    {
        agent=GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        fieldOfView=GetComponent<FieldOfView>();
    }
    private void Start()
    {
        ChangeState(new PatrolState());
    }
    private void Update()
    {
        currentState.UpdateState(this);
        AnimUpdate();
    }
    void AnimUpdate()
    {
        animator.SetFloat("Blend", Mathf.Clamp(Mathf.Sqrt(Mathf.Pow(agent.velocity.x, 2) + Mathf.Pow(agent.velocity.z, 2)), 0, 1));
    }
    public void ChangeState(IState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
    public void MeleeAttack()
    {
        if (Vector3.Distance(transform.position, fieldOfView.player.position) <= agent.stoppingDistance)
        {
            Player.player.health.TakeDamage(damage);
        }
    }
    public void RangeAttackWait()
    {
        StartCoroutine(PreparationForFire(rangeAttackWaitTime));
    }
    IEnumerator PreparationForFire(float time)
    {
        stillRangeAttack = true;
        for (float i = 0; i <= time;)
        {
            i += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        animator.Play("Attack");
        stillRangeAttack = false;
    }
    public enum EnemyClass
    {
        melee = 0,
        ranger = 1,
    }
    public void Died()
    {
        Destroy(gameObject);
    }
}
