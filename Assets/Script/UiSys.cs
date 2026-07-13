using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class UiSys : SystemBase
{

    private GameObject m_gameObject;
    Dictionary<int, Ui> m_Sid2Ui = new Dictionary<int, Ui>();
    List<Ui> m_ActiveUi = new List<Ui>();

    public void CreateUi(EntityBase entity)
    {
        string name = VarHelper.GetString(entity.GetProp(PropId.Name));
        string path = $"Animal/{name}";//这个地方局限在animal了
        ResourceSys resourceSys = luncher.GetSystem<ResourceSys>();
        GameObject prefab = resourceSys.GetRes(path);
        if (prefab == null)
        {
            Debug.LogError("获取预制体失败");
            return;
        }
        prefab = UnityEngine.Object.Instantiate(prefab);
        Ui ui = new Ui();
        ui.OnCreate(entity, prefab);
        m_Sid2Ui.Add(ui.InstanceId, ui);
        m_ActiveUi.Add(ui);
    }
    public Ui GetUi(int s_id)
    {
        if (!m_Sid2Ui.ContainsKey(s_id))
        {
            return null;
        }
        return m_Sid2Ui[s_id];
    }
    //todo:和animal逻辑绑定，不同的动物调用不同的预制体，对表现层进行操作
    public override void Update()
    {
        for(int i = 0; i < m_ActiveUi.Count; i++) {//对于所有的ui进行更新
            m_ActiveUi[i].OnUpdate();
        }
    }
    public void DestroyUi(int s_id)
    {
        if (!m_Sid2Ui.ContainsKey(s_id))
        {
            Debug.LogError($"不存在sid为{s_id}的ui");
            return;
        }
        Ui ui = m_Sid2Ui[s_id];
        ui.OnDestroy();
        m_ActiveUi.Remove(ui);
        m_Sid2Ui.Remove(s_id);
    }
}
