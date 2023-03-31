using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLauncherBeta : MonoBehaviour {
    public Transform target;
    public Rigidbody2D rb;
    public float jumpCooldown;
    public float jumpLenght;
    public Vector2 targetDist;
    Timer shootJump;


    private void Start() {
        shootJump = new Timer(jumpCooldown, ShootJump).Start();
    }

    void ShootJump() {
        Bullet b = Instantiate(BasicPrefabs.Gino.Bullet);
        b.transform.position= transform.position;
        b.Velocity = (target.position - transform.position).normalized * 5;
        StartCoroutine(Jump());
    }

    IEnumerator Jump() {
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * jumpLenght;
        if(Vector3.Distance(transform.position, target.position) > targetDist.y) {
            dir += (target.position - transform.position).normalized * 1;
        } else if (Vector3.Distance(transform.position, target.position) < targetDist.x) {
            dir -= (target.position - transform.position).normalized * 1;
        }
        rb.velocity = dir;
        yield return new WaitForSeconds(jumpCooldown - 0.1f);
        rb.velocity = Vector3.zero;
    }

}
