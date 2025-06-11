using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_AttendanceNode : MonoBehaviour
{
    [Header("보상 UI")]
    public GameObject goldIcon;
    public TextMeshProUGUI goldText;

    public GameObject diamondIcon;
    public TextMeshProUGUI diamondText;

    [Header("마스크 (수령 시 표시)")]
    public GameObject mask;

    /// <summary>
    /// 슬롯 정보를 기반으로 UI를 갱신합니다.
    /// </summary>
    public void Refresh(AttendanceNode node)
    {
        // 골드 보상 표시
        if (node.GoldAmount > 0)
        {
            goldIcon.SetActive(true);
            goldText.gameObject.SetActive(true);
            goldText.text = $"{node.GoldAmount}";
        }
        else
        {
            goldIcon.SetActive(false);
            goldText.gameObject.SetActive(false);
        }

        // 다이아 보상 표시
        if (node.DiamondAmount > 0)
        {
            diamondIcon.SetActive(true);
            diamondText.gameObject.SetActive(true);
            diamondText.text = $"{node.DiamondAmount}";
        }
        else
        {
            diamondIcon.SetActive(false);
            diamondText.gameObject.SetActive(false);
        }

        // 수령 여부에 따른 마스크 처리
        mask.SetActive(node.IsClaimed);
    }
}
