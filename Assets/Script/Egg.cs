using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Product
{
    public Egg()
    {
        m_Props.Add(PropId.Name, VarHelper.PackValue("egg"));
        m_Props.Add(PropId.BodyRadius, VarHelper.PackValue(0.2f));//拾取半径
        m_Props.Add(PropId.Value, VarHelper.PackValue(10));//拾取半径
        Collectable collectable = new Collectable();
        collectable.Bind(this);
        m_ActiveCom.Add(collectable);   
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        m_ActiveCom = null;
    }
}
