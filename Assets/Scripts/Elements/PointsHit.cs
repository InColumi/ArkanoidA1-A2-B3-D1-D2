using UnityEngine;
using UnityEngine.UI;

public class PointsHit : MonoBehaviour
{
    [SerializeField] private GameObject _textObject;
    [SerializeField] private int _hitPoints;
    [SerializeField] private int _points;
    private Racket _racket;
    private Text _text;

    void Start()
    {
        if (_textObject != null)
        {
            _text = _textObject.GetComponent<Text>();
            _text.text = _hitPoints.ToString();
        }
        _racket = GameObject.FindGameObjectWithTag("Player").GetComponent<Racket>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Patron>(out Patron patron))
        {
            _hitPoints -= patron.GetDamege();   
        }
        else
        {
            --_hitPoints;
        }

        if (_hitPoints <= 0)
        {
            Destroy(gameObject);
            _racket.AddPointsFromBlock(_points);
        }

        if (_text != null)
        {
            _text.text = _hitPoints.ToString();
        }
    }
}