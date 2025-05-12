using UnityEngine;
using System.Collections;

namespace Scripts.QuestSystem
{
    public class TimerQuest : Quest
    {
        private MonoBehaviour _mono;
        private WaitForSeconds wait1Sec = new WaitForSeconds(1f);

        Coroutine timerCoroutine = null;

        public TimerQuest(string description, int goalSeconds, MonoBehaviour mono) : base(description, goalSeconds)
        {
            _mono = mono;
        }

        public override void Init()
        {
            _mono.StartCoroutine(TrackTime());
        }

        private IEnumerator TrackTime()
        {
            while (!IsCompleted)
            {
                yield return wait1Sec;
                UpdateProgress(amount: 1);
            }
        }

        public override void Dispose()
        {
            if (timerCoroutine != null)
                _mono?.StopCoroutine(timerCoroutine);
        }
    }
}