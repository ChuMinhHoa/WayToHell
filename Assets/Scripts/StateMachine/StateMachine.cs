using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public T m_Owner;
    public State<T> m_CurrentState;
    public State<T> m_PrevState;
    public StateMachine(T owner) {
        m_Owner = owner;
        m_CurrentState = null;
    }
    public void Update()
    {
        if (m_CurrentState != null) m_CurrentState.Execute(m_Owner);
    }
    public void SetCurrentState(State<T> state) {
        if (m_CurrentState != null)
            m_CurrentState.Exit(m_Owner);
        m_PrevState = m_CurrentState;
        m_CurrentState = state;
        m_CurrentState.Enter(m_Owner);
    }
    public State<T> GetCurrentState() {
        return m_CurrentState;
    }

}
