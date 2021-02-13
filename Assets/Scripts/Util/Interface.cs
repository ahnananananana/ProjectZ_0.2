using UnityEngine;
using UnityEngine.AI;
using System;

namespace HDV
{
    public interface IStateObject
    {
    }

    public interface IStateComponent
    {
        //bool IsActive { get; set; }
    }

    public interface IEventCollection<T>
    {
        event Action<T> AddEvent, RemoveEvent;
    }

    public interface IEventCollection
    {
        event Action OnAddEvent, OnRemoveEvent;
    }

    public interface IAttackable
    {
        void OnAttackHit(object source, ContactData contactData);
    }

    public interface ITargetable
    {
        Transform Transform { get; }
        Vector3 TargetPoint { get; }
        event Action<ITargetable> DisableEvent;
    }

    public interface IToggle
    {
        void SetToggle(bool value);
    }

    public interface IMovableUI
    {
        RectTransform RectTransform { get; }
        RectTransform Container { get; }
    }

    public interface IViewTemplate<M>
    {
        void Init(M model);
        M Model { get; }
    }

    public interface IEquiptable
    {
    }

    public interface IEquiptableModel
    {
        void Equip(IEquiptable equiptable);
    }

    public enum Rank
    {
    }

    public interface IRank
    {
        Rank Rank { get; }
    }

    public interface IUIAnimator
    {
        UIAnimator UIAnimator { get; }
    }

    public interface IHealthObject
    {
        StatCurrent HealthPoint { get; }
    }

    public interface IAnimator
    {
        Animator Animator { get; }
    }

    public interface ITransform
    {
        Transform Transform { get; }
    }

    public interface ILookAtTransform
    {
        Transform Transform { get; }
        EventVector3 LookDirection { get; }
    }

    public interface INavMeshMoveSystem
    {
        NavMeshMoveSystem MoveSystem { get; }
        Transform Target { get; }
    }

    public interface ITargetDistance
    {
        Transform Transform { get; }
        EventObject<Transform> Target { get; }
        EventFloat TargetDistance { get; }
    }

    public interface IInteractable
    {
        Vector3 Position { get; }
        void Interact(object interactor);
        void SetHighlight(bool value);
    }

    public interface IHittable
    {
        float TakeDamage(DamageData damageData, ContactData contactData);
    }

    public interface IDestructable
    {
        void Destruct();
    }

    public interface IExplosive
    {
        void Explode();
    }

    public interface IResetable
    {
        void Reset();
    }

    public interface IAudioSource
    {
        AudioSource AudioSource { get; }
    }
}