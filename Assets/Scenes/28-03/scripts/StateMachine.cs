using UnityEngine;

public abstract class IState
{

    public PlayerController2D owner;

    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }

}
public class StateMachine : MonoBehaviour
{
    public IState statoCorrente;

    public StateMachine(IState stato)
    {
        SetState(stato);
    }


    public void StateUpdate()
    {
        if (statoCorrente != null)
            statoCorrente.Execute();
    }

    public void SetState(IState nuovoStato)
    {
        if (statoCorrente != null)
        {
            statoCorrente.Exit();
        }

        statoCorrente = nuovoStato;

        if (statoCorrente != null)
        {
            statoCorrente.Enter();
        }



    }






}