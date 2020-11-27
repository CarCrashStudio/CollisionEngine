namespace CarKrash.Collision.Unity2D
{
    public class SkillChecks
    {
        public static int MakeCheck(Utils.Die rolledDie, int modifier)
        {
            var max = (int)rolledDie + 1;
            var roll = UnityEngine.Random.Range(1, max);
            //UnityEngine.Debug.Log($"Roll: {roll} Modifier: {modifier}");
            return roll + modifier;
        }
    }
}
