using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController_Platform : MonoBehaviourPun
{
    Animator anim;

    [Header("Rotation speed")]
    public float speed_rot;

    [Header("Movement speed during jump")]
    public float speed_move;

    [Header("Time available for combo")]
    public int term;

    public bool isJump;

    private float maxHp = 100f;
    public float hp;

    public float myDamage;

    public bool isAction;

    public bool isSkill;

    public bool dashAttackKey;

    private Rigidbody playerRigid;

    public PlayerState currentState = PlayerState.Idle;
    private void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        anim = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody>();
        hp = maxHp;
        StartCoroutine(Block());
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        Rotate();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (!isJump)
        {            
            Attack();
            
            Dodge();
            
            Jump();

            Crouch();

            Skill1();
            
            Skill2();
            
            Skill3();
            
            Skill4();
            
            Skill5();
            
            Skill6();
            
            Skill7();
            
            Skill8();
        }
    }

    Quaternion rot;
    bool isRun;

    
    void Rotate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Move();
            rot = Quaternion.LookRotation(Vector3.right);
        }


        else if (Input.GetKey(KeyCode.A))
        {
            Move();
            rot = Quaternion.LookRotation(Vector3.left);
        }

        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed_rot * Time.deltaTime);

    }

    
    void Move()
    {
        if (isJump)
        {
            transform.position += transform.forward * speed_move * Time.deltaTime;
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);

        }
        else
        {
            anim.SetBool("Run", true);
            anim.SetBool("Walk", Input.GetKey(KeyCode.LeftShift));
        }
    }

    int clickCount;
    float timer;
    bool isTimer;

    
    void Attack()
    {
        
        if (isTimer)
        {
            timer += Time.deltaTime;
        }

        if(isJump) return;
        if (isAction) return;
        if (isSkill) return;

        if (Input.GetKeyDown(KeyCode.J) && !Input.GetKey(KeyCode.W))
        {
            isAction = true;
            switch (clickCount)
            {
                
                case 0:
                    
                    anim.SetTrigger("Attack1");
                    
                    isTimer = true;
                    
                    clickCount++;
                    break;

                
                case 1:
                    
                    if (timer <= term)
                    {                        
                        anim.SetTrigger("Attack2");
                        
                        clickCount++;
                    }

                    
                    else
                    {                        
                        anim.SetTrigger("Attack1");
                        
                        clickCount = 1;
                    }

                    
                    timer = 0;
                    break;

                
                case 2:
                    
                    if (timer <= term)
                    {                        
                        anim.SetTrigger("Attack3");
                        
                        clickCount = 0;
                        
                        isTimer = false;
                    }

                    
                    else
                    {                        
                        anim.SetTrigger("Attack1");
                        
                        clickCount = 1;
                    }
                
                    timer = 0;
                    break;
            }
        }
    }

    
    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Dodge");
            isAction = false;
            isSkill = false;
        }
    }
        
    IEnumerator Block()
    {
        while(true)
        {
            if (Input.GetKey(KeyCode.O))
            {
                anim.SetBool("Block", true);
                yield return new WaitForSeconds(0.25f);
                anim.SetBool("Block", false);
                isAction = false;
                isSkill = false;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Crouch()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("Crouch", false);
        }
        if (isSkill) return;
        if (isAction) return;

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("Crouch", true);
        }
    }


    void Jump()
    {
        if (isAction) return;

        if (!Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.W))
        {            
            anim.SetBool("Block", false);
            anim.SetBool("Crouch", false);
            anim.SetTrigger("Jump");

            isJump = true;
            isAction = false;
        }
    }

    
    void JumpEnd()
    {
        isJump = false;
    }
    void ActionEnd()
    {
        isAction = false;
    }
    void SkillEnd()
    {
        isSkill = false;
    }

    // Skill1
    void Skill1()
    {
        if (isSkill) return;

        if (Input.GetKeyDown(KeyCode.W) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && Input.GetKey(KeyCode.J))
        {
            isAction = true;
            isSkill = true;
            // Play Skill1 animation
            anim.SetTrigger("Skill1");
        }
    }
    // Skill2
    void Skill2()
    {
        if (isSkill) return;

        if (Input.GetKeyDown(KeyCode.U))
        {
            isAction = true;
            isSkill = true;
            // Play Skill2 animation
            anim.SetTrigger("Skill2");
        }
    }
    // Skill3
    void Skill3()
    {
        if (isSkill) return;

        if (Input.GetKeyDown(KeyCode.K))
        {
            isAction = true;
            isSkill = true;
            // Play Skill3 animation
            anim.SetTrigger("Skill3");
        }
    }
    // Skill4
    void Skill4()
    {
        if (isSkill) return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            isAction = true;
            isSkill = true;
            // Play Skill4 animation
            anim.SetTrigger("Skill4");
        }
    }
    // Skill5
    void Skill5()
    {
        if (isSkill) return;

        if (Input.GetKeyDown(KeyCode.L))
        {
            isAction = true;
            isSkill = true;
            // Play Skill5 animation
            anim.SetTrigger("Skill5");
        }
    }
    // Skill6
    void Skill6()
    {
        if (isSkill) return;

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && Input.GetKey(KeyCode.J))
        {
            dashAttackKey = true;

            if (!Input.GetKey(KeyCode.W))
            {
                isAction = true;
                isSkill = true;
                // Play Skill6 animation
                anim.SetTrigger("Skill6");
            }
        }
    }
    // Skill7
    void Skill7()
    {
        if (isSkill) return;

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.J))
        {
            isAction = true;
            isSkill = true;
            // Play Skill7 animation
            anim.SetTrigger("Skill7");
        }
    }
    // Skill8
    void Skill8()
    {
        if (isSkill) return;

        if (Input.GetKey(KeyCode.W) && dashAttackKey)
        {
            anim.SetTrigger("Skill8");
            isAction = true;
            isSkill = true;
            isJump = false;
            dashAttackKey = false;            
        }
    }
    public void getDamage(float damage)
    {
        if(hp >= damage)
        {
            hp -= damage;
        }
        else
        {
            hp = 0;
        }
        if(hp > 0)
        {
            
        }
    }
}
