using System;

public class MoneyPresenter : IMoneyProvider
{
    private readonly MoneyModel _model;
    private readonly MoneyView _view;

    public MoneyPresenter(MoneyModel model, MoneyView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        _model.Initialize();
        _view.Initialize();

        _model.OnAddMoney += _view.AddMoney;
        _model.OnRemoveMoney += _view.RemoveMoney;
        _model.OnChangeMoney += _view.SendMoney;

        _view.SendMoney(_model.Money);
    }

    public void Dispose()
    {
        _model.OnAddMoney -= _view.AddMoney;
        _model.OnRemoveMoney -= _view.RemoveMoney;
        _model.OnChangeMoney -= _view.SendMoney;

        _model.Destroy();
    }

    #region Input

    public void SendMoney(int money)
    {
        _model.SendMoney(money);
    }

    public void SendMoney(float money)
    {
        _model.SendMoney(money);
    }

    public bool CanAfford(float bet)
    {
        return _model.CanAfford(bet);
    }

    public float GetMoney() => _model.Money;

    #endregion

    #region Output

    public event Action<float> OnChangeMoney
    {
        add { _model.OnChangeMoney += value; }
        remove { _model.OnChangeMoney -= value; }
    }

    #endregion
}

public interface IMoneyProvider
{
    event Action<float> OnChangeMoney;

    bool CanAfford(float money);
    float GetMoney();
    void SendMoney(float money);
}


