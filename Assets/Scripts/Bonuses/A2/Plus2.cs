public class Plus2 : Bonus
{
    public override void BonusActivate(Racket racket)
    {
        racket.GetGameData().IncreaseBalls();
        racket.GetGameData().IncreaseBalls();
        racket.CreateBalls(2, true);
    }
}
