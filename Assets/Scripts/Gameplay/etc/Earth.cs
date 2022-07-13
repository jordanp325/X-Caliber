using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    //variable setting the durability of the earth
    public int Durability;
    //variables for the frame counters
    public int FrameCounter;
    public int DurabilityCounter;

    //called whenever the wall is collided with
    void OnCollisionEnter2D(Collision2D collision)
    {
        //checks if its a bullet
        if (collision.gameObject.layer == 10)
        {
            //checks if the bullet has any bullet durability
            if (collision.gameObject.GetComponent<Bullet>().durability > 0)
            {
                //lowers the durability by 1
                collision.gameObject.GetComponent<Bullet>().durability--;
                //finds the angle of impact
                float StartingAngle = collision.gameObject.transform.localRotation.eulerAngles.z;
                float angle;
                //sets the angle after the bounce based on if it is within the left an right borders of the box
                if (
                    gameObject.transform.position.x - collision.contacts[0].point.x > (-1 * gameObject.GetComponent<BoxCollider2D>().size.x) / 2.3 &&
                    gameObject.transform.position.x - collision.contacts[0].point.x < gameObject.GetComponent<BoxCollider2D>().size.x / 2.3)
                {
                    angle = StartingAngle * -1;
                }
                else
                {
                    angle = (((StartingAngle) + 90) * -1) - 90;
                }
                //now sets the angle of the bullet based on those calculations
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                collision.gameObject.GetComponent<Bullet>().direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            }
            else
            {
                //if the bullet has no durrability, destroys the bullet
                Destroy(collision.gameObject);

            }
            //lowers durability
            Durability--;
        }
        else if (collision.gameObject.layer == 12)
        {
            //if it is an enemy bullet, it destroys it and lowers durability
            Destroy(collision.gameObject);
            Durability--;
        }
    }

    void FixedUpdate()
    {
        //incrementing the frame timer
        FrameCounter++;
        //checking if it has been a second yet
        if (FrameCounter >= (int)(1 / Time.deltaTime))
        {
            //resets the frame counter
            FrameCounter = 0;

            //increments the durability counter and checks if it has run out of time
            DurabilityCounter++;
            if (DurabilityCounter == 5)
            {
                //lowers durability and resets timer
                DurabilityCounter = 0;
                Durability--;
            }
        }

        //checks if it has lived its life
        if (Durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
