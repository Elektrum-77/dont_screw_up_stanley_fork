using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [System.Serializable]
    class TimedEvent : IGameEvent
    {
        public TimedEvent(double time, Component gameEvent)
        {
            this.time = time;
            this.gameEvent = gameEvent;
        }

        [SerializeField] double time;
        [SerializeField] Component gameEvent;

        public void action()
        {
            (gameEvent as IGameEvent)?.action();
        }

        public double getTime()
        {
            return time;
        }
    };

    int currentEventIndex = 0;
    [SerializeField] private List<TimedEvent> timelines = new List<TimedEvent>();

/*
    public void addEvent(double time, IGameEvent gameEvent)
    {
        timelines.Add(new TimedEvent(time, gameEvent));
    }
*/

    bool isCurrentEventOccuring(double timer)
    {
        return timelines[currentEventIndex].getTime() < timer;
    }


    bool hasEventLeft()
    {
        return timelines.Count > currentEventIndex;
    }

    public void UpdateEventList(double timer)
    {
        while (hasEventLeft() && isCurrentEventOccuring(timer))
        {
            var currentEvent = timelines[currentEventIndex++];
            currentEvent.action();
        }
    }
}