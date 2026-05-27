public class LevelsData
{
    public static int[] completedLevelsMap = new int[2];

    public static int completedAllLevels
    {
        get
        {
            int value = 0;

            foreach (int level in completedLevelsMap)
            {
                value += level;
            }

            return value;
        }
    }
}
