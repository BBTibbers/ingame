using UnityEngine;
using UnityEngine.UI;

public class UI_Attendance : MonoBehaviour
{
    public GameObject attendancePanel;
    public Button openCloseButton;
    public Button claimButton;

    public AttendanceManager attendanceManager; // 씬에 있는 매니저 참조

    private void Start()
    {
        openCloseButton.onClick.AddListener(OpenClosePanel);
        claimButton.onClick.AddListener(OnClickClaim);

        UpdateClaimButtonState();
    }

    public void OpenClosePanel()
    {
        attendancePanel.SetActive(!attendancePanel.activeSelf);
        UpdateClaimButtonState();
    }
    private void OnClickClaim()
    {
        attendanceManager.OnClickClaimAll();
        UpdateClaimButtonState();
    }

    public void UpdateClaimButtonState()
    {
        bool canClaim = !attendanceManager.IsTodayChecked();
        claimButton.interactable = canClaim;
    }
}
