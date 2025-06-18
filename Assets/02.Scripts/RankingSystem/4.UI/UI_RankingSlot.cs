using TMPro;
using UnityEngine;

public class UI_RankingSlot : MonoBehaviour
{
    public TextMeshProUGUI RankingText;
    public TextMeshProUGUI NickNameText;
    public TextMeshProUGUI ScoreText;

    public void SetData(int rank, string nickname, int score)
    {
        RankingText.text = $"{rank}위";
        NickNameText.text = nickname;
        ScoreText.text = $"{score}점";
    }
}
