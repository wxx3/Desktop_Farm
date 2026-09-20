using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AnimalSys : SystemBase
{

    //实现动物的创建，销毁，更新和获取，还有动物的事件处理
    Dictionary<int, Animal> m_Sid2Animal = new Dictionary<int, Animal>();
    List<Animal> m_ActiveAnimal = new List<Animal>();
    Dictionary<string, int> m_Name2Animal = new Dictionary<string, int>();
    ResourceSys m_Resources;
    UiSys uiSys;
    public override void Init(GameLuncher gameLuncher)
    {
        base.Init(gameLuncher);
        InitAnimalSellPrice();
    }
    public override void Update()
    {
        for (int i = 0; i <= m_ActiveAnimal.Count - 1; i++)
        {
            m_ActiveAnimal[i].OnUpdate();
        }
    }
    public void InitAnimalSellPrice()
    {
        m_Name2Animal.Add("chicken", 20);
        m_Name2Animal.Add("duck", 25);
    }
    public void CreateAnimal(string name)
    {
        m_Resources = luncher.GetSystem<ResourceSys>();
        uiSys = luncher.GetSystem<UiSys>();

        Animal animal = null;
        if(name == "chicken")//可以改成switch
        {
            animal = new Chicken();
        }
        if(name == "duck")
        {
            animal = new Duck();
        }
        if (animal != null)
        {
            animal.OnCreate();
            uiSys.CreateUi(animal);//ui绑定逻辑
            m_Sid2Animal.Add(animal.InstanceId, animal);
            m_ActiveAnimal.Add(animal);
            animal.OnEntityEvent += HandleEntityEvent;
        }
    }
    public int GetAnimalPrice(string name)
    {
        if (name == "chicken")
        {
            return 50;
        }
        if (name == "duck")
        {
            return 70;
        }
        return 0;
    }
    public int GetAnimalSellPrice(string name)
    {
        return m_Name2Animal.ContainsKey(name) ? m_Name2Animal[name] : 0;
    }
    private void HandleEntityEvent(EntityBase entity, string eventName, object data)
    {//处理动物事件
        switch (eventName)
        {
            case "SpawnEgg":
                SpawnEgg(entity);
                break;
        }
    }
    
    private void SpawnEgg(EntityBase entity)
    {
        float posX = VarHelper.GetFloat(entity.m_Props[PropId.PosX]);
        float posY = VarHelper.GetFloat(entity.m_Props[PropId.PosY]);
        ProductSys productSys = luncher.GetSystem<ProductSys>();
        Debug.LogFormat("SpawnEgg: posX={0}, posY={1}", posX, posY);
        productSys.CreateProduct("egg", posX, posY);
    }
    public void KillAnimalBySid(int sid)
    {
        uiSys = luncher.GetSystem<UiSys>();

        Animal animal = m_Sid2Animal.ContainsKey(sid) ? m_Sid2Animal[sid] : null;

        if (GetAnimalBySid(sid) != null)
        {
            //animal去自己销毁
            animal.OnEntityEvent -= HandleEntityEvent;
            animal.OnDestroy();
            m_Sid2Animal.Remove(sid);
            m_ActiveAnimal.Remove(animal);
            uiSys.DestroyUi(sid);
        }
        else
        {
            Debug.LogError($"删除动物失败");
        }
    }
    private Animal GetAnimalBySid(int sid)
    {
        if (!m_Sid2Animal.ContainsKey(sid))
        {
            return null;
        }
        return m_Sid2Animal[sid];
    }
    public string GetAnimalNameBySid(int sid)
    {
        return VarHelper.GetString(m_Sid2Animal[sid].m_Props[PropId.Name]);
    }

}

    //public void KillAnimalByType(string name)
    //{
    //    foreach (var obj in m_Sid2Animal)
    //    {
    //        var anim = obj.Value;
    //        if (anim.AnimalName == name)
    //        {
    //            // anim.Kill();
    //        }
    //    }
    //}

    //public void KillAnimal(int sid)
    //{
    //    var anim = GetAnimal(sid);
    //    if (anim != null)
    //    {
    //        // anim.Kill();

    //        m_Sid2Animal.Remove(sid);
    //        m_ActiveAnimal.Remove(anim);
    //    }
    //}

    //public void CreateAnim(string name)
    //{
    //    // name => //
    //    if (name == "chicken")
    //    {
    //        Chicken chicken = new Chicken();
    //        m_Sid2Animal.Add(chicken.InstanceId, chicken);
    //        m_ActiveAnimal.Add(chicken);
    //    }
    //}


    //ObjBase GetAnimal(int sid)
    //{
    //    if (!m_Sid2Animal.ContainsKey(sid))
    //    {
    //        return null;
    //    }

    //    return m_Sid2Animal[sid];
    //}



    //void OnUseMoney()
    //{
    //    // 
    //}

//public override void Update()
//{
//    base.Update();

//    foreach (var obj in m_ActiveAnimal)
//    {
//        int canEnterWater = obj.GetProp(PropId.CanEnterWater);
//        if (canEnterWater == 1)
//        {
//            // 下水
//        }
//        else
//        {
//            continue;
//        }
//    }
//    // animal
//    // 下水
//}
