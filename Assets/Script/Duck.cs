using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Duck : Animal
{
    public Duck()
    {
        InitPops();
        Produceable produce = new Produceable();
        produce.Bind(this);
        AddComponent(produce);
    }
    public override void InitPops()
    {
        m_Props.Add(PropId.Name, VarHelper.PackValue("duck"));
        m_Props.Add(PropId.FlySpeed, VarHelper.PackValue(3.0f));
        m_Props.Add(PropId.FallSpeed, VarHelper.PackValue(0.0f));
        m_Props.Add(PropId.WalkSpeed, VarHelper.PackValue(1.5f));
        m_Props.Add(PropId.PosX, VarHelper.PackValue(0f));
        m_Props.Add(PropId.PosY, VarHelper.PackValue(-2f));
        //下蛋时间
        //m_Props.Add(PropId.MaxProduceSpeed, VarHelper.PackValue(600f));
        //m_Props.Add(PropId.MinProduceSpeed, VarHelper.PackValue(1800f));
        //演示
        m_Props.Add(PropId.MaxProduceSpeed, VarHelper.PackValue(300f));
        m_Props.Add(PropId.MinProduceSpeed, VarHelper.PackValue(1500f));

    }
}
