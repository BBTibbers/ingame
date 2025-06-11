using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceReward", menuName = "Game/Attendance Reward")]
public class AttendanceRewardSO : ScriptableObject
{
    public int Day;
    public int GoldRewardAmount;
    public int DiamondRewardAmount;
}
