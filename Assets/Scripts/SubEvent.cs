using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEvent : MonoBehaviour, IGameEvent
{
    public Building building;
    public int nbPeopleToSub;

    public void action()
    {
        var buildingPeopleCount = building.Population;
        var removingPeople = (nbPeopleToSub > buildingPeopleCount) ? buildingPeopleCount : nbPeopleToSub;
        this.building.RemovePopulation(removingPeople);
    }
}