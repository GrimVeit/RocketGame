using System;

public class BetSelectModel
{
    public event Action OnIncreaseBet;
    public event Action OnDecreaseBet;

    public event Action<float> OnSetBet;

    public void IncreaseBet()
    {
        OnIncreaseBet?.Invoke();
    }

    public void DecreaseBet()
    {
        OnDecreaseBet?.Invoke();
    }

    public void SetBet(float value)
    {
        OnSetBet?.Invoke(value);
    }
}
