using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewEnemyScript : MonoBehaviour
{
    public ENEMY_STATE states;
    public float moveSpeed;
    public float deltaStep;

    protected Vector3 scaleLeft = new Vector3(1,1,1);
    protected Vector3 scaleRight = new Vector3(-1, 1, 1);

    protected Vector3 lastPlayerPosition;

    public Animator animator;
    public SpriteRenderer spriteR;
    public Player player;
    private void Awake()
    {
        states = ENEMY_STATE.IDLE;
    }

    private void Start()
    {
        deltaStep = moveSpeed * Time.deltaTime;
        animator = GetComponentInChildren<Animator>();
        spriteR = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        lastPlayerPosition = player.transform.position;
        StartCoroutine(EnemyFSM());
    }

    IEnumerator EnemyFSM()
    {
        while(true)
        {
            yield return StartCoroutine(states.ToString());
        }
    }
    public enum ENEMY_STATE
    {
        IDLE = 0,
        CHASE = 1,
        ATTACK = 2,
    }

    public abstract IEnumerator IDLE();
    public abstract IEnumerator ATTACK();
    public abstract IEnumerator CHASE();
    public abstract IEnumerator DAMAGE();
    public abstract IEnumerator DIE();
}
