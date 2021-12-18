public class Plus10 : Bonus
{
    public override void BonusActivate(Racket racket)
    {
        int count = 10;
        for (int i = 0; i < count; i++)
        {
            racket.GetGameData().IncreaseBalls();
        }
        racket.CreateBalls(10, true);
    }
}
