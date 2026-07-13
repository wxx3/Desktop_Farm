using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    public Chicken()
    {
        InitPops();
        Produceable produce = new Produceable();
        produce.Bind(this);
        AddComponent( produce );
    }
     public override void InitPops()
    {
        m_Props.Add(PropId.Name, VarHelper.PackValue("chicken"));
        m_Props.Add(PropId.FlySpeed, VarHelper.PackValue(3.0f));
        m_Props.Add(PropId.FallSpeed, VarHelper.PackValue(0.0f));
        m_Props.Add(PropId.WalkSpeed, VarHelper.PackValue(2.0f));
        m_Props.Add(PropId.PosX, VarHelper.PackValue(0f));
        m_Props.Add(PropId.PosY, VarHelper.PackValue(-2f));
        //下蛋时间
        m_Props.Add(PropId.MaxProduceSpeed, VarHelper.PackValue(1f));
        m_Props.Add(PropId.MinProduceSpeed, VarHelper.PackValue(5f));

    }

}
