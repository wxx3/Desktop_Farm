using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Collectable : ComponentBase
{
    private float m_radius;
    public override void Bind(EntityBase entity)
    {
        base.Bind(entity);
        m_radius = VarHelper.GetFloat(m_entity.GetProp(PropId.BodyRadius));
    }

    public override void OnUpdate()
    {
        OnCollect();
    }
    
    private void OnCollect()
    {
        Vector2 pos = ScreenHelper.GetMouseWorldPos();
        float posx = VarHelper.GetFloat(m_entity.GetProp(PropId.PosX));
        float posy = VarHelper.GetFloat(m_entity.GetProp(PropId.PosY));
        float distance = Vector2.Distance(new Vector2(posx, posy), pos);
        if (distance <= m_radius)
        {
            //todo: collect
            m_entity.TriggerEvent("OnCollect", null);
        }
    }
}
