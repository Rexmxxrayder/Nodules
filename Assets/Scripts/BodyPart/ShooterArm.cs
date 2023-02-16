using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterArm : BodyPart {
    [SerializeField] Bullet bulletPrefab;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot(Input.mousePosition);
        }
    }


    void Shoot(Vector3 mousePosition) {
        if (currentNodule != null) {
            switch (currentNodule.Id) {
                case 1:
                    for (int i = 0; i < 3; i++) {
                        Bullet newBullet = Instantiate(bulletPrefab);
                        newBullet.transform.position = transform.position;
                        newBullet.Velocity = (Quaternion.Euler(0, -30 + 30 * i, 0) * (Camera.main.ScreenToWorldPoint(mousePosition) - transform.parent.position)).normalized * 5;
                        newBullet.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    break;
            }
        } else {
            Bullet newBullet = Instantiate(bulletPrefab);
            newBullet.transform.position = transform.position;
            newBullet.Velocity = (Camera.main.ScreenToWorldPoint(mousePosition) - transform.parent.position).normalized * 5;
        }
    }
}
