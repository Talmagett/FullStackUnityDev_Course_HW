/**
* Code generation. Don't modify! 
**/

using Atomic.Contexts;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Contexts;
using Atomic.Entities;
using Atomic.Elements;
using System.Collections.Generic;

namespace SampleGame
{
	public static class GameContextAPI
	{


		///Values
		public const int BulletPool = 1915726678; // IEntityPool
		public const int WorldTransform = -486031409; // Transform
		public const int EntityPool = 1931115573; // GenericSceneEntityPool


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEntityPool GetBulletPool(this IContext obj) => obj.GetValue<IEntityPool>(BulletPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetBulletPool(this IContext obj, out IEntityPool value) => obj.TryGetValue(BulletPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddBulletPool(this IContext obj, IEntityPool value) => obj.AddValue(BulletPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasBulletPool(this IContext obj) => obj.HasValue(BulletPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelBulletPool(this IContext obj) => obj.DelValue(BulletPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetBulletPool(this IContext obj, IEntityPool value) => obj.SetValue(BulletPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetWorldTransform(this IContext obj) => obj.GetValue<Transform>(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWorldTransform(this IContext obj, out Transform value) => obj.TryGetValue(WorldTransform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddWorldTransform(this IContext obj, Transform value) => obj.AddValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWorldTransform(this IContext obj) => obj.HasValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWorldTransform(this IContext obj) => obj.DelValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWorldTransform(this IContext obj, Transform value) => obj.SetValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GenericSceneEntityPool GetEntityPool(this IContext obj) => obj.GetValue<GenericSceneEntityPool>(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEntityPool(this IContext obj, out GenericSceneEntityPool value) => obj.TryGetValue(EntityPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddEntityPool(this IContext obj, GenericSceneEntityPool value) => obj.AddValue(EntityPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEntityPool(this IContext obj) => obj.HasValue(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEntityPool(this IContext obj) => obj.DelValue(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEntityPool(this IContext obj, GenericSceneEntityPool value) => obj.SetValue(EntityPool, value);
    }
}
