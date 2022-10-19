using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEvent : MonoBehaviour, IGameEvent
{
    public Building building;
    public int nbPeopleToAdd;

    public void action()
    {
        building.AddPopulation(nbPeopleToAdd);
    }
}