using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    public int hits;
    int durability;
    int counter = 0;
    //this triggers when the collider of the object has a trigger object enter its borders(reffered to as "other")
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //checks if it is a player bullet
        if (collision.gameObject.layer == 10)
        {
            Destroy(collision.gameObject);
            counter = 150;
            durability++;
            if (durability == hits) {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void FixedUpdate()
    {
        counter--;
        if (counter == 0)
        {
            durability = 0;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
