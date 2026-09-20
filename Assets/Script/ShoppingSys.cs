using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingSys : SystemBase
{
    public int m_Coin = 0;
    private UiSys m_uiSys;

    public override void Init(GameLuncher luncher)
    {
        base.Init(luncher);
        m_uiSys = luncher.GetSystem<UiSys>();
        ShowCoin();
    }

    public void AddCoin(int coin)
    {
        m_Coin += coin;
        Debug.Log($"AddCoin: {coin}, total: {m_Coin}");
        m_uiSys.ShowCoin(m_Coin);
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
            Debug.LogWarning("Coins not enough!");
        }
        ShowCoin();
    }
    public void SellAnimalById(int index)
    {
        AnimalSys animalSys = luncher.GetSystem<AnimalSys>();  
        string name = animalSys.GetAnimalNameBySid(index);
        int price = animalSys.GetAnimalSellPrice(name);
        AddCoin(price);
        animalSys.KillAnimalBySid(index);
        ShowCoin();
    }
    private void ShowCoin()
    {
        m_uiSys.ShowCoin(m_Coin);
    }

}
