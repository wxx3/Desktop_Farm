using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Ui
{
    static int m_Sid = 1;
    public static int sid
    {
        get { return m_Sid++; }
    }
    public int InstanceId;
    public EntityBase m_Entity;
    public SpriteRenderer m_Renderer;
    public Animator m_Animator;
    private GameObject m_GameObject;
    public void OnCreate(EntityBase entity, GameObject prefab)
    {
        InstanceId = sid;

        m_Entity = entity;
        
        m_GameObject = prefab;
        m_Animator = prefab.GetComponent<Animator>();

        entity.OnPropChanged += HandlePropsChange;
        m_Renderer = prefab.GetComponent<SpriteRenderer>();
    }

    public void OnUpdate()
    {
        float x = VarHelper.GetFloat(m_Entity.GetProp(PropId.PosX));
        float y = VarHelper.GetFloat(m_Entity.GetProp(PropId.PosY));
        UpdatePos(x, y);//传入x,y坐标

    }
    private void HandlePropsChange(PropId propId, VarValue value)
    {
        switch (propId)
        {
            case PropId.State:
                m_Animator.SetInteger("State", VarHelper.GetInt(value));
                break;

            case PropId.Face:
                m_Renderer.flipX = (VarHelper.GetInt(value) == -1);
                break;
        }
    }
    public void OnDestroy()
    {
        if (m_Entity != null)
        {
            m_Entity.OnPropChanged -= HandlePropsChange;
            UnityEngine.Object.Destroy(m_GameObject);
        }

    }
    public void UpdatePos(float pos_x, float pos_y)
    {
        m_GameObject.transform.position = new Vector2(pos_x, pos_y);
    }
}
