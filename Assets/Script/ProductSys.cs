using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSys : SystemBase
{
    UiSys uiSys;
    Dictionary<int, Product> m_Sid2Product = new Dictionary<int, Product>();
    List<Product> m_ActiveProduct = new List<Product>();
    public override void Init(GameLuncher gameLuncher)
    {
        base.Init(gameLuncher);
        uiSys = gameLuncher.GetSystem<UiSys>();
    }

    public override void Update()
    {
        for (int i = m_ActiveProduct.Count - 1; i >= 0; i--)
        {
            m_ActiveProduct[i].OnUpdate();
        }
    }

    public void CreateProduct(string name, float posX, float posY)
    {
        Product product = null;
        if(name == "egg")
        {
            product = new Egg();
        }
        if(product == null)
        {
            Debug.LogError($"CreateProduct failed, name: {name}");
            return;
        }
        product.OnCreate();
        product.m_Props.Add(PropId.PosX, VarHelper.PackValue(posX));
        product.m_Props.Add(PropId.PosY, VarHelper.PackValue(posY));
        m_ActiveProduct.Add(product);
        m_Sid2Product.Add(product.InstanceId, product);
        
        product.OnEntityEvent += HandleEntityEvent;//默认订阅事件
        uiSys.CreateUi(product);
    }
    private void HandleEntityEvent(EntityBase entity, string eventName, object data)
    {
        switch (eventName)
        {
            case "OnCollect":
                Debug.LogFormat("收集事件触发, sid: {0}", entity.InstanceId);
                OnCollect(entity);
                break;
        }
    }
    private void OnCollect(EntityBase entity)
    {
        DestroyProductBySid(entity.InstanceId);
    }
    private void DestroyProductBySid(int sid)
    {
        if (m_Sid2Product.TryGetValue(sid, out Product product))
        {
            product.OnEntityEvent -= HandleEntityEvent;
            product.OnDestroy();
            m_ActiveProduct.Remove(product);
            m_Sid2Product.Remove(sid);
            uiSys.DestroyUi(sid);
        }
    }
}
