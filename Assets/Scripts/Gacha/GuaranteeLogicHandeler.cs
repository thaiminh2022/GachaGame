using UnityEngine;

class GuaranteeLogicHandeler
{
    

    public ItemsObject rolledItem { get; set; }
    public int fourStarsRollCounter{get; private set;} = 0;
    public int fiveStarsRollCounter {get; private set;} = 0;
 
    public GuaranteeLogicHandeler()
    {
        fourStarsRollCounter  = 0;
        fiveStarsRollCounter  = 0;
    }
    public bool CanGetFourStars(){

        return fourStarsRollCounter >= 9;
    }

    public bool CanGetFiveStars(){
       return fiveStarsRollCounter >= 89;
    }

    public void AddingCounters(){
        fourStarsRollCounter++;
        fiveStarsRollCounter++;
    }
    public void ResetCounters(rollTypes rollType){
        switch(rollType){
            case rollTypes.FiveStars:
                fiveStarsRollCounter =0;
                break;
            case rollTypes.FourStars:
                fourStarsRollCounter = 0;
                break;
        }
    }

}