using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public enum PropId
{
    Gravity = 1,
    FallSpeed = 2,
    WalkSpeed = 3,
    FlySpeed = 4,
    Name = 5,
    MinProduceSpeed = 6,
    MaxProduceSpeed = 7,
    FaceDir = 8,
    State = 9,
    PosX = 10,
    PosY = 11,
    Face = 12,
    BodyRadius = 13
}

public class Animal: EntityBase
{
    
    protected SpriteRenderer m_spriteRenderer;
    public override void OnCreate()
    {
        InstanceId = Animal.sid;

        Moveable move = new Moveable();
        AddComponent(move);
    }
    
    public override void OnDestroy()
    {

    }

    public override void InitPops()
    {

    }


    //protected Dictionary<PropId, VarValue> m_Props = new Dictionary<PropId, VarValue>();

    //List<ICompoment> m_ActiveComs = new List<ICompoment>();

    //public int InstanceId;

    //public string AnimalName;

    //void AddCompente<T>()
    //{
    //    // new
    //    // m_ActiveComs.Add();
    //}

    //public virtual void OnCreate()
    //{
    //    InstanceId = Animal.sid;
    //    InitPops();
    //}
    //public virtual void OnUpdate()
    //{
    //    // 成熟，长大了，有生蛋功能了，
    //    // AddCompente  MakeEggCom

    //    // Update m_ActiveComs
    //}
    //public virtual void Destroy()
    //{

    //}

    //protected virtual void InitPops()
    //{

    //}

    //public virtual int GetProp(PropId propId)
    //{
    //    bool has = m_Props.TryGetValue(propId, out VarValue result);
    //    if (has)
    //    {
    //        return result.intValue;
    //    }

    //    return -1;
    //}
}
