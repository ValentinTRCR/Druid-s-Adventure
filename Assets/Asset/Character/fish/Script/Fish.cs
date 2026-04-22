using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fish : Entity
{
    public GameObject fish;

    Rigidbody2D rb;

    Animator anim;

    float movey;

    float speed = 7f;

    float acceleration = 4f;

    Vector2 VelocityCourante;

    float rotationSpeed = 200f;

    SpriteRenderer spriteRenderer;

 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = fish.GetComponent<Rigidbody2D>();
        anim = fish.GetComponentInChildren<Animator>();
        spriteRenderer = fish.GetComponentInChildren<SpriteRenderer>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsInWater)
        {
            rb.gravityScale = 0f;
            Vector2 VitesseVoulue = new Vector2(movex * speed,movey * speed);
            VelocityCourante = Vector2.Lerp(VelocityCourante, VitesseVoulue, acceleration * Time.deltaTime);
            rb.linearVelocity = VelocityCourante;

            if(Mathf.Abs(movex) > 0.1 || Mathf.Abs(movey) > 0.1)
            {
               float angleVise = Mathf.Atan2(movey, movex) * Mathf.Rad2Deg;
               float angleActuel = fish.transform.rotation.eulerAngles.z;

               float nouvelAngle = Mathf.MoveTowardsAngle(angleActuel, angleVise, rotationSpeed * Time.deltaTime);
               fish.transform.rotation = Quaternion.Euler(0, 0, nouvelAngle);
            }

        }
        else
        {
           rb.gravityScale = 1f;
           rb.linearVelocity = new Vector2(0,rb.linearVelocityY);
        }

        GererAnimation();
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 d = inputValue.Get<Vector2>();
        movex = d.x;
        movey = d.y;
        
    }

    void GererAnimation()
    {
        if(fish.transform.rotation.eulerAngles.z > 90 && fish.transform.rotation.eulerAngles.z < 270)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }

        //anim.SetBool("IsSwimming", (movex > 0.1 || movex < -0.1 || movey > 0.1 || movey < -0.1) && IsInWater);
    }

    
}
