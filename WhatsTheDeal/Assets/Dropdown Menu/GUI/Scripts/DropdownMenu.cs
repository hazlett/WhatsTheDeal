using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropdownMenu : MonoBehaviour{

    private List<string> dropDownMenuList = new List<string>();

    public string[] listMembers;

    void Start()
    {
        PopulateList();
    }

    internal void PopulateList()
    {
        for (int i = 0; i < listMembers.Length; i++)
        {
            dropDownMenuList.Add(listMembers[i]);
        }
    }

    internal void Add(string name)
    {
        dropDownMenuList.Add(name);
    }

    internal void Remove(string name)
    {
        dropDownMenuList.Remove(name);
    }

    internal int Count()
    {
        return dropDownMenuList.Count;
    }

    internal string this[int index]
    {
        get { return dropDownMenuList[index]; }
        set { dropDownMenuList[index] = value; }
    }

    internal bool Contains(string name)
    {
        return dropDownMenuList.Contains(name);
    }

    internal int IndexOf(string name)
    {
        return dropDownMenuList.IndexOf(name);
    }
}
