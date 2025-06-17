/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Entities;
using System;
using Atomic.Elements;

namespace Game.Gameplay
{
	public static class EntityAPI
	{
		///Tags
		public const int Damageable = 563499515;
		public const int Character = 294335127;


		///Values
		public const int GameObject = 1482111001; // GameObject
		public const int Transform = -180157682; // Transform
		public const int MoveRequest = 608421636; // IRequest<Vector3>
		public const int MoveEvent = 735308719; // IEvent<Vector3>
		public const int MoveSpeed = 526065662; // IValue<float>
		public const int LookRequest = -266010916; // IRequest<Vector3>
		public const int RotateSpeed = -1838353354; // IValue<float>
		public const int Health = -915003867; // Health
		public const int Lifetime = -997109026; // Cooldown
		public const int TakeDamageEvent = 1486057413; // IEvent<DamageArgs>
		public const int TakeDeathEvent = 420717635; // IEvent<DamageArgs>
		public const int DestroyAction = 85938956; // IAction
		public const int Team = 1691486497; // IReactiveVariable<TeamType>
		public const int Weapon = 1855955664; // IEntity
		public const int Damage = 375673178; // IValue<int>
		public const int Target = 1103309514; // IReactiveVariable<IEntity>
		public const int FirePoint = 397255013; // Transform
		public const int FireRequest = 1469079819; // IRequest
		public const int FireCondition = -280402907; // IFunction<bool>
		public const int FireEvent = -1683597082; // IEvent
		public const int Trigger = -707381567; // TriggerEventReceiver
		public const int MeshRenderer = 1670531983; // Renderer
		public const int Animator = -1714818978; // Animator


		///Tag Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamageableTag(this IEntity obj) => obj.HasTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddDamageableTag(this IEntity obj) => obj.AddTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamageableTag(this IEntity obj) => obj.DelTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacterTag(this IEntity obj) => obj.HasTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacterTag(this IEntity obj) => obj.AddTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacterTag(this IEntity obj) => obj.DelTag(Character);


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject GetGameObject(this IEntity obj) => obj.GetValueUnsafe<GameObject>(GameObject);

