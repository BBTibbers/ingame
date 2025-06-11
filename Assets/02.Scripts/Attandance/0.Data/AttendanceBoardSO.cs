using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceBoard", menuName = "Game/Attendance Board")]
public class AttendanceBoardSO : ScriptableObject
{
    public List<AttendanceRewardSO> Rewards;
}