using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [HideInInspector] public Enemy enemy;
    [HideInInspector] public Transform player;
    [HideInInspector] public bool isRed;
    public float range = 8;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = Player.player.transform;
    }

    void Update()
    {
        if (isRed && enemy.currentState is not AlertState)
        {
            enemy.ChangeState(new AlertState());
        }
        isRed = Vector3.Distance(player.position, transform.position) < range;
    }
}
