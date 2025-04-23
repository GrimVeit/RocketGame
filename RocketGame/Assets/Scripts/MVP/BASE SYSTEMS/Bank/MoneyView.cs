using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyView : View
{

    [SerializeField] private List<MoneyDisplayView> bankDisplayViews = new List<MoneyDisplayView>();

    public void Initialize()
    {
        for (int i = 0; i < bankDisplayViews.Count; i++)
        {
            bankDisplayViews[i].Initialize();
        }
    }

    public void SendMoney(float money)
    {
        for (int i = 0; i < bankDisplayViews.Count; i++)
        {
            bankDisplayViews[i].SendMoneyDisplay(money);
        }
    }

    public void AddMoney()
    {
        for (int i = 0; i < bankDisplayViews.Count; i++)
        {
            bankDisplayViews[i].AddMoney();
        }
    }

    public void RemoveMoney()
    {
        for (int i = 0; i < bankDisplayViews.Count; i++)
        {
            bankDisplayViews[i].RemoveMoney();
        }
    }
}
