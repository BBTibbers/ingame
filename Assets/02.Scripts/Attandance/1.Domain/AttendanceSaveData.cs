using System.Collections.Generic;
using System;

[Serializable]
public class AttendanceSaveData
{
    public List<AttendanceNodeData> Nodes;
    public string LastLoginDate;
    public bool TodayChecked;
    public int TotalCheckedDays; 
}