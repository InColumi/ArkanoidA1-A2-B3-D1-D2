using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private bool _isReload = true;
    [SerializeField] private GameObject _patronObj;
    private Patron _patron;

    private void Start()
    {
        _patron = _patronObj.GetComponent<Patron>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _isReload == true)
        {
            _patron.StartFly(transform.position.x, transform.position.y);
            _isReload = false;
            StartCoroutine(Reloader());
        }
    }

    public void TakePatron(Patron patron)
    {
        if (patron != null)
        {
            _patron = patron;
        }
        else
        {
            throw new System.Exception("Patron is null!!!");
        }
    }

    IEnumerator Reloader()
    {
        yield return new WaitForSeconds(1.5f);
        _isReload = true;
    }
}
