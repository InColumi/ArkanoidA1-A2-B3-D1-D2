using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector2 InitialForce;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _racket;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private GameDataScript _gameData;
    private AudioSource _audioSrc;

    private float _deltaX;

    void Start()
    {
        _deltaX = transform.position.x;
        _rigidbody = GetComponent<Rigidbody2D>();
        _racket = GameObject.FindGameObjectWithTag("Player");
        _deltaX = transform.position.x;
        _audioSrc = GameObject.FindGameObjectWithTag("Audio Sourse").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_rigidbody.isKinematic)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(InitialForce);
            }
            else
            {
                var pos = transform.position;
                pos.x = _racket.transform.position.x + _deltaX;
                transform.position = pos;
            }
        }

        if (!_rigidbody.isKinematic && Input.GetKeyDown(KeyCode.J))
        {
            var v = _rigidbody.velocity;
            if (Random.Range(0, 2) == 0)
            {
                v.Set(v.x - 10f, v.y + 10f);
            }
            else
            {
                v.Set(v.x + 10f, v.y - 10f);
            }
            _rigidbody.velocity = v;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        if (_audioSrc != null)
        {
            if (_gameData.IsEnebledSound())
            {
                _audioSrc.PlayOneShot(_loseSound, 0.5f);
                _racket.GetComponent<Racket>().BallDestroyed(this);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_audioSrc != null)
        {
            if (_gameData.IsEnebledSound())
            {
                _audioSrc.PlayOneShot(_hitSound, 0.5f);
            }
        }
    }
}
