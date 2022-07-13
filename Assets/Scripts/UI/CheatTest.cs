using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatTest : MonoBehaviour {
    //UI refrences
    public Container Display;
    public Text Amount;
    public Text TypeText;

    //type vars
    private int type;
    public int Type //this varible cycles through all parts so that they can be cheated in
    {
        get
        {
            //default return 
            return type;
        }
        set
        {
            //this defines the edges of the cycle
            if (value < 0)
                type = (Vars.NumberOfParts - 1);
            else if (value > (Vars.NumberOfParts - 1))
                type = 0;
            else
                type = value;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //updates the text that displays the current type
        TypeText.text = Type.ToString();

        //tries to parse the number input, if successful, it creates a new part at the output
        try
        {
            int a = int.Parse(Amount.text);
            Display.Containsp = new Part(Type, a);
        }
        catch { }
	}

    //button refrences to increase/decrease the type
    public void TypeUp()
    {
        Type++;
    }
    public void TypeDown()
    {
        Type--;
    }
}
