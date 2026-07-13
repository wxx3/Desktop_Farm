using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AnimalSys : SystemBase
{

    //实现动物的创建，销毁，更新和获取，还有动物的事件处理
    Dictionary<int, Animal> m_Sid2Animal = new Dictionary<int, Animal>();
    List<Animal> m_ActiveAnimal = new List<Animal>();
    ResourceSys m_Resources;
    UiSys uiSys;
    public override void Update()
    {
        for (int i = 0; i <= m_ActiveAnimal.Count - 1; i++)
        {
            m_ActiveAnimal[i].OnUpdate();
        }
    }
    public void CreatAnimal(string name)
    {
        m_Resources = luncher.GetSystem<ResourceSys>();
        uiSys = luncher.GetSystem<UiSys>();

        Animal animal = null;
        if(name == "chicken")//可以改成switch
        {
            animal = new Chicken();
        }
        if(animal != null)
        {
            animal.OnCreate();
            uiSys.CreateUi(animal);//ui绑定逻辑
            m_Sid2Animal.Add(animal.InstanceId, animal);
            m_ActiveAnimal.Add(animal);
            animal.OnEntityEvent += HandleEntityEvent;
        }
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
        productSys.CreateProduct("egg", posX, posY);
    }
    public void KillAnimalBySid(int sid)
    {
        if(GetAnimalBySid(sid) != null)
        {
            //animal去自己销毁
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

    //public override void Init(GameLuncher gameLuncher)
    //{
    //    base.Init(gameLuncher);

    //    gameLuncher.GetSystem<MsgSystem>().RegsignMsg(CommandID.OnUseMoney, OnUseMoney);
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
