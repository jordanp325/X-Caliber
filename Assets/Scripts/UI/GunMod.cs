using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunMod : MonoBehaviour {
    //UI refrences
    public Module Active;
    public Image display;
    public Text[] stats;

    public void Start()
    {
        if (Active != null) UpdateGun();
        else Vars.GunRarity = 0;
    }

    //is called when the input slot is clicked on
    public void ClickEvent()
    {
        //checks if holding a mod
        if (Vars.vars.MF.held == 2)
        {
            //changes the mod in the gun and drops the parts on the floor
            if (Active != null) {
                foreach (Part P in Active.parts)
                {
                    //changes the amount based on rarity
                    P.amount = (int)Mathf.Round(P.amount * ((.25f * Common.GetRarity(P.type)) + .25f));
                    if (P != null && P.amount > 0)
                    {
                        //creates the new pickup
                        GameObject Pickup = Instantiate(Vars.vars.PickUpPrefab);
                        Pickup.GetComponent<PartPU>().Contained = P;
                        Pickup.transform.position = Vars.vars.Player.gameObject.transform.position;
                    }
                }
            }
            //moves the mods around
            Active = Vars.vars.MF.ContainsM;
            Vars.vars.MF.ContainsM = null;
            //calls the gun update
            UpdateGun();
        }
    }

    //to update the gun if the mod is changed
    public void UpdateGun()
    {
        //moves the accessor up one
        Vars.GunModNumber++;
        //changes the sprite of the mod in the gun
        display.sprite = Active.sprite;
        //changes the rarity shown in the vars script
        Vars.GunRarity = Active.rarity;
        //clears all existing parts
        Vars.GunModActiveParts[Vars.GunModNumber] = new int[Vars.NumberOfParts];
        //turn off all stats
        foreach (Text T in stats)
        {
            T.text = "";
        }
        //loops for each supposed part in the mod
        for (int i = 0; i < 6; i++)
        {
            //checks if it contains another part
            if (Active.parts.Length <= i)
            {
                stats[i].text = "";
                break;
            }
            else if (Active.parts[i] == null)
            {
                stats[i].text = "";
            }
            else
            {
                //changes the labels
                stats[i].text = Common.GetEffect2(Active.parts[i]);
                //changes the log accessor
                Vars.GunModActiveParts[Vars.GunModNumber][Active.parts[i].type] = Active.parts[i].amount;
            }
        }

    }

}
