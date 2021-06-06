using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewEnemyScript : MonoBehaviour
{
    public ENEMY_STATE states;
    public float moveSpeed;
    public float deltaStep;
    public float knockback;
    public float aggroRange;
    public int damage;
    public int maxHp;
    public int currentHp;

    public bool isInvincible;

    protected Vector3 scaleLeft = new Vector3(1,1,1);
    protected Vector3 scaleRight = new Vector3(-1, 1, 1);
    protected Vector3 knockBackTaken;
    protected Vector3 lastPlayerPosition;

    public Animator animator;
    public SpriteRenderer spriteR;
    public Player player;

    public Coroutine coroutine;
    protected GameHandlerScript gameHandler;
    private void Awake()
    {
        states = ENEMY_STATE.IDLE;
        currentHp = maxHp;
    }

    private void Start()
    {
        gameHandler = FindObjectOfType<GameHandlerScript>();
        deltaStep = moveSpeed * Time.deltaTime;
        animator = GetComponentInChildren<Animator>();
        spriteR = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        lastPlayerPosition = player.transform.position;
        coroutine = StartCoroutine(EnemyFSM());
    }

    public void ResetRoutine()
    {
        StopAllCoroutines();
        deltaStep = moveSpeed * Time.deltaTime;
        lastPlayerPosition = player.transform.position;
        coroutine = StartCoroutine(EnemyFSM());
    }

    public virtual IEnumerator EnemyFSM()
    {
        while (Mathf.Abs(transform.position.x - player.transform.position.x) < aggroRange)
        {
            yield return StartCoroutine(states.ToString());
        }
    }
    public enum ENEMY_STATE
    {
        IDLE = 0,
        CHASE = 1,
        ATTACK = 2,
        DAMAGE = 3,
    }

    public abstract IEnumerator IDLE();
    public abstract IEnumerator ATTACK();
    public abstract IEnumerator CHASE();
    public abstract IEnumerator DAMAGE();
    public abstract void DIE();




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))            
        {                
            player.getHitForDamage(damage, this.gameObject.transform, knockback);            
        }
        if (!isInvincible)
        {
            if (collision.gameObject.CompareTag("Attack"))
            {
                if(currentHp > 1)
                {
                    isInvincible = true;
                    if (states != ENEMY_STATE.DAMAGE) { StopCoroutine(coroutine); }
                    getHitForDamage(1);
                    knockBackTaken = Vector3.zero;
                    states = ENEMY_STATE.DAMAGE;
                    StartCoroutine(EnemyFSM());
                }
                    else DIE();
            }
            if (collision.gameObject.CompareTag("heavyAttack"))
            {
                getHitByHeavyAttack();
            }
            if (collision.gameObject.CompareTag("downTilt"))
            {
                if (currentHp > 1)
                {
                    isInvincible = true;
                    if (states != ENEMY_STATE.DAMAGE) { StopCoroutine(coroutine); }
                    getHitForDamage(1);
                    knockBackTaken = new Vector3(0, 3, 0);
                    states = ENEMY_STATE.DAMAGE;
                    StartCoroutine(EnemyFSM());
                }
                    else DIE();
            }
        }
    }

    public void getHitForDamage(int damage)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
            StopAllCoroutines();
            DIE();
        }
    }

    public virtual void getHitByHeavyAttack()
    {
        isInvincible = true;
        if (states != ENEMY_STATE.DAMAGE) { StopCoroutine(coroutine); }
        getHitForDamage(2);
        knockBackTaken = new Vector3(2, 0, 0);
        states = ENEMY_STATE.DAMAGE;
        StartCoroutine(EnemyFSM());
    }
}
