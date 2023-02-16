using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventDataBase",menuName = "SakamotoScriptable/EventDataBase")]
public class EventDataBase : ScriptableObject
{
    public List<EventSelectDataScript> EventDataList;

    public EventSelectDataScript RamdomEventData() 
    {
        var index = Random.Range(0, EventDataList.Count);
        return EventDataList[index];
    }
}
