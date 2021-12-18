using UnityEngine;

public class CancelWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Racket>(out Racket racket))
        {
            racket.CancelWeapon();
        }
        Destroy(this.gameObject);
    }
}
