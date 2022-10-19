using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReferenceSharing;
using UnityEngine;
using UnityEngine.Events;

public class City : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    public event Action<bool> isGameWin;
    [SerializeField] double timer;
    [SerializeField] double startingBatteryLevel;
    List<Building> buildings = new List<Building>();
    [SerializeField] Reference<double> batteryLevelRef, startingBatteryRef, timerRef, batteryPercentRef;
    [SerializeField] private Reference<int> totalPeopleRef;

    double eventTimer = 0;
    double batteryCount;
    int nbDead;
    int nbLeave;

    public bool isPause = false;
    private bool playingAlert = false;

    public int getNbPeople()
    {
        int somme = 0;
        for (int i = 0; i < this.buildings.Count; i++)
        {
            somme += this.buildings[i].Population;
        }

        totalPeopleRef.Value = somme;
        return somme;
    }

    private double GetCityConsumption() => buildings.Sum(b => b.CurrentConsumption);

    void Start()
    {
        AudioManager.Instance.Stop("alert");
        batteryCount = startingBatteryLevel;
        startingBatteryRef.Value = startingBatteryLevel;
        batteryLevelRef.Value = startingBatteryLevel;
        batteryPercentRef.Value = batteryLevelRef / startingBatteryLevel;
        isPause = false;
        playingAlert = false;
        Building[] tabBuilding = FindObjectsOfType<Building>();
        for (int i = 0; i < tabBuilding.Length; i++)
        {
            this.buildings.Add(tabBuilding[i]);
        }
    }

    void win()
    {
        AudioManager.Instance.Stop("alert");
        isPause = true;
        isGameWin?.Invoke(true);
        foreach (var b in buildings)
        {
            b.GetComponent<Outline>().enabled = false;
        }
    }

    void lose()
    {
        AudioManager.Instance.Stop("alert");
        isPause = true;
        isGameWin?.Invoke(false);
        foreach (var b in buildings)
        {
            b.GetComponent<Outline>().enabled = false;
        }
    }

    void turnOffAllBuilding()
    {
        foreach (var b in buildings)
        {
            b.SetPower(false);
        }
    }

    void Update()
    {
        if (!isPause)
        {
            eventTimer += Time.deltaTime;
            eventManager.UpdateEventList(eventTimer);
            timer -= Time.deltaTime;
            timerRef.Value = timer;
            batteryCount -= GetCityConsumption() * Time.deltaTime;
            batteryLevelRef.Value = batteryCount;
            batteryPercentRef.Value = batteryCount / startingBatteryLevel * 100f;
            if (batteryCount / startingBatteryLevel <= .25 && !playingAlert)
            {
                playingAlert = true;
                AudioManager.Instance.Play("alert");
            }

            foreach (var b in buildings)
            {
                b.HandlePopulationBehaviours();
            }
        }

        if (batteryCount <= 0)
        {
            turnOffAllBuilding();
        }

        if (timer <= 0)
        {
            win();
        }

        if (getNbPeople() == 0 && timer > 0)
        {
            lose();
        }
    }
}