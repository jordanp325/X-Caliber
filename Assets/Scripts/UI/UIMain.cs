using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMain : MonoBehaviour{
    //the UI gmaeobjects and scripts for refrence
    public GameObject Inv;
    public GameObject Crft;
    public GameObject Cht;
    public Container[] CrftP;
    public Container[] CrftM;
    public Container[] InvP;
    public Container[] InvM;
    public UnityEngine.UI.Text Money;
    public bool changed;
    public bool open;

    // Use this for initialization
    void Start()
    {
        //sets all the update functions for the part containers in the crafting menu
        foreach (Container C in CrftP)
        {
            C.update = new Action<Container>(update);
        }
        //sets all the update functions for the part containers in the inventory menu
        foreach (Container C in InvP)
        {
            C.update = new Action<Container>(update);
        }
    }

    // Update is called once per frame
    void Update () {
        //displays the money variable in the UI
        Money.text = Vars.money.ToString() + "Ӿ";
        //sets the variable if anything is open
        if (Inv.activeSelf || Cht.activeSelf || Crft.activeSelf)
            open = true;
        else
            open = false;
        //checks which button was pressed
        if (Vars.vars.Player.dead) ;
        else if (Input.GetKeyUp(KeyCode.E))
        {
            //taking note if the menu was already open, then closing all menus
            bool B = Inv.activeSelf;
            CloseAll();
            //setting the time scale to 1 if it isnt going to re open itself
            Time.timeScale = Convert.ToInt32(B);
            //re opening itself by activating it and loading the data stored elsewhere
            Common.SaveLoad(InvP, InvM, B);
            Inv.SetActive(!B);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            //taking note if the menu was already open, then closing all menus
            bool B = Crft.activeSelf;
            CloseAll();
            //setting the time scale to 1 if it isnt going to re open itself
            Time.timeScale = Convert.ToInt32(B);
            //re opening itself by activating it and loading the data stored elsewhere
            Common.SaveLoad(CrftP, CrftM, B);
            Crft.SetActive(!B);
        }
        else if (Input.GetKeyUp(KeyCode.F) && Vars.vars.crafting.DevMode)
        {
            //taking note if the menu was already open, then closing all menus
            bool B = Cht.activeSelf;
            CloseAll();
            //setting the time scale to 1 if it isnt going to re open itself
            Time.timeScale = Convert.ToInt32(B);
            //simply reactivating it since it has nothing to load
            Cht.SetActive(!B);
        }
    }

    //a simple method for closing all open windows
    public void CloseAll()
    {
        //checks which window is open (in any) then closes it and saves the data
        if (Inv.activeSelf)
        {
            Inv.SetActive(false);
            Common.SaveLoad(InvP, InvM, true);
        }
        else if (Crft.activeSelf)
        {
            Crft.SetActive(false);
            Common.SaveLoad(CrftP, CrftM, true);
        }
        else if (Cht.activeSelf)
        {
            //though here it has nothing to save so it just closes
            Cht.SetActive(false);
        }
    }

    //simple exit method for the exit button
    public void Exit()
    {
        Common.Exit();
    }

    //to update the inventory variable
    public void update(Container C)
    {
        //changes the part in the slot that was modified for both the crafting and inventory menu
        if (Crft.activeSelf)
        {
            Vars.InventoryParts[Array.IndexOf(CrftP, C)] = C.Containsp;
        }
        if (Inv.activeSelf)
        {
            Vars.InventoryParts[Array.IndexOf(InvP, C)] = C.Containsp;
        }
        //changes the variable to show that it was updated recently
        changed = !changed;
    }

    //a more simple save load function for other scripts
    public void SaveLoad(bool saving)
    {
        //checks which inventory menu is open, then saves or loads it
        if (Inv.activeSelf)
        {
            Common.SaveLoad(InvP, InvM, saving);
        }
        else if(Crft.activeSelf)
        {
            Common.SaveLoad(CrftP, CrftM, saving);
        }
    }
}
