using Types;

namespace Scripts.QuestSystem
{
    public class KillByTypeQuest : Quest
    {
        private CubeType _targetType;

        public KillByTypeQuest(string description, int goal, CubeType type) : base(description, goal)
        {
            _targetType = type;
        }

        public override void Init()
        {
            Cube.OnCubeKilled += OnKilled;
        }

        void OnKilled(CubeType type)
        {
            if (type == _targetType)
                UpdateProgress(amount: 1);
        }

        public override void Dispose()
        {
            Cube.OnCubeKilled -= OnKilled;
        }
    }
}