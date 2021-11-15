using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    [SerializeField]
    private List<Item> items = new List<Item>();
    [SerializeField]
    private List<string> itemsStringVersion = new List<string>();

    public void AddToInventory(ItemsObject itemsObject, int ammount = 0)
    {
        if (ammount <= 0)
        {
            Debug.Log("Not a valid ammout, returned!");
            return;
        }

        if (itemsStringVersion.Contains(itemsObject.name) == false)
        {
            items.Add(new Item(itemsObject, ammount));
            AddItemsListStringVerse();

        }
        else
        {
            int index = itemsStringVersion.IndexOf(itemsObject.name);
            items[index].AddAmmout(ammount);
        }

    }

    private void AddItemsListStringVerse()
    {
        itemsStringVersion.Clear();

        foreach (var item in items)
        {
            itemsStringVersion.Add(item.itemsObject.name);
        }
    }
    public List<Item> GetItemList()
    {
        return items;
    }
}

[Serializable]
public class Item
{
    public ItemsObject itemsObject;
    public int ammout;

    public Item(ItemsObject itemsObject, int ammout)
    {
        this.itemsObject = itemsObject;
        this.ammout = ammout;
    }

    public void AddAmmout(int ammount)
    {
        this.ammout += ammount;
    }


    // Implement from IComaparable
    public int CompareTo(Item obj)
    {
        return this.ammout - obj.ammout;
    }
}