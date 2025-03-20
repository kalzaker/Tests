namespace Enemy.Config
{
    public class EnemySpawnConfig
    {
        public readonly float MinXSpawn = 0.1f;
        public readonly float MaxXSpawn = 2f;
        public readonly float MinYSpawn = -0.1f;
        public readonly float MaxYSpawn = 1f;
        public readonly float OutsideRightX = 1.1f;
        public readonly float OutsideLeftX = -0.1f;
        public readonly float OutsideTopY = 1.1f;
        public readonly float OutsideBottomY = -0.1f;
        public readonly float ThresholdX = 1f;
        public readonly float ThresholdY = 0.5f;
    }
}