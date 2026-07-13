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
        product.OnEntityEvent += HandleEntityEvent;
        uiSys.CreateUi(product);
    }
    private void HandleEntityEvent(EntityBase entity, string eventName, object data)
    {
        switch (eventName)
        {
            case "OnCollect":
                OnCollect(entity);
                break;
        }
    }
    private void OnCollect(EntityBase entity)
    {
        entity.OnEntityEvent -= HandleEntityEvent;
        entity.OnDestroy();
        uiSys.DestroyUi(entity.InstanceId);
    }
}
