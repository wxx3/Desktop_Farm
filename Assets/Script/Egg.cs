using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : EntityBase
{
    public Egg()
    {
        m_Props.Add(PropId.Name, VarHelper.PackValue("egg"));
    }
}
