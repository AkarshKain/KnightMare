using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameScene GameScene;
    public Animator animator;
    public Player Player;
    bool setuped = false;
    public int health = 1;
    public int maxHealth = 1;
    public Transform HealthBar;
    public Transform HealthBarInner;
    public float HealthBarMaxX = 0.9f;

    bool isAttacking = false;
    public bool isDead = false;
    public float timeToDestroy = 4f;
    float destroyTimeStamp = 0;
    public float threathDistance = 3f;
    public float Speed = 0.1f;
    public float Lerp_Speed = 2f;

    public MaceHitBox hitBox;
    public List<AudioSource> deathSounds = new List<AudioSource>();
    public GameObject DeathParticles;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameScene.gamePaused)
        {
            return;
        }
        if (setuped && !isDead)
        {
            if (isAttacking)
            {

            } else
            {
                if (Vector3.Distance(Player.transform.position, transform.position) > threathDistance)
                {
                    animator.SetBool("isWalking", true);
                    Vector3 directionToPlayer = Player.transform.position - transform.position;
                    transform.position += directionToPlayer.normalized* Speed;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), Time.deltaTime * Lerp_Speed);

                } else
                {
                    animator.SetBool("isWalking", false);
                    animator.SetInteger("isAttacking", Random.Range(1, 3));
                    hitBox.canHit = true;
                    isAttacking = true;
                }
            }
        }
	}

    private void FixedUpdate()
    {
        HealthBar.LookAt(GameScene.GeneralCamera.transform);
        if (isDead)
        {
            destroyTimeStamp += Time.deltaTime;
            if (destroyTimeStamp >= timeToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Setup(int health)
    {
        setuped = true;
        this.health = health;
        maxHealth = health;
    }

    public void BloodHit()
    {
        if (health > 0)
        {
            --health;
            if (health <= 0)
            {
                isDead = true;
                HealthBar.gameObject.SetActive(false);
                animator.Play("Death");
                GameScene.RefreshScore();
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                hitBox.canHit = false;
                deathSounds[Random.Range(0, deathSounds.Count)].Play();
                DeathParticles.SetActive(true);
            } else
            {
                HealthBarInner.localScale = new Vector3((float)((float)health / (float)maxHealth)  * HealthBarMaxX, HealthBarInner.localScale.y, HealthBarInner.localScale.z);
            }
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetInteger("isAttacking", 0);
    }
}
