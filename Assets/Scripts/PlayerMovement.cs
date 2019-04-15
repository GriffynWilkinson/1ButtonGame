using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    PlayerInput m_Input = new PlayerInput();
    Rigidbody2D m_Body;
    float m_ForwardSpeed = 300;
    float m_FlipSpeed = 500;
    float m_YVelocity = 0;
    bool isDead = false;
    
	// Use this for initialization
	void Start () {
        m_Input.HandlePress += FlipPlayer;
        m_Body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead)
        {
            MoveForward();
            m_Input.HandleInput();
        }
	}

    void FlipPlayer()
    {
        // add velocity to move the player to either the roof or floor
        m_YVelocity = transform.up.y * m_FlipSpeed * Time.deltaTime;

        // flip the player over
        transform.Rotate(transform.eulerAngles.x + 180, 0, 0);

    }

    // add a constant forward velocity to the player
    void MoveForward()
    {
        m_Body.velocity = new Vector2(m_ForwardSpeed * Time.deltaTime, m_YVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Floor")
        {
            m_YVelocity = 0;
        }

        if (collision.collider.gameObject.tag == "Spikes")
        {
            Die();
        }
    }


    void Die()
    {
        isDead = true;

        m_ForwardSpeed = 0;
        m_YVelocity = 0;

        m_Body.velocity = new Vector2(m_ForwardSpeed * Time.deltaTime, m_YVelocity);

        m_Body.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

        GetComponent<BoxCollider2D>().enabled = false;
    }

}
