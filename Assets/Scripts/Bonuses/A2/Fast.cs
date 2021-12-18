public class Fast : Bonus
{
    public override void BonusActivate(Racket racket)
    {
        racket.DoFastBalls(0.5f);
    }
}