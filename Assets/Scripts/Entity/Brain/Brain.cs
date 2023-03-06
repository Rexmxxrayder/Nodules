using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Brain : MonoBehaviour {
    [SerializeField] protected UnityEvent firstAbility = new UnityEvent();
    [SerializeField] protected UnityEvent secondAbility = new UnityEvent();
    [SerializeField] protected UnityEvent thirdAbility = new UnityEvent();
    [SerializeField] protected UnityEvent fourthAbility = new UnityEvent();
    [SerializeField] protected UnityEvent fifthAbility = new UnityEvent();
    [SerializeField] protected UnityEvent<Vector2> newMovePoint = new UnityEvent<Vector2>();
    [SerializeField] protected Vector2 visor;

    #region Properties
    public event UnityAction FirstAbility { add { firstAbility.AddListener(value); } remove { firstAbility.RemoveListener(value); } }
    public event UnityAction SecondAbility { add { secondAbility.AddListener(value); } remove { secondAbility.RemoveListener(value); } }
    public event UnityAction ThirdAbility { add { thirdAbility.AddListener(value); } remove { thirdAbility.RemoveListener(value); } }
    public event UnityAction FourthAbility { add { fourthAbility.AddListener(value); } remove { fourthAbility.RemoveListener(value); } }
    public event UnityAction FifthAbility { add { fifthAbility.AddListener(value); } remove { fifthAbility.RemoveListener(value); } }
    public event UnityAction<Vector2> NewMovePoint { add { newMovePoint.AddListener(value); } remove { newMovePoint.RemoveListener(value); } }
    public Vector2 Visor { get { return visor; } }
    #endregion
}
