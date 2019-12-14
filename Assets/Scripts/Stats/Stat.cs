using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// use this whenever we want unity to serialize a custom class
// meaning fields inside of this class will show up in the inspector
[System.Serializable]
public class Stat
{

    [SerializeField]
    private int baseValue;


    private List<int> modifiers = new List<int>();


    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }


    public void AddModifier (int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }


    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
}
