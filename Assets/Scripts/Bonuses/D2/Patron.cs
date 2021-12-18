using System.Collections;
using UnityEngine;

public class Patron : MonoBehaviour
{
    [SerializeField] private GameObject _prefubPatron;
    [SerializeField] private int _damage;
    [SerializeField] private uint _initialSpeed = 300;

    public void StartFly(float x, float y)
    {
        var patron = Instantiate(_prefubPatron, new Vector2(x, y + 10f), Quaternion.identity);
        patron.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _initialSpeed);
    }

    public int GetDamege()
    {
        return _damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
