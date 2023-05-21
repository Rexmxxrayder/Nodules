using UnityEngine;
using Sloot;

public class CouardBrain : EntityBrain {
    public Transform target;
    [SerializeField] float distDetect;
    [SerializeField] float timeNewDirection;
    bool awake = false;
    Timer newDirection;

    protected override void StartSetup() {
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
        newDirection = new Timer(this, timeNewDirection, NewDirection);
        Get<EntityHealth>().OnDamaged += (x) => AwakeCouard();
    }


    private void Update() {
        if (!awake && Vector3.Distance(gameObject.GetRootPosition(), target.position) <= distDetect) {
            AwakeCouard();
        }
        if(awake) {
            ShootOnEnemy();
        }
    }

    public void ShootOnEnemy() {
        visor = target.transform.position;
        if (Get<EntityBodyParts>().Bodyparts[0].Available) {
            Get<EntityBodyParts>().Bodyparts[0].OnButtonUp(this);
        }
    }

    public void NewDirection() {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        visor = direction.normalized * 100 + gameObject.GetRootPosition();
        Get<EntityBodyParts>().Bodyparts[1].OnButtonUp(this);
    }

    public void AwakeCouard() {
        if (awake) {
            return;
        } else {
            awake= true;
        }
        newDirection.Start();
        ShootOnEnemy();
    }

}
