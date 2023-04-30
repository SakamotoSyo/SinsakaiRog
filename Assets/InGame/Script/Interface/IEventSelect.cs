using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEventSelect
{
    public void IfSelected(Text text, EventSelectDataScript eventData);
    public void IfNotSelected();
}
