using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


//���P�_
public class StateMachine<TOwner>
{

    public abstract class State 
    {
        /// <summary>�X�e�[�g���Ǘ����Ă���X�e�[�g�}�V��</summary>
        protected StateMachine<TOwner> StateMachine => stateMachine;
        internal StateMachine<TOwner> stateMachine;
        /// <summary>�J�ڂ̈ꗗ</summary>
        internal Dictionary<int, State> transitions = new Dictionary<int, State>();

        /// <summary>���̃X�e�[�g�̃I�[�i�[</summary>
        protected TOwner Owner => stateMachine.Owner;

        internal void Enter(State currentState, CancellationToken token) 
        {
            OnEnter(currentState, token);
        }
        protected virtual void OnEnter(State currentState, CancellationToken token) { }

        internal void Update() 
        {
            OnUpdate();
        }

        protected virtual void OnUpdate() { }

        internal void Exit(State nextState) 
        {
            OnExit(nextState);
        }
        protected virtual void OnExit(State nextState){ }
    }

    /// <summary>���݂̃X�e�[�g</summary>
    public State CurrentState { get; private set; }

    //�X�e�[�g���X�g
    private LinkedList<State> states = new LinkedList<State>();

    /// <summary>�K����State�ɑJ�ڂ��邽�߂̂���̃N���X</summary>
    public sealed class AnyState : State { }

    /// <summary>���̃X�e�[�g�}�V���̃I�[�i�[</summary>
    public TOwner Owner { get; }

    public StateMachine(TOwner owner) 
    {
        Owner = owner;
    }

    /// <summary>
    /// �X�e�[�g��ǉ�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Add<T>() where T : State, new() 
    {
        var state = new T();
        state.stateMachine = this;
        states.AddLast(state);
        return state;
    }

    /// <summary>
    /// �w�肵��State���擾�A�Ȃ���ΐ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetOrAddState<T>() where T : State, new() 
    {
        foreach (var state in states) 
        {
            if (state is T result) 
            {
                return result;
            }
        }

        return Add<T>();
    }

    /// <summary>
    /// �J�ڂ��`���邽�߂̃��\�b�h
    /// </summary>
    /// <typeparam name="Tdo">�J�ڌ�</typeparam>
    /// <typeparam name="Tto">�J�ڐ�</typeparam>
    /// <param name="eventID">�C�x���gID</param>
    public void AddTransition<TFrom, Tto>(int eventId)
        where TFrom : State, new()
        where Tto : State, new()
    {
        var from = GetOrAddState<TFrom>();
        if (from.transitions.ContainsKey(eventId)) 
        {
            throw new System.ArgumentException($"�X�e�[�g{nameof(TFrom)}�ɑ΂��ăC�x���gID{eventId.ToString()}�̑J�ڂ͒�`�ς݂ł�");
        }

        var to = GetOrAddState<Tto>();
        from.transitions.Add(eventId, to);
    }

    /// <summary>
    /// �ǂ̃X�e�[�g����ł�����̃X�e�[�g�ɑJ�ڂł���C�x���g��ǉ�����
    /// </summary>
    public void AddAnyTranstion<Tto>(int eventId) where Tto : State, new() 
    {
        AddTransition<AnyState, Tto>(eventId);
    }

    /// <summary>
    /// �X�e�[�g�}�V���̎��s���J�n����i�W�F�l���b�N�Łj
    /// </summary>
    public void Start<TFirst>(CancellationToken token) where TFirst : State, new()
    {
        Start(GetOrAddState<TFirst>(), token);
    }

    public void Start(State firstState, CancellationToken token) 
    {
       CurrentState = firstState;
        CurrentState.Enter(null, token);
    }

    public void Update() 
    {
        CurrentState.Update();
    }

    /// <summary>
    /// �J�ڃC�x���g�𔭍s����
    /// </summary>
    /// <param name="eventId">�C�x���gID</param>
    public void Dispatch(int eventId, CancellationToken Token) 
    {
        State to;

        if (!CurrentState.transitions.TryGetValue(eventId, out to)) 
        {
            if (!GetOrAddState<AnyState>().transitions.TryGetValue(eventId, out to)) 
            {
                return;
            }
        }

        Change(to, Token);
    }

    /// <summary>
    /// �X�e�[�g��ύX����
    /// </summary>
    /// <param name="nextState">�J�ڐ�̃X�e�[�g</param>
    public void Change(State nextState, CancellationToken Token) 
    {
        CurrentState.Exit(nextState);
        nextState.Enter(null, Token);
        CurrentState = nextState;
    }
}

