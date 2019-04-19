using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to add spikes to the block if there is nothing infront of it at the start of the game
public class CreateSpikes : MonoBehaviour
{
    public GameObject m_Spikes;
    float m_Length;

    // Start is called before the first frame update
    void Start()
    {
        
        // this is the length of one side of a block
        // calculated by the scale of the box collider (doesn't matter what axis we use as they are both the same)
        // multiplied by the scale of the local transform, incase the object is scaled.
        m_Length = (gameObject.GetComponent<BoxCollider2D>().size.x) * (transform.localScale.x);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.left, m_Length);

        if (hitInfo.transform == null)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            pos += Vector2.left * m_Length;
            Instantiate(m_Spikes, pos, Quaternion.identity);
        }
    }

}
