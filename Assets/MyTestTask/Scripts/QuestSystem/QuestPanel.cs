using System.Collections.Generic;
using Types;
using UnityEngine;

namespace Scripts.QuestSystem
{
    public class QuestPanel : MonoBehaviour
    {
        [SerializeField] private QuestUI questUIPrefab;
        [SerializeField] private Transform questUIContainer;

        private List<Quest> _quests = new List<Quest>();

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            AddTimerQuest("Пробыть в игре 120 секунд", 120);
            AddKillAnyQuest("Уничтожь 10 любых кубов", 10);
            AddKillByTypeQuest("Уничтожь 10 красных кубов", 10, CubeType.Red);

            foreach (var quest in _quests)
            {
                var ui = Instantiate(questUIPrefab, questUIContainer);
                ui.Setup(quest);
                quest.Init();
            }
        }

        private void AddTimerQuest(string description, int goalSeconds)
        {
            _quests.Add(new TimerQuest(description, 120, this));
        }

        private void AddKillAnyQuest(string description, int goal)
        {
            _quests.Add(new KillAnyQuest(description, goal));
        }

        private void AddKillByTypeQuest(string description, int goal, CubeType type)
        {
            _quests.Add(new KillByTypeQuest(description, goal, type));
        }
    }
}