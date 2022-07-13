using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour {
    //UI refrences
    public Text[] labels;
    public Text Probability;
    //script refrences
    public Container[] Parts;
    public Container Output;
    //a simple bool that if turned on, just sets the probability to 100% all the time
    public bool DevMode;
    //the variable meant to display the probability of crafting
    private decimal probability;

    // Use this for initialization
    void Start()
    {
        //adds the update function to each crafting slot
        foreach (Container P in Parts)
        {
            P.update = new System.Action<Container>(ProbUpdate);
        }
    }
    
    //the method that updates the probability and labels
    public void ProbUpdate(Container C)
    {
        //loops through each part
        for (int i = 0; i < 6; i++)
        {
            if (Parts[i].Containsp != null)
            {
                //if there is a part in the slot, it displays its effect on the label
                labels[i].text = Common.GetEffect2(Parts[i].Containsp);
            }
            else //otherwise it displays nothing
                labels[i].text = "";
        }
        //checks if devmode is turned off
        if (!DevMode) {
            //initializes variables for the probability and craft success
            decimal a = 1;
            int b = 0;
            for (int i = 0; i < 6; i++)
            {
                //loops through each part to see if it exists
                if (Parts[i].Containsp != null)
                {
                    //multiplies the probability by the part's chance
                    a *= (decimal)Common.GetProbability(Common.GetRarity(Parts[i].Containsp.type));
                    //checks if it's a crafting success part
                    if (Parts[i].Containsp.type == 5)
                    {
                        //if so, adds it to the variable
                        b = Parts[i].Containsp.amount;
                    }
                }
            }
            //makes the probability into a percent
            probability = 100 * a;
            probability = System.Math.Truncate(probability);
            //checks if there is any CS parts
            if (b > 0)
            {
                //adds the CS mods to the probability
                probability += (decimal)(100 * Common.FullEffect(new Part(5, b)));
                //flattens the chance to 100 if it is greater
                if (probability > 100)
                {
                    probability = 100;
                }
            }
        }
        else
        {
            //sets probability to 100 if devmode is on
            probability = 100;
        }
        //updates the indicator for the probability
        Probability.text = (probability / 100).ToString("P0");
    }

    //the method for actually crafting the mod
    public void Craft()
    {
        //checks if there isnt already another mod in the slot
        if (Output.Containsm == null) {
            //creates the variable that tells if the craft was successful
            int a = Random.Range(1, 100);
            //creates the list for storing the parts
            List<Part> parts = new List<Part>();
            for (int i = 0; i < 6; i++)
            {
                //cycles through the parts and adds it to the list if it isnt null
                if (Parts[i].Containsp != null)
                {
                    parts.Add(Parts[i].Containsp);
                }
            }
            //if the crafing slots contains any parts
            if (parts.Count > 0) {
                if (a <= probability)
                {
                    //creates the new mod if the craft was a success
                    Output.Containsm = new Module(parts.ToArray());
                }
                //loops through all parts and clears them
                foreach (Container P in Parts)
                {
                    P.Containsp = null;
                }
            }
            //updates the probability now that the crafting slots were changed
            ProbUpdate(null);
        }
    }

    public void DevSwitch()
    {
        DevMode = !DevMode;
    }
}
