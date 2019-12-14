using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{


    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
      instance = this;
    }

    #endregion

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;

    //essential for equipment manager, array for all items we currently have equipped
    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;


    //creates a callback method
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    //references inventory, cache instead of accesing instance in -Equip-
    Inventory inventory;


    //initializes the array
    void Start()
    {

        inventory = Inventory.instance;

        //give amount you have with Equipment.cs -enum- , this is the shortcut
        //creates a string array of elements inside of equipment slot
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];


        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();

    }


    //method for equipping items
    public void Equip (Equipment newItem)
    {
        //inserts item in array *placement matters*
        //hover over EquipmentSlot array to see where it corresponds
        int slotIndex = (int)newItem.equipSlot;


        //adds back previous item
        Equipment oldItem = Unequip(slotIndex); ;


        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, null);
        }

        SetEquipmentBlendShapes(newItem, 100);

        //this corresponds your new item to the correct array
        currentEquipment[slotIndex] = newItem;

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        //tells new mewsh how it should deform based on the bones of targetMesh
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;

    }


    //unequips item
    public Equipment Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {

            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;


            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }
        return null;
    }


    //unequips all items
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

         EquipDefaultItems();

    }


    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }


    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.U))
            UnequipAll();
    }


}
