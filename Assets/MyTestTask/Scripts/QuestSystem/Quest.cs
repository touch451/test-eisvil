using System;

namespace Scripts.QuestSystem
{
    public abstract class Quest
    {
        public string Description { get; private set; }
        public int CurrentProgress { get; private set; }
        public int Goal { get; private set; }

        public bool IsCompleted => CurrentProgress >= Goal;
        public event Action<Quest> OnProgressUpdated;

        public Quest(string description, int goal)
        {
            Description = description;
            Goal = goal;
        }

        public abstract void Init();
        public abstract void Dispose();

        protected void UpdateProgress(int amount)
        {
            if (IsCompleted)
                return;

            CurrentProgress += amount;
            OnProgressUpdated?.Invoke(this);
        }
    }
}