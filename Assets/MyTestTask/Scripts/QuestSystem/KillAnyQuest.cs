using Types;

namespace Scripts.QuestSystem
{
    public class KillAnyQuest : Quest
    {
        public KillAnyQuest(string description, int goal) : base(description, goal) { }

        public override void Init()
        {
            Cube.OnCubeKilled += OnKilled;
        }

        void OnKilled(CubeType type)
        {
            UpdateProgress(amount: 1);
        }

        public override void Dispose()
        {
            Cube.OnCubeKilled -= OnKilled;
        }
    }
}