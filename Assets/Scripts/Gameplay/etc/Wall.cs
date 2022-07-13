using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    
    //called whenever the wall is collided with
    void OnCollisionEnter2D(Collision2D collision)
    {
        //checks if its a bullet
        if (collision.gameObject.layer == 10)
        {
            //checks if the bullet has any bullet durability
            if (collision.gameObject.GetComponent<Bullet>().durability > 0 && collision.gameObject.GetComponent<Bullet>().Ricochet)
            {
                //lowers the durability by 1
                collision.gameObject.GetComponent<Bullet>().durability--;
                //finds the angle of impact
                float StartingAngle = collision.gameObject.transform.localRotation.eulerAngles.z;
                float angle;
                //sets the angle after the bounce based on if it is within the left and right borders of the box
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
                collision.gameObject.GetComponent<Bullet>().Damage *= (2f / 3f);
                collision.gameObject.GetComponent<Bullet>().Ricochet = false;
            }
            else if (collision.gameObject.GetComponent<Bullet>().Ricochet)
            {
                //if the bullet has no durrability, destroys the bullet
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.layer == 12)
        {
            if (collision.gameObject.GetComponent<EnemyBullet>().durability > 0)
            {
                //lowers the durability by 1
                collision.gameObject.GetComponent<EnemyBullet>().durability--;
                //finds the angle of impact
                float StartingAngle = collision.gameObject.transform.localRotation.eulerAngles.z;
                float angle;
                //sets the angle after the bounce based on if it is within the left and right borders of the box
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
                collision.gameObject.GetComponent<EnemyBullet>().direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            }
            else
                Destroy(collision.gameObject);
        }
    }
}