		public static ref GameObject RefGameObject(this IEntity obj) => ref obj.GetValueUnsafe<GameObject>(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameObject(this IEntity obj, out GameObject value) => obj.TryGetValueUnsafe(GameObject, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameObject(this IEntity obj, GameObject value) => obj.AddValue(GameObject, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameObject(this IEntity obj) => obj.HasValue(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameObject(this IEntity obj) => obj.DelValue(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameObject(this IEntity obj, GameObject value) => obj.SetValue(GameObject, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetTransform(this IEntity obj) => obj.GetValueUnsafe<Transform>(Transform);

		public static ref Transform RefTransform(this IEntity obj) => ref obj.GetValueUnsafe<Transform>(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTransform(this IEntity obj, out Transform value) => obj.TryGetValueUnsafe(Transform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTransform(this IEntity obj, Transform value) => obj.AddValue(Transform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTransform(this IEntity obj) => obj.HasValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTransform(this IEntity obj) => obj.DelValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTransform(this IEntity obj, Transform value) => obj.SetValue(Transform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IRequest<Vector3> GetMoveRequest(this IEntity obj) => obj.GetValueUnsafe<IRequest<Vector3>>(MoveRequest);

		public static ref IRequest<Vector3> RefMoveRequest(this IEntity obj) => ref obj.GetValueUnsafe<IRequest<Vector3>>(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveRequest(this IEntity obj, out IRequest<Vector3> value) => obj.TryGetValueUnsafe(MoveRequest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveRequest(this IEntity obj, IRequest<Vector3> value) => obj.AddValue(MoveRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveRequest(this IEntity obj) => obj.HasValue(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveRequest(this IEntity obj) => obj.DelValue(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveRequest(this IEntity obj, IRequest<Vector3> value) => obj.SetValue(MoveRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<Vector3> GetMoveEvent(this IEntity obj) => obj.GetValueUnsafe<IEvent<Vector3>>(MoveEvent);

		public static ref IEvent<Vector3> RefMoveEvent(this IEntity obj) => ref obj.GetValueUnsafe<IEvent<Vector3>>(MoveEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveEvent(this IEntity obj, out IEvent<Vector3> value) => obj.TryGetValueUnsafe(MoveEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveEvent(this IEntity obj, IEvent<Vector3> value) => obj.AddValue(MoveEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveEvent(this IEntity obj) => obj.HasValue(MoveEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveEvent(this IEntity obj) => obj.DelValue(MoveEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveEvent(this IEntity obj, IEvent<Vector3> value) => obj.SetValue(MoveEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMoveSpeed(this IEntity obj) => obj.GetValueUnsafe<IValue<float>>(MoveSpeed);

		public static ref IValue<float> RefMoveSpeed(this IEntity obj) => ref obj.GetValueUnsafe<IValue<float>>(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveSpeed(this IEntity obj, out IValue<float> value) => obj.TryGetValueUnsafe(MoveSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveSpeed(this IEntity obj, IValue<float> value) => obj.AddValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveSpeed(this IEntity obj) => obj.HasValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveSpeed(this IEntity obj) => obj.DelValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveSpeed(this IEntity obj, IValue<float> value) => obj.SetValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IRequest<Vector3> GetLookRequest(this IEntity obj) => obj.GetValueUnsafe<IRequest<Vector3>>(LookRequest);

		public static ref IRequest<Vector3> RefLookRequest(this IEntity obj) => ref obj.GetValueUnsafe<IRequest<Vector3>>(LookRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetLookRequest(this IEntity obj, out IRequest<Vector3> value) => obj.TryGetValueUnsafe(LookRequest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddLookRequest(this IEntity obj, IRequest<Vector3> value) => obj.AddValue(LookRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasLookRequest(this IEntity obj) => obj.HasValue(LookRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelLookRequest(this IEntity obj) => obj.DelValue(LookRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLookRequest(this IEntity obj, IRequest<Vector3> value) => obj.SetValue(LookRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotateSpeed(this IEntity obj) => obj.GetValueUnsafe<IValue<float>>(RotateSpeed);

		public static ref IValue<float> RefRotateSpeed(this IEntity obj) => ref obj.GetValueUnsafe<IValue<float>>(RotateSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotateSpeed(this IEntity obj, out IValue<float> value) => obj.TryGetValueUnsafe(RotateSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotateSpeed(this IEntity obj, IValue<float> value) => obj.AddValue(RotateSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotateSpeed(this IEntity obj) => obj.HasValue(RotateSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotateSpeed(this IEntity obj) => obj.DelValue(RotateSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotateSpeed(this IEntity obj, IValue<float> value) => obj.SetValue(RotateSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Health GetHealth(this IEntity obj) => obj.GetValueUnsafe<Health>(Health);

		public static ref Health RefHealth(this IEntity obj) => ref obj.GetValueUnsafe<Health>(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHealth(this IEntity obj, out Health value) => obj.TryGetValueUnsafe(Health, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddHealth(this IEntity obj, Health value) => obj.AddValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHealth(this IEntity obj) => obj.HasValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHealth(this IEntity obj) => obj.DelValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHealth(this IEntity obj, Health value) => obj.SetValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetLifetime(this IEntity obj) => obj.GetValueUnsafe<Cooldown>(Lifetime);

		public static ref Cooldown RefLifetime(this IEntity obj) => ref obj.GetValueUnsafe<Cooldown>(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetLifetime(this IEntity obj, out Cooldown value) => obj.TryGetValueUnsafe(Lifetime, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddLifetime(this IEntity obj, Cooldown value) => obj.AddValue(Lifetime, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasLifetime(this IEntity obj) => obj.HasValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelLifetime(this IEntity obj) => obj.DelValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLifetime(this IEntity obj, Cooldown value) => obj.SetValue(Lifetime, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<DamageArgs> GetTakeDamageEvent(this IEntity obj) => obj.GetValueUnsafe<IEvent<DamageArgs>>(TakeDamageEvent);

		public static ref IEvent<DamageArgs> RefTakeDamageEvent(this IEntity obj) => ref obj.GetValueUnsafe<IEvent<DamageArgs>>(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTakeDamageEvent(this IEntity obj, out IEvent<DamageArgs> value) => obj.TryGetValueUnsafe(TakeDamageEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTakeDamageEvent(this IEntity obj, IEvent<DamageArgs> value) => obj.AddValue(TakeDamageEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTakeDamageEvent(this IEntity obj) => obj.HasValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTakeDamageEvent(this IEntity obj) => obj.DelValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTakeDamageEvent(this IEntity obj, IEvent<DamageArgs> value) => obj.SetValue(TakeDamageEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<DamageArgs> GetTakeDeathEvent(this IEntity obj) => obj.GetValueUnsafe<IEvent<DamageArgs>>(TakeDeathEvent);

		public static ref IEvent<DamageArgs> RefTakeDeathEvent(this IEntity obj) => ref obj.GetValueUnsafe<IEvent<DamageArgs>>(TakeDeathEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTakeDeathEvent(this IEntity obj, out IEvent<DamageArgs> value) => obj.TryGetValueUnsafe(TakeDeathEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTakeDeathEvent(this IEntity obj, IEvent<DamageArgs> value) => obj.AddValue(TakeDeathEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTakeDeathEvent(this IEntity obj) => obj.HasValue(TakeDeathEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTakeDeathEvent(this IEntity obj) => obj.DelValue(TakeDeathEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTakeDeathEvent(this IEntity obj, IEvent<DamageArgs> value) => obj.SetValue(TakeDeathEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IAction GetDestroyAction(this IEntity obj) => obj.GetValueUnsafe<IAction>(DestroyAction);

		public static ref IAction RefDestroyAction(this IEntity obj) => ref obj.GetValueUnsafe<IAction>(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDestroyAction(this IEntity obj, out IAction value) => obj.TryGetValueUnsafe(DestroyAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDestroyAction(this IEntity obj, IAction value) => obj.AddValue(DestroyAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDestroyAction(this IEntity obj) => obj.HasValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDestroyAction(this IEntity obj) => obj.DelValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDestroyAction(this IEntity obj, IAction value) => obj.SetValue(DestroyAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetTeam(this IEntity obj) => obj.GetValueUnsafe<IReactiveVariable<TeamType>>(Team);

		public static ref IReactiveVariable<TeamType> RefTeam(this IEntity obj) => ref obj.GetValueUnsafe<IReactiveVariable<TeamType>>(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeam(this IEntity obj, out IReactiveVariable<TeamType> value) => obj.TryGetValueUnsafe(Team, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeam(this IEntity obj, IReactiveVariable<TeamType> value) => obj.AddValue(Team, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeam(this IEntity obj) => obj.HasValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeam(this IEntity obj) => obj.DelValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeam(this IEntity obj, IReactiveVariable<TeamType> value) => obj.SetValue(Team, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEntity GetWeapon(this IEntity obj) => obj.GetValueUnsafe<IEntity>(Weapon);

		public static ref IEntity RefWeapon(this IEntity obj) => ref obj.GetValueUnsafe<IEntity>(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWeapon(this IEntity obj, out IEntity value) => obj.TryGetValueUnsafe(Weapon, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddWeapon(this IEntity obj, IEntity value) => obj.AddValue(Weapon, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWeapon(this IEntity obj) => obj.HasValue(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWeapon(this IEntity obj) => obj.DelValue(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWeapon(this IEntity obj, IEntity value) => obj.SetValue(Weapon, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetDamage(this IEntity obj) => obj.GetValueUnsafe<IValue<int>>(Damage);

		public static ref IValue<int> RefDamage(this IEntity obj) => ref obj.GetValueUnsafe<IValue<int>>(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDamage(this IEntity obj, out IValue<int> value) => obj.TryGetValueUnsafe(Damage, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDamage(this IEntity obj, IValue<int> value) => obj.AddValue(Damage, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamage(this IEntity obj) => obj.HasValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamage(this IEntity obj) => obj.DelValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDamage(this IEntity obj, IValue<int> value) => obj.SetValue(Damage, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<IEntity> GetTarget(this IEntity obj) => obj.GetValueUnsafe<IReactiveVariable<IEntity>>(Target);

		public static ref IReactiveVariable<IEntity> RefTarget(this IEntity obj) => ref obj.GetValueUnsafe<IReactiveVariable<IEntity>>(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTarget(this IEntity obj, out IReactiveVariable<IEntity> value) => obj.TryGetValueUnsafe(Target, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTarget(this IEntity obj, IReactiveVariable<IEntity> value) => obj.AddValue(Target, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTarget(this IEntity obj) => obj.HasValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTarget(this IEntity obj) => obj.DelValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTarget(this IEntity obj, IReactiveVariable<IEntity> value) => obj.SetValue(Target, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetFirePoint(this IEntity obj) => obj.GetValueUnsafe<Transform>(FirePoint);

		public static ref Transform RefFirePoint(this IEntity obj) => ref obj.GetValueUnsafe<Transform>(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFirePoint(this IEntity obj, out Transform value) => obj.TryGetValueUnsafe(FirePoint, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFirePoint(this IEntity obj, Transform value) => obj.AddValue(FirePoint, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFirePoint(this IEntity obj) => obj.HasValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFirePoint(this IEntity obj) => obj.DelValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFirePoint(this IEntity obj, Transform value) => obj.SetValue(FirePoint, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IRequest GetFireRequest(this IEntity obj) => obj.GetValueUnsafe<IRequest>(FireRequest);

		public static ref IRequest RefFireRequest(this IEntity obj) => ref obj.GetValueUnsafe<IRequest>(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireRequest(this IEntity obj, out IRequest value) => obj.TryGetValueUnsafe(FireRequest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireRequest(this IEntity obj, IRequest value) => obj.AddValue(FireRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireRequest(this IEntity obj) => obj.HasValue(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireRequest(this IEntity obj) => obj.DelValue(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireRequest(this IEntity obj, IRequest value) => obj.SetValue(FireRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IFunction<bool> GetFireCondition(this IEntity obj) => obj.GetValueUnsafe<IFunction<bool>>(FireCondition);

		public static ref IFunction<bool> RefFireCondition(this IEntity obj) => ref obj.GetValueUnsafe<IFunction<bool>>(FireCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireCondition(this IEntity obj, out IFunction<bool> value) => obj.TryGetValueUnsafe(FireCondition, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireCondition(this IEntity obj, IFunction<bool> value) => obj.AddValue(FireCondition, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireCondition(this IEntity obj) => obj.HasValue(FireCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireCondition(this IEntity obj) => obj.DelValue(FireCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireCondition(this IEntity obj, IFunction<bool> value) => obj.SetValue(FireCondition, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetFireEvent(this IEntity obj) => obj.GetValueUnsafe<IEvent>(FireEvent);

		public static ref IEvent RefFireEvent(this IEntity obj) => ref obj.GetValueUnsafe<IEvent>(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireEvent(this IEntity obj, out IEvent value) => obj.TryGetValueUnsafe(FireEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireEvent(this IEntity obj, IEvent value) => obj.AddValue(FireEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireEvent(this IEntity obj) => obj.HasValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireEvent(this IEntity obj) => obj.DelValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireEvent(this IEntity obj, IEvent value) => obj.SetValue(FireEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TriggerEventReceiver GetTrigger(this IEntity obj) => obj.GetValueUnsafe<TriggerEventReceiver>(Trigger);

		public static ref TriggerEventReceiver RefTrigger(this IEntity obj) => ref obj.GetValueUnsafe<TriggerEventReceiver>(Trigger);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTrigger(this IEntity obj, out TriggerEventReceiver value) => obj.TryGetValueUnsafe(Trigger, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTrigger(this IEntity obj, TriggerEventReceiver value) => obj.AddValue(Trigger, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTrigger(this IEntity obj) => obj.HasValue(Trigger);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTrigger(this IEntity obj) => obj.DelValue(Trigger);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTrigger(this IEntity obj, TriggerEventReceiver value) => obj.SetValue(Trigger, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Renderer GetMeshRenderer(this IEntity obj) => obj.GetValueUnsafe<Renderer>(MeshRenderer);

		public static ref Renderer RefMeshRenderer(this IEntity obj) => ref obj.GetValueUnsafe<Renderer>(MeshRenderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMeshRenderer(this IEntity obj, out Renderer value) => obj.TryGetValueUnsafe(MeshRenderer, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMeshRenderer(this IEntity obj, Renderer value) => obj.AddValue(MeshRenderer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMeshRenderer(this IEntity obj) => obj.HasValue(MeshRenderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMeshRenderer(this IEntity obj) => obj.DelValue(MeshRenderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMeshRenderer(this IEntity obj, Renderer value) => obj.SetValue(MeshRenderer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Animator GetAnimator(this IEntity obj) => obj.GetValueUnsafe<Animator>(Animator);

		public static ref Animator RefAnimator(this IEntity obj) => ref obj.GetValueUnsafe<Animator>(Animator);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetAnimator(this IEntity obj, out Animator value) => obj.TryGetValueUnsafe(Animator, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddAnimator(this IEntity obj, Animator value) => obj.AddValue(Animator, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAnimator(this IEntity obj) => obj.HasValue(Animator);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAnimator(this IEntity obj) => obj.DelValue(Animator);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetAnimator(this IEntity obj, Animator value) => obj.SetValue(Animator, value);
    }
}
