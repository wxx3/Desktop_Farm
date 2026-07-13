using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum State
{
    Idle,
    Walking,
    Sleeping,
    Falling
}
public class Moveable : ComponentBase
{
    private State m_state;
    private int m_face = 1;
    private float m_x;
    private float m_y;
    private float m_time;
    
    public override void Bind(EntityBase entity)
    {
        base.Bind(entity);
        m_state = State.Falling;
        m_x = VarHelper.GetFloat(m_entity.GetProp(PropId.PosX));
        m_y = VarHelper.GetFloat(m_entity.GetProp(PropId.PosY));
    }
    public override void OnUpdate()
    {
        Behavior();
        
    }
    protected void Behavior()
    {
        switch (m_state)
        {
            case State.Falling:
                Fall();
                break;
            case State.Walking:
                Walk();
                Keep();
                break;
            case State.Idle:
                Idle();
                Keep();
                break;
        }
        //Debug.Log($"调用{m_x}, {m_y}，{ScreenHelper.Bottom}");

        m_entity.m_Props[PropId.PosX] = VarHelper.PackValue(m_x);
        m_entity.m_Props[PropId.PosY] = VarHelper.PackValue(m_y);
        m_entity.SetProp(PropId.Face, VarHelper.PackValue(m_face));
        m_entity.SetProp(PropId.State, VarHelper.PackValue((int)m_state));
    }
    protected void Walk()
    {
        float walkSpeed = VarHelper.GetFloat(m_entity.GetProp(PropId.WalkSpeed));
        float moveX = m_face * Time.deltaTime * walkSpeed;
        float extentX = VarHelper.GetFloat(m_entity.GetProp(PropId.BodyRadius), 0.5f);
        m_x += moveX;
        if (m_x + extentX >= ScreenHelper.Right && m_face == 1 || m_x - extentX <= ScreenHelper.Left && m_face == -1)
        {
            FlipX();
        }
    }
    protected void Idle()
    {

    }
    protected void Keep()
    {
        m_time -= Time.deltaTime;
        if (m_time <= 0f)
        {
            m_time = RandomHelper.RangeFloat(3f, 5f);
            if (RandomHelper.Chance(0.5f))
            {
                if (m_state == State.Idle)
                {
                    m_state = State.Walking;
                }
                else if (m_state == State.Walking)
                {
                    m_state = State.Idle;
                }
            }
            if (RandomHelper.Chance(0.5f))
            {
                FlipX();
            }
        }
    }
    protected void Fall()
    {
        float gravity = VarHelper.GetFloat(m_entity.GetProp(PropId.Gravity), 9.8f);
        float fallSpeed = VarHelper.GetFloat(m_entity.GetProp(PropId.FallSpeed), 0.0f);
        float flySpeed = VarHelper.GetFloat(m_entity.GetProp(PropId.FlySpeed), 0.0f);
        int faceDir = VarHelper.GetInt(m_entity.GetProp(PropId.FaceDir), 1);

        if (m_y <= ScreenHelper.Bottom)
        {
            m_y = ScreenHelper.Bottom;
            m_state = State.Idle;
            m_time = RandomHelper.RangeFloat(3f, 5f);

            m_entity.m_Props[PropId.FallSpeed] = VarHelper.PackValue(0f);
        }
        else
        {
            //Debug.Log("111");
            fallSpeed += Time.deltaTime * gravity;
            m_entity.m_Props[PropId.FallSpeed] = VarHelper.PackValue(fallSpeed);
            float moveX = flySpeed * faceDir * Time.deltaTime;
            float moveY = -fallSpeed * Time.deltaTime;
            m_x = m_x + moveX;
            m_y += moveY;

        }
        //Debug.LogError("落下");
    }

    protected void FlipX()
    {
        m_face = -m_face;
        //m_spriteRenderer.flipX = (m_face == -1);
    }

    //protected void UpdateAnimator()
    //{
    //    if (m_spriteRenderer != null)
    //    {
    //        m_animator.SetInteger("State", (int)m_state);
    //    }
    //}
}
