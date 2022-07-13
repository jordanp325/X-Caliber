using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part { 
    //info for the part
    public int type;
    public int amount;
    public Sprite sprite;

    //this is the only constructor, so no empty parts
    public Part(int Type, int Amount)
    {
        type = Type;
        amount = Amount;
        sprite = Vars.vars.Parts[Type];
    }
}
