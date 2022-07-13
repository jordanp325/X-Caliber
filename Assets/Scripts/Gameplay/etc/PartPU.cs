using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPU : PickUp
{
    //var for UI refrence
    public SpriteRenderer Display;
    //the contained part
    public Part Contained;
    //bools monitoring if there is space
    private bool Space;
    private bool last;

    // Use this for initialization
    public override void Start()
    {
        //sets the sprite for the object
        Display.sprite = Vars.vars.DroppedParts[Contained.type];
        //sets the variable for the last change
        last = !Vars.vars.UI.changed;
    }

    // Update is called once per frame
    public override void Update()
    {
        //checks if it was changed recently
        if (last != Vars.vars.UI.changed)
        {
            //goes through each and every slot seeing if there is a spot for the item
            for (int i = 0; i < (Vars.InventoryParts.Length - 1); i++)
            {
                if (Vars.InventoryParts[i] == null || Vars.InventoryParts[i].type == Contained.type)
                {
                    Space = true;
                    break;
                }
            }
        }
    }

    // FixedUpdate is called once per fixed frame
    public override void FixedUpdate()
    {
        //checks if there is space in the inventory
        if (Space)
        {
            //calculates the distance
            float distance = Vector2.Distance(gameObject.transform.position, Vars.vars.Player.gameObject.transform.position);
            if (distance <= .25f)
            {
                //adds the part to the inventory if it's close enough
                int index = 0;
                bool nul = true;
                for (int i = (Vars.InventoryParts.Length - 2); i >= 0; i--)
                {
                    if (Vars.InventoryParts[i] != null && Vars.InventoryParts[i].type == Contained.type)
                    {
                        Vars.InventoryParts[i].amount += Contained.amount;
                        nul = false;
                        break;
                    }
                    else if (Vars.InventoryParts[i] == null)
                    {
                        index = i;
                    }
                }
                //if it was an empty slot, it fills the empty slot
                if (nul)
                    Vars.InventoryParts[index] = Contained;
                //loads the change and sets it to changed
                Vars.vars.UI.SaveLoad(false);
                Vars.vars.UI.changed = !Vars.vars.UI.changed;
                //destroys the gameobject
                Destroy(gameObject);
            }
            else if (distance <= Vars.vars.PickUpDistance)
            {
                //moves the pickup closer if it is too far away, and increases speed the closer you are to it
                Vector3 force = Vars.vars.Player.gameObject.transform.position - gameObject.transform.position;
                gameObject.GetComponent<Rigidbody2D>().velocity = (force / (force.magnitude * force.magnitude)) * 5;
            }
        }
    }
}
