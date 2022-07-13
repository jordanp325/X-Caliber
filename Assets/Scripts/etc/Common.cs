using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Common
{
    //simple switch method for returning the rarities
    public static int GetRarity(int part_num)
    {
        //rarity:
        // 0 is common
        // 1 is uncommon
        // 2 is rare
        // 3 is epic
        switch (part_num)
        {
            case 0:
                return 0;
            case 1:
                return 0;
            case 2:
                return 1;
            case 3:
                return 3;
            case 4:
                return 1;
            case 5:
                return 2;
            case 6:
                return 3;
            case 7:
                return 2;
            case 8:
                return 2;
            case 9:
                return 0;
            case 10:
                return 1;
            case 11:
                return 1;
            case 12:
                return 3;
            case 13:
                return 0;
            case 14:
                return 0;
            case 15:
                return 2;
            case 16:
                return 1;
            case 17:
                return 0;
            case 18:
                return 1;
            case 19:
                return 3;
            case 20:
                return 3;
            case 21:
                return 1;
            case 22:
                return 3;
            case 23:
                return 0;
            case 24:
                return 2;
            case 25:
                return 3;
            case 26:
                return 2;
            case 27:
                return 2;
            case 28:
                return 2;
            case 29:
                return 2;
            default:
                return 0;
        }
    }

    //get the number of basic/advanced enemies spawning based on the floor number
    public static int EnemiesToSpawn(int FloorNum)
    {
        switch (FloorNum)
        {
            case 0:
                return 3;
            case 1:
                return 4;
            case 2:
                return 5;
            case 3:
                return 6;
            default:
                return -1;
        }
    }

    //get the rarity of uniques based on floor
    public static int UniqueRarity(int FloorNum)
    {
        switch (FloorNum)
        {
            case 0:
                return 20;
            case 1:
                return 30;
            case 2:
                return 40;
            case 3:
                return 50;
            default:
                return -1;
        }
    }

    //get the index of a sprite array from the angle of the model in-game
    public static int GetSpriteIndex(float angle)
    {
        if (angle < -120)
        {
            return 2;
        }
        else if (angle < -60)
        {
            return 0;
        }
        else if (angle < 0)
        {
            return 4;
        }
        else if (angle < 60)
        {
            return 5;
        }
        else if (angle < 120)
        {
            return 1;
        }
        else
        {
            return 3;
        }
    }

    //get the color of a rarity
    public static Color GetColor(int rarety)
    {
        //switching through all rarities
        switch (rarety)
        {
            //rarety
            //common: black
            //uncommon: dark red
            //rare: dark bluish purble
            //epic: cyan
            case 0:
                return new Color(.15f, .15f, .15f);
            case 1:
                return new Color(.6f, 0, 0);
            case 2:
                return new Color(.6f, 0, .8f);
            case 3:
                return new Color(0, .7f, 1);
            default:
                //returns yellow if input was not a rarity
                return Color.yellow;
        }
    }

    //get the crafting probability
    public static float GetProbability(int rarety)
    {
        //switch statement for rarity
        switch (rarety)
        {
            case 0:
                return .98f;
            case 1:
                return .95f;
            case 2:
                return .85f;
            case 3:
                return .65f;
            default:
                //if not a rarity input, it returns 1
                return 1;
        }
    }

    //get a description of the part based on the part number
    public static string GetEffect1(Part P)
    {
        //this is the variable containing how much of an effect it will have and one for rarity
        float effect = FullEffect(P);
        int R = GetRarity(P.type);
        string rarity = "";
        switch (R)
        {
            case 0:
                rarity = "Common";
                break;
            case 1:
                rarity = "Uncommon";
                break;
            case 2:
                rarity = "Rare";
                break;
            case 3:
                rarity = "Epic";
                break;
        }

        //switch for each part
        switch (P.type)
        {
            case 0:
                return "<B>" + "Bullet Velocity" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the velocity of your bullets by " + (effect).ToString("P0");
            case 1:
                return "<B>" + "Bullet Range " + "</B> [" + rarity + "]" + '\n' +
                    "Increases the range of your bullets by " + (effect).ToString("P0");
            case 2:
                return "<B>" + "Spread Shot" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets fire in a " + Vars.vars.SpreadAngle.ToString() + "° cone that contains " + (effect).ToString() + " bullets" + '\n' +
                    "Bullets deal a combined " + (P.amount + 19).ToString() + " damage" + '\n' +
                    "Halves firing speed";
            case 3:
                return "<B>" + "Ice Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>frozen</i> on impact for " + (effect).ToString() + " seconds" + '\n' +
                    "<i>Frozen</i>: Target is stunned, making them incapable of moving or attacking";
            case 4:
                return "<B>" + "Water Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with wet on impact for 3 seconds" + '\n' +
                    "(Removes fire debuff from enemies)" + '\n' +
                    "<i>Wet</i>: Slows target for " + (1 - effect).ToString("P0");
            case 5:
                return "<B>" + "Crafting Success" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the chance of module crafting success by " + (effect).ToString("P0") + " (additively)";
            case 6:
                return "<B>" + "Explosive Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets explode on impact" + '\n' +
                    "Explosions knock back and deal " + (effect).ToString() + " damage to nearby enemies";
            case 7:
                return "<B>" + "Bullet Durability" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets pierce and/or ricochet " + (effect).ToString() + " times" + '\n' +
                    "(Bullet damage gets reduced by 1/3 every time it pierces or bounces)";
            case 8:
                return "<B>" + "Homing Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets home in on enemies at a rate of " + (effect).ToString() + "° per second";
            case 9:
                return "<B>" + "Bullet Damage" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the damage of your bullets by " + (effect).ToString();
            case 10:
                return "<B>" + "Cluster Shot" + "</B> [" + rarity + "]" + '\n' +
                    "Your bullets shoot out " + (effect).ToString() + " cluster bullets upon killing an enemy" + '\n' +
                    "These bullets deal 50% damage, have 50% size, and have 50% range" + '\n' +
                    "If these bullets kill an enemy the effect is started again";
            case 11:
                return "<B>" + "Burst Shot" + "</B> [" + rarity + "]" + '\n' +
                    "Your bullets fire in bursts of " + (1 + (effect)).ToString() + '\n' +
                    "Bullets deal a combined " + (P.amount + 19).ToString() + " damage" + '\n' +
                    "Halves firing speed";
            case 12:
                return "<B>" + "Electric Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets have a 10% chance to shoot lighting to nearby enemies on impact" + '\n' +
                    "Lightning deals " + Vars.vars.LightningDamage.ToString() + " damage" + '\n' +
                    "Lighting strikes the enemy hit and " + (effect).ToString() + " nearby enemies" + '\n' +
                    "doubled against targets effected by the wet debuff";
            case 13:
                return "<B>" + "Magazine Size" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the capacity of your magazine by " + (effect).ToString() + " bullets";
            case 14:
                return "<B>" + "Bullet Accuracy" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the accuracy of your bullets by " + (1 - effect).ToString("P0");
            case 15:
                return "<B>" + "Armor Piercing Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Deals bonus damage equal to " + '\n' + (effect).ToString("P0") + " of enemies max HP on impact" + '\n' +
                    "This damage ignores all damage resistance," + '\n' +
                    "Decreased by 90% vs bosses"+ '\n' + "Decreased by 80% when using AoEs";
            case 16:
                return "<B>" + "Bullet Penetration" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets able to destroy " + (Mathf.Floor(effect)).ToString() + " enemy bullets";
            case 17:
                return "<B>" + "Reload Speed" + "</B> [" + rarity + "]" + '\n' +
                    "Decreases your reload speed by " + (1 - effect).ToString("P0");
            case 18:
                return "<B>" + "Automatic Fire" + "</B> [" + rarity + "]" + '\n' +
                    "Allows your gun to automatically fire by holding down left-click" + '\n' +
                    "(Increases your attack speed by " + (effect).ToString("P0") + ")";
            case 19:
                return "<B>" + "Fire Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>fire</i> on impact for 3 seconds" + '\n' +
                    "<i>Fire</i>: Target takes " + (effect).ToString() + " damage per second";
            case 20:
                return "<B>" + "Poison Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>poison</i> on impact for " + (effect).ToString() + " seconds" + '\n' +
                    "<i>Poison</i>: Target takes 3 damage per second";
            case 21:
                return "<B>" + "Knockback" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets knock enemies back (+" + (((effect - Vars.vars.StartingStrength[21]) / Vars.vars.StartingStrength[21])).ToString("P0") + ")";
            case 22:
                return "<B>" + "Shockwave Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets release a shockwave on impact, dealing 5 damage to enemies hit" + '\n' +
                    "(+" + (((effect - Vars.vars.StartingStrength[22]) / Vars.vars.StartingStrength[22])).ToString("P0") + " shockwave size)";
            case 23:
                return "<B>" + "Bullet Size" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the size of your bullets by " + (effect).ToString("P0");
            case 24:
                return "<B>"+ "Earthen Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>earthen</i> on impact for 3 seconds" + '\n' +
                    "<i>Earthen</i>: Target has a 10% chance to turn to stone upon death, stone has " + (effect).ToString() + " durability" + '\n' +
                    "Stone loses 1 durability every 5 seconds and upon getting hit by any bullet";
            case 25:
                return "<B>"+ "Vampiric Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Your bullets have a " + (effect).ToString("P0") + " chance to heal you for 1 HP upon killing an enemy";
            case 26:
                return "<B>"+ "Midas Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>midas</i> on impact for 3 seconds" + '\n' +
                    "Midas: Target drops " + (effect).ToString("P0") + " more Ӿ upon death (rounding up)" + '\n' +
                    "This effect has a 10% chance of going off";
            case 27:
                return "<B>"+ "Assassin Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets have a " + (effect).ToString("P0") + " chance to inflict enemies with <i>Vulnerable</i> on impact for 3 seconds" + '\n' +
                    "<i>Vulnerable</i>: The next attack against the target deals 100 bonus damage";
            case 28:
                return "<B>"+ "Holy Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>holy light</i> on impact for 3 seconds" + '\n' +
                    "<i>Holy Light</i>: Target deals " + (effect).ToString("P0") + " less damage with their bullets" + '\n' +
                    "<i>Holy Light</i> and <i>Unholy Darkness</i> cannot both be on a target";
            case 29:
                return "<B>" + "Unholy Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>unholy darkness</i> on impact for 3 seconds" + '\n' +
                    "<i>Unholy Darkness</i>: Target takes " + (effect).ToString("P0") + " more damage from bullets" + '\n' +
                    "<i>Holy Light</i> and <i>Unholy Darkness</i> cannot both be on a target";
            default:
                return "";
        }
    }

    //same as the method before except it has line breaks for the stat boxes
    public static string GetEffect2(Part P)
    {
        //this is the variable containing how much of an effect it will have and one for rarity
        float effect = FullEffect(P);
        int R = GetRarity(P.type);
        string rarity = "";
        switch (R)
        {
            case 0:
                rarity = "Common";
                break;
            case 1:
                rarity = "Uncommon";
                break;
            case 2:
                rarity = "Rare";
                break;
            case 3:
                rarity = "Epic";
                break;
        }

        //switch for each part
        switch (P.type)
        {
            case 0:
                return "<B>" + "Bullet Velocity" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the velocity of your bullets by " + (effect).ToString("P0");
            case 1:
                return "<B>" + "Bullet Range " + "</B> [" + rarity + "]" + '\n' +
                    "Increases the range of your bullets by " + (effect).ToString("P0");
            case 2:
                return "<B>" + "Spread Shot" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets fire in a " + Vars.vars.SpreadAngle.ToString() + "° cone that contains " + (effect).ToString() + " bullets" + '\n' +
                    "Bullets deal a combined " + (P.amount + 19).ToString() + " damage" + '\n' +
                    "Halves firing speed";
            case 3:
                return "<B>" + "Ice Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>frozen</i> on impact for " + (effect).ToString() + " seconds" + '\n' +
                    "<i>Frozen</i>: Target is stunned, making them incapable of moving or attacking";
            case 4:
                return "<B>" + "Water Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with wet on impact for 3 seconds" + '\n' +
                    "(Removes fire debuff from enemies)" + '\n' +
                    "<i>Wet</i>: Slows target for " + (1 - effect).ToString("P0");
            case 5:
                return "<B>" + "Crafting Success" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the chance of module crafting success by " + (effect).ToString("P0") + " (additively)";
            case 6:
                return "<B>" + "Explosive Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets explode on impact" + '\n' +
                    "Explosions knock back and deal " + (effect).ToString() + " damage to nearby enemies";
            case 7:
                return "<B>" + "Bullet Durability" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets pierce and/or ricochet " + (effect).ToString() + " times" + '\n' +
                    "(Bullet damage gets reduced by 1/3 every time it pierces or bounces)";
            case 8:
                return "<B>" + "Homing Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets home in on enemies at a rate of " + (effect).ToString() + "° per second";
            case 9:
                return "<B>" + "Bullet Damage" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the damage of your bullets by " + (effect).ToString();
            case 10:
                return "<B>" + "Cluster Shot" + "</B> [" + rarity + "]" + '\n' +
                    "Your bullets shoot out " + (effect).ToString() + " cluster bullets upon killing an enemy" + '\n' +
                    "These bullets deal 50% damage, have 50% size, and have 50% range" + '\n' +
                    "If these bullets kill an enemy the effect is started again";
            case 11:
                return "<B>" + "Burst Shot" + "</B> [" + rarity + "]" + '\n' +
                    "Your bullets fire in bursts of " + (1 + (effect)).ToString() + '\n' +
                    "Bullets deal a combined " + (P.amount + 19).ToString() + " damage" + '\n' +
                    "Halves firing speed";
            case 12:
                return "<B>" + "Electric Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets have a 10% chance" + '\n' + " to shoot lighting to nearby enemies on impact" + '\n' +
                    "Lightning deals " + Vars.vars.LightningDamage.ToString() + " damage" + '\n' +
                    "Lighting strikes the enemy hit" + '\n' + " and " + (effect).ToString() + " nearby enemies" + '\n' +
                    "doubled against targets effected by the wet debuff";
            case 13:
                return "<B>" + "Magazine Size" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the capacity of your magazine by " + (effect).ToString() + " bullets";
            case 14:
                return "<B>" + "Bullet Accuracy" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the accuracy of your bullets by " + (1 - effect).ToString("P0");
            case 15:
                return "<B>" + "Armor Piercing Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Deals bonus damage equal to " + '\n' + (effect).ToString("P0") + " of enemies max HP on impact" + '\n' +
                    "This damage ignores all damage resistance," + '\n' +
                    "Decreased by 90% vs bosses" + '\n' + "Decreased by 80% when using AoEs";
            case 16:
                return "<B>" + "Bullet Penetration" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets able to destroy " + (Mathf.Floor(effect)).ToString() + " enemy bullets";
            case 17:
                return "<B>" + "Reload Speed" + "</B> [" + rarity + "]" + '\n' +
                    "Decreases your reload speed by " + (1 - effect).ToString("P0");
            case 18:
                return "<B>" + "Automatic Fire" + "</B> [" + rarity + "]" + '\n' +
                    "Allows your gun to automatically fire by holding down left-click" + '\n' +
                    "(Increases your attack speed by " + (effect).ToString("P0") + ")";
            case 19:
                return "<B>" + "Fire Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>fire</i> on impact for 3 seconds" + '\n' +
                    "<i>Fire</i>: Target takes " + (effect).ToString() + " damage per second";
            case 20:
                return "<B>" + "Poison Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>poison</i> on impact for " + (effect).ToString() + " seconds" + '\n' +
                    "<i>Poison</i>: Target takes 3 damage per second";
            case 21:
                return "<B>" + "Knockback" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets knock enemies back (+" + (((effect - Vars.vars.StartingStrength[21]) / Vars.vars.StartingStrength[21])).ToString("P0") + ")";
            case 22:
                return "<B>" + "Shockwave Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets release a shockwave on impact, dealing 5 damage to enemies hit" + '\n' +
                    "(+" + (((effect - Vars.vars.StartingStrength[22]) / Vars.vars.StartingStrength[22])).ToString("P0") + " shockwave size)";
            case 23:
                return "<B>" + "Bullet Size" + "</B> [" + rarity + "]" + '\n' +
                    "Increases the size of your bullets by " + (effect).ToString("P0");
            case 24:
                return "<B>" + "Earthen Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>earthen</i> on impact for 3 seconds" + '\n' +
                    "<i>Earthen</i>: Target has a 10% chance to turn to " + '\n' + "stone upon death, stone has " + (effect).ToString() + " durability" + '\n' +
                    "Stone loses 1 durability every 5 seconds" + '\n' + " and upon getting hit by any bullet";
            case 25:
                return "<B>" + "Vampiric Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Your bullets have a " + (effect).ToString("P0") + " chance to heal you for 1 HP upon killing an enemy";
            case 26:
                return "<B>" + "Midas Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>midas</i> on impact for 3 seconds" + '\n' +
                    "Midas: Target drops " + (effect).ToString("P0") + " more Ӿ upon death (rounding up)" + '\n' +
                    "This effect has a 10% chance of going off";
            case 27:
                return "<B>" + "Assassin Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets have a " + (effect).ToString("P0") + " chance" + '\n' + " to inflict enemies with <i>Vulnerable</i> " + '\n' + "on impact for 3 seconds" + '\n' +
                    "<i>Vulnerable</i>: The next attack against the target deals 100 bonus damage";
            case 28:
                return "<B>" + "Holy Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>holy light</i> on impact for 3 seconds" + '\n' +
                    "<i>Holy Light</i>: Target deals " + (effect).ToString("P0") + " less damage with their bullets" + '\n' +
                    "<i>Holy Light</i> and <i>Unholy Darkness</i> cannot both be on a target";
            case 29:
                return "<B>" + "Unholy Bullets" + "</B> [" + rarity + "]" + '\n' +
                    "Makes your bullets inflict enemies with <i>unholy darkness</i> on impact for 3 seconds" + '\n' +
                    "<i>Unholy Darkness</i>: Target takes " + (effect).ToString("P0") + " more damage from bullets" + '\n' +
                    "<i>Holy Light</i> and <i>Unholy Darkness</i> cannot both be on a target";
            default:
                return "";
        }
    }

    //get the full effect of a certain part, basicly how much strength it has based on # of parts
    public static float FullEffect(Part P)
    {
        //checks if it is a multiplicative part
        if (P.type == 14 || P.type == 17 || P.type == 4)
        {
            if (P.amount == 0)
            {
                return 1;
            }
            return ((1 - Vars.vars.StartingStrength[P.type]) * Mathf.Pow(1 - Vars.vars.IncrementalStrength[P.type], (P.amount - 1)));
        }
        //checks if there is any parts at all
        if (P.amount > 0)
        {
            //returns a starting strength plus an incremental strength for each additional part
            return (Vars.vars.StartingStrength[P.type] + (Vars.vars.IncrementalStrength[P.type] * (P.amount - 1)));
        }
        else
        {
            //if no parts, returns no effect
            return 0;
        }
    }

    //same as the previous method but with the part type passed in (strength is based on the # of parts in the gun), instead of an entire part
    public static float FullEffect(int type, int access)
    {
        //checks if it is a multiplicative part
        if (type == 14 || type == 17 || type == 4)
        {
            if (Vars.GunModActiveParts[access][type] == 0)
            {
                return 1;
            }
            return ((1 - Vars.vars.StartingStrength[type]) * Mathf.Pow(1 - Vars.vars.IncrementalStrength[type], Vars.GunModActiveParts[access][type]));
        }
        //checks if there is any parts at all
        if (Vars.GunModActiveParts[access][type] > 0)
        {
            //returns a starting strength plus an incremental strength for each additional part
            return (Vars.vars.StartingStrength[type] + (Vars.vars.IncrementalStrength[type] * (Vars.GunModActiveParts[access][type] - 1)));
        }
        else
        {
            //if no parts, returns no effect
            return 0;
        }
    }

    //same as pervious version but assums that it is current
    public static float FullEffect(int type)
    {
        //checks if it is a multiplicative part
        if ((type == 14 || type == 17 || type == 4) && Vars.GunModActiveParts[Vars.GunModNumber][type] > 0)
        {
            if (Vars.GunModActiveParts[Vars.GunModNumber][type] == 0)
            {
                return 1;
            }
            return ((1 - Vars.vars.StartingStrength[type]) * Mathf.Pow(1 - Vars.vars.IncrementalStrength[type], Vars.GunModActiveParts[Vars.GunModNumber][type] - 1));
        }
        //checks if there is any parts at all
        if (Vars.GunModActiveParts[Vars.GunModNumber][type] > 0)
        {
            //returns a starting strength plus an incremental strength for each additional part
            return (Vars.vars.StartingStrength[type] + (Vars.vars.IncrementalStrength[type] * (Vars.GunModActiveParts[Vars.GunModNumber][type] - 1)));
        }
        else
        {
            //if no parts, returns no effect
            return 0;
        }
    }

    //method for storing the parts and mods in a varible to be communicated through different UI menus and possibly saved
    public static void SaveLoad(Container[] Parts, Container[] Mods, bool Saving)
    {
        //it loops once for each part in the inventory 
        for (int i = 0; i < Vars.vars.MaxParts; i++)
        {
            //checks if you want to save or load
            if (Saving) {
                //assigns each variable in the array to each corrisponding inventory slot
                Vars.InventoryParts[i] = Parts[i].Containsp;
            }
            else
            {
                //assigns each inventory slot based on the contents of the array
                Parts[i].Containsp = Vars.InventoryParts[i];
            }
        }

        //it loops once for each Mod in the inventory 
        for (int i = 0; i < 9; i++)
        {
            //checks if you want to save or load
            if (Saving)
            {
                //assigns each variable in the array to each corrisponding inventory slot
                Vars.InventoryMods[i] = Mods[i].Containsm;
            }
            else
            {
                //assigns each inventory slot based on the contents of the array
                Mods[i].Containsm = Vars.InventoryMods[i];
            }
        }
    }

    //method for spawning a new bullet given direction
    public static void SpawnBullet(float rotation, Vector3 position, float Damage, bool Cluster, GameObject avoider, int access)
    {
        //calcualtes the full error of each shot and lowers it to 0 if enough accuracy mods are contained in the gun
        float error = UnityEngine.Random.Range(-1 * Vars.vars.Inaccuracy, Vars.vars.Inaccuracy);
        if ((1 - FullEffect(14, access)) < 0)
        {
            error = 0;
        }
        //adding the error to the angle given
        rotation += error * FullEffect(14, access);
        //creating the bullet
        GameObject bullet = MonoBehaviour.Instantiate(Vars.vars.BulletPrefab, position, Quaternion.Euler(0, 0, rotation));
        //making it ignore the specified gameobject named "avoider"
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), avoider.GetComponent<Collider2D>());
        //set the basic properties such as sprite, direction, and scale
        bullet.GetComponent<Bullet>().direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * rotation), Mathf.Sin(Mathf.Deg2Rad * rotation));
        bullet.GetComponent<SpriteRenderer>().sprite = Vars.vars.Bullets[Vars.GunRarity];
        bullet.transform.localScale = new Vector3((1 + FullEffect(23, access)) * 2,(1 + FullEffect(23, access)) * 2, 1);
        //set the buffs/debuffs in the bullet based on the parts in the gun
        bullet.GetComponent<Bullet>().Damage = Damage;
        bullet.GetComponent<Bullet>().durability = (int)FullEffect(7, access);
        bullet.GetComponent<Bullet>().Cluster = Cluster;
        bullet.GetComponent<Bullet>().ModVersion = access;
        bullet.GetComponent<Bullet>().Pierce = 2 * (int)FullEffect(16, access);
        if (!Cluster)
            bullet.GetComponent<Bullet>().LifeTime = (int)(bullet.GetComponent<Bullet>().LifeTime * .5f);
    }

    //method for finding the closest enemy based on position
    public static GameObject GetClosestEnemy(Vector3 Position)
    {
        //declare the variables that keep track of the smallest distance found and the gameobject that holds that record
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        //loops through all enemies
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //calculates the distance and checks if it is lower than the loowest distance
            float dist = Vector3.Distance(t.transform.position, Position);
            if (dist < minDist)
            {
                //if so, sets the new lowest distance and sets the gameobject that holds that record
                tMin = t;
                minDist = dist;
            }
        }
        //return the lowest distance enemy
        return tMin;
    }

    //method for finding the closest enemy based on position and any enemies to avoid
    public static GameObject GetClosestEnemy(Vector3 Position, List<GameObject> Avoid)
    {
        //declare the variables that keep track of the smallest distance found and the gameobject that holds that record
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        //loops through all enemies
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //calculates the distance and checks if it is lower than the loowest distance
            float dist = Vector3.Distance(t.transform.position, Position);
            if (dist < minDist && !Avoid.Contains(t))
            {
                //if so, sets the new lowest distance and sets the gameobject that holds that record
                tMin = t;
                minDist = dist;
            }
        }
        //return the lowest distance enemy
        return tMin;
    }

    //to create the level
    public static Room[,] LevelGen()
    {
        bool[,] levelmap = new bool[Vars.vars.Levelsize, Vars.vars.Levelsize];

        // INSERT RANDOM LEVEL GEN HERE 
        for (int i = 0; i < levelmap.GetLength(0); i++ )
        {
            for (int j = 0; j < levelmap.GetLength(1); j++)
            {
                //true is accessable, false is not
                levelmap[i, j] = true;
            }
        }
        //THIS CODE IS ONLY TEMPORARY ^
        
        Vars.Levelmap = levelmap;
        Room[,] level = new Room[Vars.vars.Levelsize, Vars.vars.Levelsize];

        for (int i = 0; i < levelmap.GetLength(0); i++)
        {
            for (int j = 0; j < levelmap.GetLength(1); j++)
            {
                if (i == 0 && j == 0)
                {
                    level[i, j] = RoomGen(new Vector2[0] { }, new Vector2(i * (Vars.vars.Roomsize), j * (Vars.vars.Roomsize)), i, j);
                }
                else if (levelmap[i, j])
                {
                    Vector2[] Base = Level.Layouts[UnityEngine.Random.Range(0, Level.Layouts.Length)];
                    level[i, j] = RoomGen(Base, new Vector2(i * (Vars.vars.Roomsize), j * (Vars.vars.Roomsize)), i, j);
                }
            }
        }

        return level;
    }

    //method for building a room from a location
    public static Room RoomGen(Vector2[] Walls, Vector2 position, int indx, int indy)
    {
        Theme theme = Level.themes[UnityEngine.Random.Range(0, 2)];
        return RoomGen(Walls, theme, position, indx, indy);
    }

    //method for building a room from a location
    public static Room RoomGen(Vector2[] Walls, Theme theme, Vector2 position, int indx, int indy)
    {
        Room room = new Room(Walls, position + new Vector2(.5f, .5f), indx, indy);
        for (int i = 0; i < room.size; i++)
        {
            for (int j = 0; j < room.size; j++)
            {
                if (room.tiles[i,j])
                {
                    GameObject wall = MonoBehaviour.Instantiate(Vars.vars.WallPrefab);
                    wall.transform.position = new Vector2(position.x + i - (room.size / 2) + .5f, position.y + j - (room.size / 2) + 1.5f);
                    wall.transform.parent = room.manager.transform;
                    if (Getsuroundings(room.tiles, new Vector2(i,j)) == 0)
                    {
                        MonoBehaviour.Destroy(wall);
                    }
                    else
                    {
                        Vector2 index = GetTileIndex(room.tiles, new Vector2(i, j));
                        if (index.x == -1)
                            wall.GetComponent<SpriteRenderer>().enabled = false;
                        else
                            wall.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.Walls[(int)index.x, (int)index.y];
                        if (index == new Vector2(1, 2) || index == new Vector2(2, 1) || index == new Vector2(2, 2))
                        {
                            GameObject back = MonoBehaviour.Instantiate(Vars.vars.BackWallPrefab);
                            back.GetComponent<SpriteRenderer>().sprite = theme.SideWall;
                            back.transform.position = wall.transform.position - new Vector3(0, 1, 0);
                            back.transform.parent = room.manager.transform;
                        }
                    }
                }
                else
                {
                    GameObject Floor = MonoBehaviour.Instantiate(Vars.vars.FloorPrefab);
                    Floor.transform.position = new Vector2(position.x + i - (room.size / 2) + .5f, position.y + j - (room.size / 2) + .5f);
                    Floor.transform.parent = room.manager.transform;
                    if (Getsuroundings(room.tiles, new Vector2(i, j)) == 8)
                    {
                        int rand = UnityEngine.Random.Range(1, 101);
                        if (rand <= 1) 
                            Floor.GetComponent<SpriteRenderer>().sprite = theme.Floor[3];
                        else if (rand <= 11)
                            Floor.GetComponent<SpriteRenderer>().sprite = theme.Floor[2];
                        else if (rand <= 21)
                            Floor.GetComponent<SpriteRenderer>().sprite = theme.Floor[1];
                        else
                            Floor.GetComponent<SpriteRenderer>().sprite = theme.Floor[0];
                    }
                    else
                    {
                        Vector2 index = GetTileIndex(room.tiles, new Vector2(i, j));
                        if (index.x == -1)
                            Floor.GetComponent<SpriteRenderer>().enabled = false;
                        else
                            Floor.GetComponent<SpriteRenderer>().sprite = theme.FloorWalls[(int)index.x, (int)index.y];
                    }

                }
            }
        }
        return room;
    }

    //method for turning a number of points in the room to a bool map for if the coordinate is a wall or not
    public static bool[,] ToRoom(Vector2[] walls, int indX, int indY)
    {
        bool[,] Room = new bool[Vars.vars.Roomsize, Vars.vars.Roomsize];
        for (int i = 0; i < Vars.vars.Roomsize; i++)
        {
            Room[0, i] = true;
        }
        for (int i = 0; i < Vars.vars.Roomsize; i++)
        {
            Room[Vars.vars.Roomsize - 1, i] = true;
        }
        for (int i = 0; i < Vars.vars.Roomsize; i++)
        {
            Room[i, 0] = true;
        }
        for (int i = 0; i < Vars.vars.Roomsize; i++)
        {
            Room[i, Vars.vars.Roomsize - 1] = true;
        }
        foreach (Vector2 V in walls)
        {
            Room[(int)V.x, (int)V.y] = true;
        }

        int j = Vars.vars.Roomsize / 2;
        // INSERT DOORS HERE
        if (Vars.vars.Roomsize % 2 == 0)
        {
            if (WallArraytest(Vars.Levelmap, new Vector2(indX - 1, indY), false))
            {
                Room[0, j] = false;
                Room[0, j - 1] = false;
            }
            if (WallArraytest(Vars.Levelmap, new Vector2(indX + 1, indY), false))
            {
                Room[Vars.vars.Roomsize - 1, j] = false;
                Room[Vars.vars.Roomsize - 1, j - 1] = false;
            }
            if (WallArraytest(Vars.Levelmap, new Vector2(indX, indY- 1), false))
            {
                Room[j, 0] = false;
                Room[j - 1, 0] = false;
            }
            if (WallArraytest(Vars.Levelmap, new Vector2(indX, indY + 1), false))
            {
                Room[j, Vars.vars.Roomsize - 1] = false;
                Room[j - 1, Vars.vars.Roomsize - 1] = false;
            }
        }
        else
        {
            j = (Vars.vars.Roomsize - 1) / 2;
            if (WallArraytest(Vars.Levelmap, new Vector2(indX - 1, indY), false))
                Room[0, j] = false;
            if (WallArraytest(Vars.Levelmap, new Vector2(indX + 1, indY), false))
                Room[Vars.vars.Roomsize - 1, j] = false;
            if (WallArraytest(Vars.Levelmap, new Vector2(indX, indY - 1), false))
                Room[j, 0] = false;
            if (WallArraytest(Vars.Levelmap, new Vector2(indX, indY + 1), false))
                Room[j, Vars.vars.Roomsize - 1] = false;
        }

        return Room;
    }

    //method for getting the index of the sprite needed for the wall
    public static Vector2 GetTileIndex(bool[,] walls, Vector2 position)
    {
        //create a temporary array of bools for the surrounding ground
        bool[,] temp = new bool[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                temp[i, j] = WallArraytest(walls, new Vector2(position.x + i - 1, position.y + j - 1), walls[(int)position.x, (int)position.y]);
            }
        }


        //checks every implemented case for a sprite
        if (temp[1,1]) {
            if (!temp[1, 2] && temp[1, 0] && !temp[2, 1] && temp[0, 1])
                return new Vector2(2, 0);
            if (temp[1, 2] && !temp[1, 0] && !temp[2, 1] && temp[0, 1])
                return new Vector2(2, 1);
            if (temp[1, 2] && !temp[1, 0] && temp[2, 1] && !temp[0, 1])
                return new Vector2(2, 2);
            if (!temp[1, 2] && temp[1, 0] && temp[2, 1] && !temp[0, 1])
                return new Vector2(2, 3);

            if (!temp[1, 2] && temp[1, 0] && temp[2, 1] && temp[0, 1])
                return new Vector2(1, 0);
            if (temp[1, 2] && temp[1, 0] && !temp[2, 1] && temp[0, 1])
                return new Vector2(1, 1);
            if (temp[1, 2] && !temp[1, 0] && temp[2, 1] && temp[0, 1])
                return new Vector2(1, 2);
            if (temp[1, 2] && temp[1, 0] && temp[2, 1] && !temp[0, 1])
                return new Vector2(1, 3);

            if (!temp[2, 2])
                return new Vector2(0, 0);
            if (!temp[2, 0])
                return new Vector2(0, 1);
            if (!temp[0, 2])
                return new Vector2(0, 3);
            if (!temp[0, 0])
                return new Vector2(0, 2);

            return new Vector2(-1, -1);
        }
        else
        {
            if (temp[1, 2] && !temp[1, 0] && temp[2, 1] && !temp[0, 1])
                return new Vector2(2, 0);
            if (!temp[1, 2] && temp[1, 0] && temp[2, 1] && !temp[0, 1])
                return new Vector2(2, 1);
            if (!temp[1, 2] && temp[1, 0] && !temp[2, 1] && temp[0, 1])
                return new Vector2(2, 2);
            if (temp[1, 2] && !temp[1, 0] && !temp[2, 1] && temp[0, 1])
                return new Vector2(2, 3);

            if (temp[1, 2] && !temp[1, 0] && !temp[2, 1] && !temp[0, 1])
                return new Vector2(1, 0);
            if (!temp[1, 2] && !temp[1, 0] && temp[2, 1] && !temp[0, 1])
                return new Vector2(1, 1);
            if (!temp[1, 2] && temp[1, 0] && !temp[2, 1] && !temp[0, 1])
                return new Vector2(1, 2);
            if (!temp[1, 2] && !temp[1, 0] && !temp[2, 1] && temp[0, 1])
                return new Vector2(1, 3);

            if (temp[2, 2])
                return new Vector2(0, 0);
            if (temp[2, 0])
                return new Vector2(0, 1);
            if (temp[0, 2])
                return new Vector2(0, 3);
            if (temp[0, 0])
                return new Vector2(0, 2);

            return new Vector2(-1, -1);
        }
    }

    //method for getting the index of the sprite needed for the wall
    private static int Getsuroundings(bool[,] walls, Vector2 position)
    {
        //create a temporary array of bools for the surrounding ground
        bool[,] temp = new bool[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                temp[i, j] = WallArraytest(walls, new Vector2(position.x + i - 1, position.y + j - 1), walls[(int)position.x, (int)position.y]);
            }
        }
        int floor = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!temp[i, j] && !(i == 1 && j == 1))
                    floor++;
            }
        }
        return floor;
    }

    //simple try catch for accessing the wall array
    private static bool WallArraytest(bool[,] array, Vector2 place, bool wall)
    {
        try
        {
            return array[(int)place.x, (int)place.y];
        }
        catch (IndexOutOfRangeException )
        {
            return wall;
        }
    }

    //just a simple exit method
    public static void Exit()
    {
        Application.Quit();
    }

    //just a simple reset method
    public static AsyncOperation Reset()
    {
        Scene S = SceneManager.GetActiveScene();
        return SceneManager.LoadSceneAsync(S.buildIndex);
    }
}
