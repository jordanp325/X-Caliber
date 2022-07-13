using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseFollow : MonoBehaviour {
    //All UI refrences (A lot)
    public Image displayM;
    public Image displayP;
    public Text num;
    public Canvas canvas;
    public GameObject Partholder;
    public GameObject Modholder;
    public GameObject PartTooltip;
    public RectTransform PartTooltipBG;
    public Text PartTooltipText;
    public GameObject ModTooltip;
    public Text[] ModTooltipText;
    //variable showing what the player is holding in their hand at the moment
    private int holding; //0 is nothing, 1 is a part, 2 is a mod
    public int held
    {
        get
        {
            return holding;
        }
    }
    //the variables for the contained part/mod
    public Part ContainsP;
    public Module ContainsM;
    //the variables for the time needed to show a tooltip
    public const int length = 50;
    private int timer = length;
    //the variables for monitoring the mouse's position
    Vector2 lastPosition;
    Vector2 mousePosition;
    //the part and mod that the tooltips display
    public Part TooltipP;
    public Module TooltipM;
    //variable monitoring if the tooltip needs to be updated
    public bool switched = false;
    //the variable for showing if the tooltip displays a part or a mod
    public bool part;
    //for updating things once al menus are closed
    bool updated;

    // Use this for initialization
    void Start () {
        //starts with holding nothing
        ContainsP = null;
        ContainsM = null;
        holding = 0;
        //starts with the UI off
        Modholder.SetActive(false);
        Partholder.SetActive(false);
    }
    
    void Update () {
        //drop held objects when inventory closed + etc
        if (!Vars.vars.UI.open && !updated)
        {
            if (ContainsP != null)
            {
                //creates the pickup and sets its values
                GameObject Pickup = Instantiate(Vars.vars.PickUpPrefab);
                Pickup.GetComponent<PartPU>().Contained = ContainsP;
                Pickup.transform.position = Vars.vars.Player.gameObject.transform.position;
                //deletes the contained part
                ContainsP = null;
            }
            //sets the trash slot to nothing
            Vars.InventoryParts[Vars.vars.MaxParts - 1] = null;
            //drops all held objects in crafting
            foreach (Container C in Vars.vars.crafting.Parts)
            {
                if (C.Containsp != null)
                {
                    //creates the pickup and sets its values
                    GameObject Pickup = Instantiate(Vars.vars.PickUpPrefab);
                    Pickup.GetComponent<PartPU>().Contained = C.Containsp;
                    Pickup.transform.position = Vars.vars.Player.gameObject.transform.position;
                    //deletes the contained part
                    C.Containsp = null;
                }
            }
            //checks if it holds any mod
            if (Vars.vars.crafting.Output.Containsm != null)
            {
                int index = -1;
                for (int i = 0; i < 9; i++)
                {
                    if (Vars.InventoryMods[i] == null)
                    {
                        index = i;
                        break;
                    }
                }
                if (index >= 0)
                {
                    Vars.InventoryMods[index] = Vars.vars.crafting.Output.Containsm;
                }
                Vars.vars.crafting.Output.Containsm = null;
            }
        }
        else if (Vars.vars.UI.open)
            updated = false;

       //move UI with mouse
       transform.localPosition = Input.mousePosition - canvas.transform.localPosition;

        //display the part/mod or neither
        if (ContainsP != null)
        {

            //changes the UI to reflect the chosen part
            displayP.sprite = ContainsP.sprite;
            num.text = ContainsP.amount.ToString();
            Partholder.SetActive(true);
            num.color = Common.GetColor(Common.GetRarity(ContainsP.type));
            //sets holding to 1
            holding = 1;
        }
        else if (ContainsM != null)
        {
            //changes the UI to reflect the chosen mod
            displayM.sprite = ContainsM.sprite;
            Modholder.SetActive(true);
            //sets holding to 2
            holding = 2;
        }
        else
        {
            //disables all UI
            Modholder.SetActive(false);
            Partholder.SetActive(false);
            //sets holding to 0
            holding = 0;
        }


        //moves the two variables forward in time
        lastPosition = mousePosition;
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //checks if the mouse cursor hasnt moved
        if (lastPosition == mousePosition)
        {
            //increments the tooltip timer
            timer--;
            //checks if the timer has ended, it isnt holding anything, and if it needs to be updated
            if (timer <= 0 && switched && holding == 0)
            {
                if (part)
                {
                    //if it is hovering over a part, it displays the info on the tooltip
                    PartTooltip.SetActive(true);
                    PartTooltipText.text = Common.GetEffect1(TooltipP);
                    //sets the appropriate size for the tooltips and the correct sprite
                    TextGenerator textGen = new TextGenerator();
                    TextGenerationSettings generationSettings = PartTooltipText.GetGenerationSettings(PartTooltipText.rectTransform.rect.size);
                    float width = textGen.GetPreferredWidth(PartTooltipText.text, generationSettings);
                    float height = textGen.GetPreferredHeight(PartTooltipText.text, generationSettings);
                    PartTooltipBG.sizeDelta = new Vector2(55f + width, 40f + height);
                    PartTooltipBG.localPosition = (PartTooltipBG.sizeDelta / 2f) + new Vector2(.5f, .5f);
                    PartTooltipBG.localPosition = new Vector2(PartTooltipBG.localPosition.x, PartTooltipBG.localPosition.y * -1f);
                    PartTooltipBG.GetComponent<Image>().sprite = Vars.vars.TooltipUIs[Common.GetRarity(TooltipP.type)];
                }
                else if (Vars.vars.UI.Inv.activeSelf || Vars.vars.UI.Crft.activeSelf)
                {
                    //if it is hovering over a mod and the inventory is active, it enables the tooltip and sets the sprite
                    ModTooltip.SetActive(true);
                    ModTooltip.GetComponent<Image>().sprite = Vars.vars.ComparisonUIs[TooltipM.rarity];
                    for (int i = 0; i < 6; i++)
                    {
                        //loops through all parts in the mod being displayed to check if they are null
                        if (TooltipM.parts.Length <= i)
                        {
                            ModTooltipText[i].text = "";
                            break;
                        }
                        else if (TooltipM.parts[i] == null)
                        {
                            ModTooltipText[i].text = "";
                        }
                        else
                        {
                            //displays the part info if it isnt null
                            ModTooltipText[i].text = Common.GetEffect2(TooltipM.parts[i]);
                        }
                    }
                }
                //updates the variable(saying it has updated)
                switched = false;
            }
            else
            {
                //if it doesnt need to be updated, then everything turns off
                PartTooltip.SetActive(false);
                ModTooltip.SetActive(false);
            }
        }
        else
        {
            //if the mouse was moved, it resets the timer, turns off all tooltips, and sets the switched variable to false
            timer = length;
            PartTooltip.SetActive(false);
            ModTooltip.SetActive(false);
            switched = false;
        }
    }
}
