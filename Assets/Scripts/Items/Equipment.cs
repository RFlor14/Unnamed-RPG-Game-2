using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{

    public EquipmentSlot equipSlot; //slot to store equipment in 
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;


    //Player Stats
    public int armorModifier; //Increase or decrease in armor
    public int damageModifier; //Increase or decrease in damage

    //overrides -Use- method
    public override void Use()
    {
        base.Use();

        // Equip item
        EquipmentManager.instance.Equip(this);

        // Removes equipped item from inventory
        RemoveFromInventory();

    }


}

//create enum outside of class so its not encapsulated
//This results in us being able to use it in multiple places
public enum EquipmentSlot { Head, Chest, Legs, Boots, Weapon, Shield }


// corresponds to body blendshapes
public enum EquipmentMeshRegion {Legs, Arms, Torso} 
