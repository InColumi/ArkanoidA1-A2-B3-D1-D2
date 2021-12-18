public class Slow : Bonus
{
    public override void BonusActivate(Racket racket)
    {
        racket.DoSlowBalls(0.9f);
    }
}
