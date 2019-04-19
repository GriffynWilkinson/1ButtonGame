using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    PlayerInput m_Input = new PlayerInput();
    Rigidbody2D m_Body;
    float m_ForwardSpeed = 75f;
    float m_FlipSpeed;
    float m_YVelocity = 0;
    bool isDead = false;

    // Use this for initialization
    void Start()
    {
        m_FlipSpeed = m_ForwardSpeed * 1.5f;
        m_Input.HandlePress += FlipPlayer;
        m_Body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            MoveForward();
            m_Input.HandleInput();
        }
    }

    void FlipPlayer()
    {
        // Make sure the player is touching the ground or roof so he can't flip in mid air
        float length;

        length = (gameObject.GetComponent<BoxCollider2D>().size.y) * (transform.localScale.y);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, length);

        if (hitInfo.transform == null)
        {
            return;
        }

        // add velocity to move the player to either the roof or floor
        m_YVelocity = transform.up.y * m_FlipSpeed;

        // flip the player over
        transform.Rotate(transform.eulerAngles.x + 180, 0, 0);

    }

    // add a constant forward velocity to the player
    void MoveForward()
    {
        Debug.Log(m_Body.velocity);
        m_Body.velocity = new Vector2(m_ForwardSpeed, m_YVelocity) * Time.deltaTime;
        Debug.Log(m_Body.velocity);
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
