using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingSys : SystemBase
{
    public int m_Coin = 1000;

    public override void Init(GameLuncher luncher)
    {
        base.Init(luncher);
        UiSys uiSys = luncher.GetSystem<UiSys>();
        uiSys.ShowPrice(m_Coin);
    }

    public void AddCoin(int coin)
    {
        m_Coin += coin;
        Debug.Log($"AddCoin: {coin}, total: {m_Coin}");
        UiSys uiSys = luncher.GetSystem<UiSys>();
        uiSys.ShowPrice(m_Coin);
    }
    public void RemoveCoin(int coin)
    {
    }
    public int GetCoin(int coin)
    {
        return m_Coin;
    }
    public void BuyAnimal(string name)
    {
        AnimalSys animalSys = luncher.GetSystem<AnimalSys>();
        int price = animalSys.GetAnimalPrice(name);
        if (m_Coin >= price)
        {
            m_Coin -= price;
            animalSys.CreateAnimal(name);
        }
        else
        {
            Debug.LogError("Coins not enough!");
        }
        UiSys uiSys = luncher.GetSystem<UiSys>();
        uiSys.ShowPrice(m_Coin);
    }
}
