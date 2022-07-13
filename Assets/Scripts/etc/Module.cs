using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module {
    //info on the mod
    public Part[] parts;
    public Sprite sprite;
    public int rarity;

    //this is the only constructor, no empty mods
    public Module(Part[] Parts)
    {
        parts = Parts;
        int[] rareties = new int[4] { 0, 0, 0, 0 };
        //looping through the rarities of the mods to get totals of each
        foreach (Part P in parts)
        {
            rareties[Common.GetRarity(P.type)]++;
        }
        //then finds the rarety based on the highest rarety of parts
        rarity = 0;
        for (int i = 3; i >= 0; i--)
        {
            if (rareties[i] >= 1)
            {
                rarity = i;
                break;
            }
        }
        //sets the sprite based on the rarity found
        sprite = Vars.vars.Mods[rarity];
    }
}
