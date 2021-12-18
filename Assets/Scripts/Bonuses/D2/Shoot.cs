using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private bool _isReload = false;
     private Patron _patron;
    //[SerializeField] private GameObject _superPricel;
    //[SerializeField] private Patron _superPatron;
    //[SerializeField] private Patron _choosePatron;
    //[SerializeField] private GameObject _choosePricel;

    private void Start()
    {
        _patron.GetComponent<Patron>();
    }


    public void BonusSuperShoot()
    {
        _isReload = true;
    }

    //public void CancelShoot()
    //{
    //    _weapon.SetActive(false);
    //    _isReload = false;
    //}

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0) && _isReload == true)
        //{
        //    Patron patron = Instantiate(_patron, new Vector2(_weapon.transform.position.x, _weapon.transform.position.y + 2f), Quaternion.identity);
        //    patron.StartFly();
        //    _isReload = false;
        //    StartCoroutine(Reloader());
        //}
        
        /*
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            BonusShoot();
        }
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            BonusSuperShoot();
        }
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            CancelShoot();
        }*/
    }

    IEnumerator Reloader()
    {
        yield return new WaitForSeconds(1.5f);
        _isReload = true;
    }
}
