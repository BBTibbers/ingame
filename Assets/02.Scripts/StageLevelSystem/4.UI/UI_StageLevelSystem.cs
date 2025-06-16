using TMPro;
using UnityEngine;

public class UI_StageLevelSystem : MonoBehaviour
{

    public TextMeshProUGUI StageLevelText;
    public TextMeshProUGUI TimeText;
    public StageLevelManager StageLevelManager;

    // Update is called once per frame
    private void Awake()
    {
        StageLevelManager.OnLevelChanged += SetStageLevelText;
    }
    void Update()
    {
        SetTimetext();
    }

    public void SetStageLevelText()
    {
        int stageLevel = StageLevelManager.Level;
        StageLevelText.text = "LV. " + stageLevel.ToString();
    }
    private void SetTimetext()
    {
        TimeText.text = string.Format("{0:00}:{1:00}", (int)(Time.time / 60), (int)(Time.time % 60));
    }
}
