using UnityEngine;

public class GiveCannon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Racket>(out Racket racket))
        {
            racket.ActivateCannon();
        }
        Destroy(this.gameObject);
    }
}
