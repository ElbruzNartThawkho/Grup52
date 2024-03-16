using UnityEngine;

public class AlertState : IState
{
    public void EnterState(Enemy enemy)
    {
        
    }

    public void ExitState(Enemy enemy)
    {

    }

    public void UpdateState(Enemy enemy)
    {
        Vector3 player = enemy.fieldOfView.player.position; player.y = enemy.transform.position.y;
        Quaternion lookOnLook = Quaternion.LookRotation(player - enemy.transform.position);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookOnLook, Time.deltaTime * 10);

        switch (enemy.type)
        {
            case Enemy.EnemyClass.melee:
                if (Vector3.Distance(enemy.transform.position, enemy.fieldOfView.player.position) <= enemy.agent.stoppingDistance)
                {
                    //enemy.meleeAttackZone.SetActive(true);
                    enemy.animator.Play("Attack");
                }
                else
                {
                    //if (enemy.meleeAttackZone.activeSelf is true)
                    //{
                    //    enemy.meleeAttackZone.SetActive(false);
                    //}
                    if (!enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        enemy.agent.SetDestination(enemy.fieldOfView.player.transform.position);
                    }
                }
                break;
            case Enemy.EnemyClass.ranger:
                if (Vector3.Distance(enemy.transform.position, enemy.fieldOfView.player.position) <= enemy.fieldOfView.range * 0.375f &&
                        Mathf.Sqrt(Mathf.Pow(enemy.agent.velocity.x, 2) + Mathf.Pow(enemy.agent.velocity.z, 2)) > 0.1f)
                {
                    enemy.agent.SetDestination(enemy.transform.position);
                }
                else if(Vector3.Distance(enemy.transform.position, enemy.fieldOfView.player.position) > enemy.fieldOfView.range)
                {
                    enemy.agent.SetDestination(enemy.fieldOfView.player.position);
                }
                //if (enemy.stillRangeAttack == false)
                //{
                //    enemy.RangeAttackWait();
                //}
                break;
        }
    }
}
