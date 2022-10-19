using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferEvent : MonoBehaviour, IGameEvent
{
    [SerializeField] Building buildingA;
    [SerializeField] Building buildingB;
    [SerializeField] int peopleToTransfer;

    //M�thode qui transf�re nbPeopleTransfer du b�timent A au b�timent B
    public void action()
    {
        var buildingAPeopleCount = buildingA.Population;
        var transferablePeopleCount = (peopleToTransfer > buildingAPeopleCount) ? buildingAPeopleCount : peopleToTransfer;
        buildingB.AddPopulation(transferablePeopleCount);
        buildingA.RemovePopulation(transferablePeopleCount);
    }
}