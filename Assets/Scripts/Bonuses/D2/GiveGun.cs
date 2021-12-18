using UnityEngine;

public class GiveGun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Racket>(out Racket racket))
        {
            racket.ActivateGun();
        }
        Destroy(this.gameObject);
    }
}
