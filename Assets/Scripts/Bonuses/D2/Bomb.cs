using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb: MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Racket racket))
        {
            racket.gameObject.SetActive(false);
            var balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (var ball in balls)
            {
                Destroy(ball);
            }
            StartCoroutine(DoCheck(racket));
        }
    }

    IEnumerator DoCheck(Racket racket)
    {
        yield return new WaitForSeconds(1);
        racket.GetGameData().Reset();
        racket.gameObject.SetActive(true);
        SceneManager.LoadScene("MainScene");
    }
}
