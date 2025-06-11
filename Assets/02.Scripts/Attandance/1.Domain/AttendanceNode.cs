using System;

public class AttendanceNode
{
    public bool IsClaimed { get; private set; }
    public int Day { get; }

    public int GoldAmount { get; }
    public int DiamondAmount { get; }

    public Action OnClaimed;

    public AttendanceNode(AttendanceNodeData data)
    {
        GoldAmount = data.GoldRewardAmount;
        DiamondAmount = data.DiamondRewardAmount;
        Day = data.Day;
        IsClaimed = false;
    }

    public void LoadClaimStatus(bool claimed)
    {
        IsClaimed = claimed;
    }

    public bool TryClaimReward(DateTime now)
    {
        if (IsClaimed)
            return false;

        if (!IsAvailable(now))
            return false;

        CurrencyManager.Instance.Add(ECurrencyType.Gold, GoldAmount);
        CurrencyManager.Instance.Add(ECurrencyType.Diamond, DiamondAmount);
        IsClaimed = true;
        OnClaimed?.Invoke();
        return true;
    }

    public bool IsAvailable(DateTime now)
    {
        return now.Day >= Day;
    }
}
