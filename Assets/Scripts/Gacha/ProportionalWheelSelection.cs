using System.Collections.Generic;

public class ProportionalWheelSelection
{
    // create a randoom object
    public static System.Random rand = new System.Random();

    // create a method to select the item
    public ItemsObject SelectItem(List<ItemsObject> items)
    {
        float poolsize = 0;

        // add all the percentage to the pool size as an int
        for(int i = 0; i < items.Count; i++ )
        {
            poolsize += items[i].percentage;
        }
        
        // generate a random number from 0 to poolsize + 1
        float randomNumber = (float)(rand.NextDouble() * (poolsize - 0) + 0) + 1;

        float probility = 0;
        for (int i = 0; i < items.Count; i++)
        {
            // ! inspect this code later
            // Generate random item
            probility += items[i].percentage;
            if (randomNumber <= probility)
                return items[i];
        }
        return null;    // this code will never come while you use this programm right :)
    }
}