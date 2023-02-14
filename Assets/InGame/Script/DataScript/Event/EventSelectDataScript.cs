using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "SakamotoScriptable/SakamotoData")]
public class EventSelectDataScript : ScriptableObject
{
    [SerializeField] private string _eventName;
    public string EventDescription => _eventDescription;
    [SerializeField] private string _eventDescription;
    public IEventSelect EventEffect => _eventEffect;
    [SerializeReference, SubclassSelector]
    [SerializeField] private IEventSelect _eventEffect;

    public void SetEventName(string st) 
    {
        _eventName = st;
    }

    public void SetEventDescription(string st) 
    {
        _eventDescription = st;
    }
}
