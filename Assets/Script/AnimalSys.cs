using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalSys : SystemBase
{
    Dictionary<int, Animal> m_Sid2Animal = new Dictionary<int, Animal>();
    List<Animal> m_ActiveAnimal = new List<Animal>();
    public override void Update()
    {
        for (int i = 0; i <= m_ActiveAnimal.Count - 1; i++)
        {
            m_ActiveAnimal[i].OnUpdate();
        }
    }
    public void CreatAnimal(string name)
    {
        ResourceSys resourceSys = luncher.GetSystem<ResourceSys>();
        UiSys uiSys = luncher.GetSystem<UiSys>();

        //animalPrefab = UnityEngine.Object.Instantiate(animalPrefab);
        Animal animal = null;
        if(name == "chicken")
        {
            animal = new Chicken();
            animal.OnCreate();
            //Debug.LogError("创建ui");
            uiSys.CreateUi(animal);//ui绑定逻辑
            m_Sid2Animal.Add(animal.InstanceId, animal);
            m_ActiveAnimal.Add(animal);
        }
        if(animal != null)
        {
            animal.OnEntityEvent += HandleEntityEvent;
        }
    }
    //todo:生蛋逻辑
    private void HandleEntityEvent(EntityBase entity, string eventName, object data)
    {
        switch (eventName)
        {
            case "SpawnEgg":
                SpawnEgg(entity);
                break;
        }
    }
    private void SpawnEgg(EntityBase entity)
    {
        UiSys uiSys = luncher.GetSystem<UiSys>();
        Egg egg = new Egg();
        egg.m_Props.Add(PropId.PosX, entity.m_Props[PropId.PosX]);
        egg.m_Props.Add(PropId.PosY, entity.m_Props[PropId.PosY]);
        uiSys.CreateUi(egg);
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
