using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameScene GameScene;
    public float Speed = 1f;
    public Animator animator;
    bool canBeMoved = false;
    bool isAttacking = false;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    float yaw = 0.0f;
    float pitch = 0.0f;

    public int Health = 100;
    public Text HealthText;
    public Slider HealthSlider;
    public Image HealthBar;

    public bool isDead = false;

    public KatanaHitBox katanaHitBox;
    public AudioSource PlayerDeathSound;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameScene.gamePaused)
        {
            return;
        }
        if (canBeMoved && !isDead)
        {
            if (!isAttacking)
            {
                bool w = Input.GetKey(KeyCode.W);
                bool s = Input.GetKey(KeyCode.S);
                bool a = Input.GetKey(KeyCode.A);
                bool d = Input.GetKey(KeyCode.D);

                if (w)
                {
                    transform.position += transform.forward * Speed;
                    animator.SetBool("isWalking", true);
                } else if (s)
                {
                    animator.SetBool("isWalking", true);
                    transform.position -= transform.forward * Speed;

                }
                if (a)
                {
                    animator.SetBool("isWalking", true);
                    transform.position -= transform.right * Speed;

                } else if (d)
                {
                    animator.SetBool("isWalking", true);
                    transform.position += transform.right * Speed;
                }
                if(!w && !s && !a && !d)
                {
                    animator.SetBool("isWalking", false);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetInteger("isAttacking", Random.Range(1, 4));
                    katanaHitBox.canHit = true;
                    isAttacking = true;
                    animator.SetBool("isWalking", false);
                }
            }
        }
        if (!isDead)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            //pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yaw, 0.0f);
        }
    }

    public void CanMove()
    {
        canBeMoved = true;
        animator.SetBool("canMove", true);
    }

    public void EndAttack()
    {
        animator.SetInteger("isAttacking", 0);
        isAttacking = false;
        katanaHitBox.canHit = false;
    }

    public void BloodHit()
    {
        if (Health > 0)
        {
            --Health;
            if (Health <= 0)
            {
                isDead = true;
                HealthBar.gameObject.SetActive(false);
                animator.Play("Death");
                PlayerDeathSound.Play();
            } else
            {
                HealthSlider.value = Health / 100f;
                HealthText.text = "Health: "+Health;
            }
        }
    }
}
