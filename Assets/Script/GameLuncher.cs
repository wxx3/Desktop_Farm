using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLuncher : MonoBehaviour
{

    Dictionary<Type, SystemBase> m_Type2Sys = new Dictionary<Type, SystemBase>();

    List<ISystem> m_SysList = new List<ISystem>();
    //必须传入继承ISystem的接口
    public T GetSystem<T>() where T : class, ISystem//获取系统
    {
        Type type = typeof(T);
        if (m_Type2Sys.TryGetValue(type, out SystemBase system))
        {
            T result = system as T;
            return result;
        }

        return null;
    }

    void Awake()
    {
        AnimalSys animalSys = new AnimalSys();//管理动物的系统
        m_SysList.Add(animalSys);
        m_Type2Sys.Add(typeof(AnimalSys), animalSys);

        ShoppingSys shoppingSys = new ShoppingSys();//管理商店的系统（购买动物，购买食物）
        m_SysList.Add(shoppingSys);
        m_Type2Sys.Add(typeof(ShoppingSys), shoppingSys);

        ResourceSys resourceSys = new ResourceSys();
        m_SysList.Add(resourceSys);
        m_Type2Sys.Add(typeof(ResourceSys), resourceSys);

        WindowSys windowSys = new WindowSys();
        m_SysList.Add(windowSys);
        m_Type2Sys.Add(typeof(WindowSys), windowSys);

        UiSys uiSys = new UiSys();
        m_SysList.Add(uiSys);
        m_Type2Sys.Add(typeof(UiSys), uiSys);

        ProductSys productSys = new ProductSys();
        m_SysList.Add(productSys);
        m_Type2Sys.Add(typeof(ProductSys), productSys);

        for (int i = 0; i < m_SysList.Count; i++)
        {
            m_SysList[i].Init(this);
        }
        OnCreate();
    }
    void Update()//统一刷新
    {
        for (int i = 0, len = m_SysList.Count; i < len; i++)
        {
            m_SysList[i].Update();
        }
    }

    void OnCreate()
    {
        AnimalSys animalSys = GetSystem<AnimalSys>();
        animalSys.CreatAnimal("chicken");
        Debug.Log("创建小鸡！");
    }
}
