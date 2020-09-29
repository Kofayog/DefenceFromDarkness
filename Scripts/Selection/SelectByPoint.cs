using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectByPoing : SelectionObject
{
    public override void Deselect()
    {
        Debug.Log("Point Deselected");
    }

    public override void Select()
    {
        Debug.Log("Point Selected");
    }


}
