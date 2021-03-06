using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Player : Entity, IDamagable {

	public LayerMask punchMe;
	
	float gravity;
	Vector2 directionalInput;
	
	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	
	float maxJumpVelocity;
	float minJumpVelocity;
	
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	
	public float moveSpeed = 6;
	Vector3 velocity;
	float velocityXSmoothing;
	
	public bool hitConnect;
	Vector3 jabSize;
	Vector2 targetDash;
	Vector2 targetKnockedBack;
	Vector2 targetDashPosition;
	public float dashSpeed = 3f;
	public bool isDashing;
	public float dashLength = 5f;
	public float dashCooldown;
	public bool isKnockedBack;

	Player[] players;
	Animator animeThor;
	Controller2D controller;
	SpriteRenderer spriteR;
	BoxCollider2D box2D;
	public AudioClip damage;
	public AudioClip death;
	public bool isDead;

	GameObject[] gOver;
	GameHandlerScript gameHandler;

	bool loadingScene;
	private void Awake()
    {
		maxHp = 10;
		DontDestroyOnLoad(this);
    }
    void Start() 
	{
		gOver = GameObject.FindGameObjectsWithTag("GameOver");
		gameHandler = FindObjectOfType<GameHandlerScript>();
		players = FindObjectsOfType<Player>();
		if (players.Length > 1)
		{
			players[1].currentHp = this.currentHp;
			Destroy(this.gameObject);
		}
		controller = GetComponent<Controller2D> ();
		animeThor = GetComponentInChildren<Animator>();
		spriteR = GetComponentInChildren<SpriteRenderer>();
		box2D = GetComponentInChildren<BoxCollider2D>();
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		currentHp = maxHp;
	}

	void Update()
	{
		if (!gameHandler.isRunning) { return; }
		CalculateVelocity();
		if (!isDashing && !isKnockedBack)
		{
			controller.Move(velocity * Time.deltaTime, directionalInput);

			if (controller.collisions.above || controller.collisions.below)
			{
				if (controller.collisions.slidingDownMaxSlope)
				{
					velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
				}
				else
				{
					velocity.y = 0;
				}
			}
		}
		if (isDashing)
		{
			gravity = 0;
			controller.Move(targetDash * Time.deltaTime * dashSpeed, directionalInput);
		}
		else
		{
			gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		}

		if (transform.position.x > 130 && !loadingScene) { loadingScene = true; OnSceneLoaded(); SceneManager.LoadScene(2);}
		updateAnimations();
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

    public void updateAnimations()
    {
        if (directionalInput.x > 0)
        {
			animeThor.gameObject.transform.localScale = scaleRight;
			animeThor.SetBool("isWalking", true);
        }
		else if(directionalInput.x < 0)
        {
			animeThor.gameObject.transform.localScale = scaleLeft;
			animeThor.SetBool("isWalking", true);
		}
		else
        {
			animeThor.SetBool("isWalking", false);
		}

        if (controller.collisions.below)
        {
			animeThor.SetBool("isGrounded", true);
		}
        else
        {
			animeThor.SetBool("isGrounded", false);
		}

        if(Mathf.Abs(transform.position.x - targetDashPosition.x) < 1f)
		{
			isDashing = false;
			isInvincible = false;
		}
    }
	public void OnJumpInputDown() {
		if (controller.collisions.below) 
		{
			animeThor.SetTrigger("Jump");
			if (controller.collisions.slidingDownMaxSlope) 
			{
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) 
				{ // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} 
			else 
			{
				velocity.y = maxJumpVelocity;
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

	public void OnMouseLeftDown()
    {
		if(canAttack())
        {
            if (controller.collisions.below || directionalInput == Vector2.zero)
            {
				lightAttack();
            }
            else
            {
                if (Time.time > dashCooldown)
                {
					dashAttack();
					dashCooldown = Time.time + 1f;
                }
            }
        }
	}

	public bool canAttack()
	{
		float coolDown = 0f;
		if (Time.time > coolDown)
		{
			coolDown = Time.time + 0.5f;
			return true;
		}
        else
        {
			return false;
        }
	}
	public void OnMouseRightDown()
	{
		animeThor.gameObject.tag = "heavyAttack";
		animeThor.SetTrigger("heavyAttack");
	}
	public void OnSInputUp()
	{
	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}

	public void lightAttack()
    {
		//if((animeThor.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.robotWalk") || animeThor.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle")))
        //{
			animeThor.gameObject.tag = "Attack";
			animeThor.SetTrigger("lightAttack");
        //}
	}

	public void OnDownTilt()
    {
		if (controller.collisions.below)
		{
			downTilt();
		}
		else
		{
			if (Time.time > dashCooldown)
			{
				dashAttack();
				dashCooldown = Time.time + 1f;
			}
		}
	}

	public void dashAttack()
    {
		isDashing = true;
		isInvincible = true;
		animeThor.gameObject.tag = "Attack";
		animeThor.SetTrigger("dashAttack");
		targetDash = new Vector2((directionalInput.x * dashLength),0);
		targetDashPosition = new Vector2(transform.position.x + (directionalInput.x * dashLength), 0);
	}
	public void downTilt()
    {
		//if (controller.collisions.below && (animeThor.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.robotWalk") || animeThor.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle")))
		//{
			animeThor.gameObject.tag = "downTilt";
			animeThor.SetTrigger("downTilt");
		//}
	}

	public override void getHitForDamage(int damage, Transform attacker, float knockback)
    {
		if(!isInvincible)
        {
			AudioSource.PlayClipAtPoint(this.damage, transform.position);
			animeThor.SetTrigger("takeDamage");
			currentHp -= damage;
            if (currentHp < 0) { currentHp = 0; }
			if (checkForDeath())
			{
				gameOver();
			}
			Vector2 direction = (transform.position - attacker.transform.position).normalized.x * Vector2.right;
			StartCoroutine(InvincibilityCoroutine(2f, direction.normalized, knockback));
			StartCoroutine(knockBackCoroutine(0.2f, direction.normalized, knockback));
        }
    }
	public override void gameOver()
    {
		AudioSource.PlayClipAtPoint(death, transform.position);
		gOver = GameObject.FindGameObjectsWithTag("GameOver");
		gameHandler.isRunning = false;
		isInvincible = true;
		isDead = true;
		animeThor.SetTrigger("isDead");
        foreach (GameObject obj in gOver)
        {
			obj.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnDrawGizmos()
    {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(Vector3.right + (0.5f * Vector3.up), jabSize);
	}

    public override IEnumerator InvincibilityCoroutine(float duration, Vector2 direction, float knockback)
	{

		isInvincible = true;
		for (float t = 0; t < duration; t += Time.deltaTime)
		{
			yield return new WaitForEndOfFrame();
		}
		isInvincible = false;
	}
	public IEnumerator knockBackCoroutine(float duration, Vector2 direction, float knockback)
    {
		if (direction.x == 0)
		{
			direction = new Vector2(-1, 0);
		}
		isKnockedBack = true;
		targetKnockedBack = new Vector2(knockback * direction.x, 0);
		for (float t = 0; t<duration; t += Time.deltaTime)
		{
			controller.Move(targetKnockedBack * Time.deltaTime * dashSpeed, direction);
			yield return new WaitForEndOfFrame();
		}
		isKnockedBack = false;
	}

    public void OnSceneLoaded()
    {
		currentHp = maxHp;
		animeThor.SetTrigger("Idle");
		gameHandler.isRunning = true;
		transform.position = new Vector3(-14, 1, 0);
	}

	public void scaleProperly()
    {
			transform.localScale = new Vector3(1, 1, 1);
			controller.CalculateRaySpacing();
			controller.UpdateRaycastOrigins();
	}
}
