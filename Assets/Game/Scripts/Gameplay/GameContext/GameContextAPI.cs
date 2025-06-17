/**
* Code generation. Don't modify! 
**/

using Atomic.Contexts;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Entities;
using Atomic.Elements;
using System.Collections.Generic;

namespace Game.Gameplay
{
	public static class GameContextAPI
	{


		///Values
		public const int BulletPool = 1915726678; // IEntityPool
		public const int WorldTransform = -486031409; // Transform
		public const int InputMap = 43340267; // InputMap
		public const int TeamConfig = -783971258; // TeamConfig
		public const int Character = 294335127; // IEntity
		public const int Camera = 1018227507; // Camera
		public const int CameraOffset = -1286660539; // IValue<Vector3>


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEntityPool GetBulletPool(this IGameContext obj) => obj.GetValue<IEntityPool>(BulletPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetBulletPool(this IGameContext obj, out IEntityPool value) => obj.TryGetValue(BulletPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddBulletPool(this IGameContext obj, IEntityPool value) => obj.AddValue(BulletPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasBulletPool(this IGameContext obj) => obj.HasValue(BulletPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelBulletPool(this IGameContext obj) => obj.DelValue(BulletPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetBulletPool(this IGameContext obj, IEntityPool value) => obj.SetValue(BulletPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetWorldTransform(this IGameContext obj) => obj.GetValue<Transform>(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWorldTransform(this IGameContext obj, out Transform value) => obj.TryGetValue(WorldTransform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddWorldTransform(this IGameContext obj, Transform value) => obj.AddValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWorldTransform(this IGameContext obj) => obj.HasValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWorldTransform(this IGameContext obj) => obj.DelValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWorldTransform(this IGameContext obj, Transform value) => obj.SetValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static InputMap GetInputMap(this IGameContext obj) => obj.GetValue<InputMap>(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInputMap(this IGameContext obj, out InputMap value) => obj.TryGetValue(InputMap, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddInputMap(this IGameContext obj, InputMap value) => obj.AddValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInputMap(this IGameContext obj) => obj.HasValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInputMap(this IGameContext obj) => obj.DelValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInputMap(this IGameContext obj, InputMap value) => obj.SetValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TeamConfig GetTeamConfig(this IGameContext obj) => obj.GetValue<TeamConfig>(TeamConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamConfig(this IGameContext obj, out TeamConfig value) => obj.TryGetValue(TeamConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddTeamConfig(this IGameContext obj, TeamConfig value) => obj.AddValue(TeamConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamConfig(this IGameContext obj) => obj.HasValue(TeamConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamConfig(this IGameContext obj) => obj.DelValue(TeamConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamConfig(this IGameContext obj, TeamConfig value) => obj.SetValue(TeamConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEntity GetCharacter(this IGameContext obj) => obj.GetValue<IEntity>(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this IGameContext obj, out IEntity value) => obj.TryGetValue(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacter(this IGameContext obj, IEntity value) => obj.AddValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this IGameContext obj) => obj.HasValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this IGameContext obj) => obj.DelValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this IGameContext obj, IEntity value) => obj.SetValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Camera GetCamera(this IGameContext obj) => obj.GetValue<Camera>(Camera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCamera(this IGameContext obj, out Camera value) => obj.TryGetValue(Camera, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCamera(this IGameContext obj, Camera value) => obj.AddValue(Camera, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCamera(this IGameContext obj) => obj.HasValue(Camera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCamera(this IGameContext obj) => obj.DelValue(Camera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCamera(this IGameContext obj, Camera value) => obj.SetValue(Camera, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<Vector3> GetCameraOffset(this IGameContext obj) => obj.GetValue<IValue<Vector3>>(CameraOffset);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCameraOffset(this IGameContext obj, out IValue<Vector3> value) => obj.TryGetValue(CameraOffset, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCameraOffset(this IGameContext obj, IValue<Vector3> value) => obj.AddValue(CameraOffset, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCameraOffset(this IGameContext obj) => obj.HasValue(CameraOffset);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCameraOffset(this IGameContext obj) => obj.DelValue(CameraOffset);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCameraOffset(this IGameContext obj, IValue<Vector3> value) => obj.SetValue(CameraOffset, value);
    }
}
