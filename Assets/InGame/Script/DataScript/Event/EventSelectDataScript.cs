using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "SakamotoScriptable/SakamotoData")]
public class EventSelectDataScript : ScriptableObject
{
    public string EventDescription => _eventDescription;
    public int ProbabilitySuccess => _probabilitySuccess;
    public string[] EventResults => _eventResults;
    public IEventSelect EventEffect => _eventEffect;

    [SerializeField] private string _eventName;
    [SerializeField] private string _eventDescription;
    private int _probabilitySuccess;
    [SerializeField] private string[] _eventResults = new string[2];
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

    public void SetProbabilitySuccess(int Probability) 
    {
        _probabilitySuccess = Probability;
    }

    public void SetEventResultsTextArray(string[] stArray) 
    {
        _eventResults = stArray;
    }
}
