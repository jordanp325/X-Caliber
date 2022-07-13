using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //a refrence to the container it uses
    public Container Con;
    //the variable showing if the mouse is over the object
    public bool MouseOver = false;
	
	// Update is called once per frame
	void Update () {
        //checks if the container is holding anything, along with if the mouse is over the object
        if (MouseOver && Con.Containsp != null)
        {
            //gives MF all the variables it needs to display the part
            Vars.vars.MF.TooltipP = Con.Containsp;
            Vars.vars.MF.part = true;
            Vars.vars.MF.switched = true;
        }
        else if (MouseOver && Con.Containsm != null)
        {
            //gives MF all the variables it needs to display the mod
            Vars.vars.MF.TooltipM = Con.Containsm;
            Vars.vars.MF.part = false;
            Vars.vars.MF.switched = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //displays the mouse as over the object when it enters it's borders
        MouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //displays the mouse as not over the object when it exits it's borders
        MouseOver = false;
    }

    void OnDisable()
    {
        //displays the mouse as not over the object when it is disabled(such as switching menus)
        MouseOver = false;
    }
}
