using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBase : ICompenent
{
    protected EntityBase m_entity;
    public virtual void Bind(EntityBase entity)
    {
        m_entity = entity;
    }
    public virtual void OnUpdate()
    {
    }
    public virtual void OnDestroy()
    {

    }

    
}
