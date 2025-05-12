using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.QuestSystem
{
    public class QuestUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI progressText;

        [Space]
        [SerializeField] private Image progressBar;
        [SerializeField] private GameObject doneIcon;

        public Quest Quest { get; private set; }

        public void Setup(Quest quest)
        {
            Quest = quest;
            descriptionText.text = quest.Description;

            UpdateProgressUI(quest);
            quest.OnProgressUpdated += UpdateProgressUI;
        }

        private void UpdateProgressUI(Quest quest)
        {
            float progress = Mathf.Clamp01((float)quest.CurrentProgress / quest.Goal);

            progressBar.fillAmount = progress;
            progressText.text = quest.CurrentProgress.ToString();

            if (quest.IsCompleted)
            {
                doneIcon.SetActive(true);
                progressText.text = "";
            }
        }
    }
}