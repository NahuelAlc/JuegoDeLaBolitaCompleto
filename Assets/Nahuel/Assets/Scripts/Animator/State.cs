using UnityEngine;

public abstract class State<T> : MonoBehaviour
{
    protected T controller;
    public virtual void OnEnterState(T controller){
        this.controller = controller;
    }
    public abstract void OnUpdateState();
    public abstract void OnExitState();
}
