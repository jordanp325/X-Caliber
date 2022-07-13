using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //the direction of the bullet and the damage it will do
    public Vector2 direction;
    public int damage;
    public float speed;
    public int durability;

    // Update is called once per frame
    void FixedUpdate ()
    {
        ////the bullet moves
        //gameObject.transform.position = new Vector3(
        //    gameObject.transform.position.x + (direction.x * Vars.vars.EnemyBulletSpeed),
        //    gameObject.transform.position.y + (direction.y * Vars.vars.EnemyBulletSpeed),
        //    gameObject.transform.position.z);

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
