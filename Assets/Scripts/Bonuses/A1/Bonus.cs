using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    [SerializeField] private Color _colorBackground;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Text _textComonent;
    [SerializeField] private Color _fontColor;
    [SerializeField] private string _text;

    private void Start()
    {
        _colorBackground.a = 1;
        _fontColor.a = 1;
        _sprite.color = _colorBackground;
        _textComonent.text = _text;
        _textComonent.color = _fontColor;
    }

    public virtual void BonusActivate(Racket racket)
    {
        racket.GetGameData().AddPoints(100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Racket racket))
        {
            BonusActivate(racket);
            Destroy(gameObject, 0.5f);
        }
        Destroy(gameObject, 0.5f);
    }
}
