using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterArm : BodyPart {

    public override void LaunchAbility(Vector2 mousePosition) {
        Debug.Log(mousePosition);
        if (currentNodule != null) {
            switch (currentNodule.Id) {
                case 1:
                    for (int i = 0; i < 3; i++) {
                        Bullet newBullet = Instantiate(BasicPrefabs.Gino.Bullet);
                        newBullet.transform.position = transform.position;
                        newBullet.Velocity = (Quaternion.Euler(0, -30 + 30 * i, 0) * ((Vector3)mousePosition - transform.parent.position)).normalized * 5;
                        newBullet.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    break;
            }
        } else {
            Bullet newBullet = Instantiate(BasicPrefabs.Gino.Bullet);
            newBullet.transform.position = transform.position;
            newBullet.Velocity = ((Vector3)mousePosition - transform.parent.position).normalized * 5;
        }
    }
}
