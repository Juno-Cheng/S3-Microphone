using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float Health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(float dmg) {
        Health -= dmg;
        if (Health < 0) {
            die();
        }
    }
    public void die() {
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.name.Contains("Laser")) {
            damage(100);
        }
    }
}
