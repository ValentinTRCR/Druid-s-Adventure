using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public GameObject Druide;
    Rigidbody2D rb;
    DetectionSol detectionSol;
    private float speed = 5f;
    private float jumpForce = 6f;

    bool jump = true;

    private float movey;



    bool isJumping;
    bool isFalling;
    bool isWalking;

    CapsuleCollider2D capsuleCollider2D;

    float offsetxRight;
    float offsetxLeft = 0.06f;

    Animator anim;

    DetectionInteraction detectionInteraction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = Druide.GetComponent<Rigidbody2D>();
        detectionSol = Druide.GetComponentInChildren<DetectionSol>();
        capsuleCollider2D = Druide.GetComponent<CapsuleCollider2D>();
        offsetxRight = capsuleCollider2D.offset.x;
        anim = Druide.GetComponentInChildren<Animator>();
        detectionInteraction = Druide.GetComponentInChildren<DetectionInteraction>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsInWater)
        {
            rb.gravityScale = 2f;
            rb.linearVelocity = new Vector2(0, 0);
        }
        else
        {
            rb.gravityScale = 1f;
            rb.linearVelocity = new Vector2(movex * speed, rb.linearVelocity.y);
        }
        gererAnimation();
    }

    void OnMove(InputValue value)
    {
        Vector2 d = value.Get<Vector2>();
        movex = d.x;
        movey = d.y;
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && detectionSol.ToucheLeSol)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump = true;
        }
    }

    void OnInteract(InputValue value)
    {
        if(value.isPressed)
        {
            GameObject objectToCollect = detectionInteraction.ObjectToCollect;
            if(objectToCollect != null && objectToCollect.name == "crystal")
            {
                objectToCollect.transform.SetParent(Druide.transform);
                objectToCollect.GetComponentInChildren<SpriteRenderer>().enabled = false;
                objectToCollect.GetComponent<Collider2D>().enabled = false;
                objectToCollect.transform.Find("Spot Light 2D").gameObject.SetActive(false);
            }
            else if(objectToCollect.name == "Levier" && objectToCollect.CompareTag("Interact"))
            {
                objectToCollect.GetComponent<Levier>().isActivated = true;
            }else if(objectToCollect.name =="depotCrystal" && objectToCollect.CompareTag("Interact"))
            {
                Transform crystal = Druide.transform.Find("crystal");
                if(crystal != null)
                {
                    crystal.SetParent(objectToCollect.transform);
                }else
                {
                    Debug.Log("No crystal to deposit");
                }
                
                

            }
        }
    }

    public void gererAnimation()
    {
        if (!Hurt)
        {


            if (movex > 0)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = false;
                capsuleCollider2D.offset = new Vector2(offsetxRight, capsuleCollider2D.offset.y);
            }
            else if (movex < 0)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = true;
                capsuleCollider2D.offset = new Vector2(offsetxLeft, capsuleCollider2D.offset.y);

            }


            isJumping = rb.linearVelocity.y > 0.1f;
            isFalling = rb.linearVelocity.y < -0.1f;
            isWalking = movex != 0;

            anim.SetBool("IsJumping", isJumping);

            anim.SetBool("IsFalling", isFalling);
            anim.SetBool("IsWalking", isWalking && !isJumping && !isFalling);
        }
        else
        {
            anim.SetTrigger("Hurt");
            Hurt = false;
        }

    }
}
