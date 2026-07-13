using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Produceable : ComponentBase
{
    private float m_MinTime;
    private float m_MaxTime;
    private int m_State;
    private float m_Time;
    private string m_Name;
    public override void Bind(EntityBase entity)
    {
        base.Bind(entity);
        m_MinTime = VarHelper.GetFloat(m_entity.GetProp(PropId.MinProduceSpeed));
        m_MaxTime = VarHelper.GetFloat(m_entity.GetProp(PropId.MaxProduceSpeed));
        m_Time = RandomHelper.RangeFloat(m_MinTime, m_MaxTime);
        m_Name = VarHelper.GetString(m_entity.GetProp((PropId.Name)));
        entity.OnPropChanged += HandlePropsChange;
    }
    public override void OnUpdate()
    {
        if (m_State == 0 && m_Time <= 0f)
        {
            //todo:生产
            if(m_Name == "chicken")
            {
                Debug.LogError("生蛋！");
                m_entity.TriggerEvent("SpawnEgg");

            }
            m_Time = RandomHelper.RangeFloat(m_MinTime, m_MaxTime);
        }
        else if (m_Time > 0f)
        {
            m_Time -= Time.deltaTime;

        }
    }
    private void HandlePropsChange(PropId propId, VarValue value)
    {
        if(PropId.State == propId) {
            m_State = VarHelper.GetInt(m_entity.GetProp(PropId.State));
        }
    }
}
