using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityBase : IEntity
{
    public Dictionary<PropId, VarValue> m_Props = new Dictionary<PropId, VarValue>();
    protected List<ComponentBase> m_ActiveCom = new List<ComponentBase>();
    public event Action<PropId, VarValue> OnPropChanged;//属性改动广播
    public event Action<EntityBase, String, object> OnEntityEvent;//在实体上触发事件广播
    static int m_Sid = 1;
    public static int sid
    {
        get { return m_Sid++; }
    }
    public int InstanceId;
    public virtual VarValue GetProp(PropId propId)
    {
        bool has = m_Props.TryGetValue(propId, out VarValue result);
        if (has)
        {
            return result;
        }
        return new VarValue { valueType = VarValue.ValueType.None };
    }
    public void SetProp(PropId propId, VarValue newValue)
    {
        m_Props[propId] = newValue;
        OnPropChanged?.Invoke(propId, newValue);
    }
    public void TriggerEvent(String eventName, object data = null)
    {
        OnEntityEvent?.Invoke(this, eventName, data);
    }
    protected virtual void AddComponent(ComponentBase component)
    {
        m_ActiveCom.Add(component);
        component.Bind(this);
    }
    public virtual void OnUpdate()
    {
        for (int i = 0; i < m_ActiveCom.Count; i++)
        {
            m_ActiveCom[i].OnUpdate();
        }
    }

    public virtual void InitPops()
    {
    }
   
    public virtual void OnCreate()
    {
    }

    public virtual void OnDestroy()
    {
        if (m_ActiveCom != null)
        {
            for (int i = 0; i < m_ActiveCom.Count; i++)
            {
                m_ActiveCom[i].OnDestroy();
            }
            m_ActiveCom.Clear();
        }

        if (m_Props != null)
        {
            m_Props.Clear();
        }

        OnPropChanged    = null;
        OnEntityEvent = null;
    }
    
}
