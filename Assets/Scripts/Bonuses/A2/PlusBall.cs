public class PlusBall : Bonus
{
    public override void BonusActivate(Racket racket)
    {
        racket.GetGameData().IncreaseBalls();
    }
}
