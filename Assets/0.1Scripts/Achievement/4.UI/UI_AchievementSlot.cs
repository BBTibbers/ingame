using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{

    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public Slider ProgressSldier;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDate;
    public Button RewardClaimButton;

    private AchievementDTO _achievementDTO;


    public void Refresh(AchievementDTO achievementDTO)
    {
        _achievementDTO = achievementDTO;

        NameTextUI.text = _achievementDTO.Name;
        DescriptionTextUI.text = _achievementDTO.Description;
        RewardCountTextUI.text = _achievementDTO.RewardAmount.ToString() + "개 보상 획득 가능";
        ProgressSldier.value = (float)_achievementDTO.CurrentValue / (float)_achievementDTO.GoalValue;
        ProgressTextUI.text = this._achievementDTO.CurrentValue.ToString() + " / " + _achievementDTO.GoalValue.ToString();

        RewardClaimButton.interactable = _achievementDTO.CanClaimReward();
    }

    public void ClaimReward()
    {
        if (AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
        }
        else
        {
        }
    }
}
