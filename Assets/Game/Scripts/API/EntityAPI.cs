/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Entities;
using Atomic.Elements;
using FMOD.Studio;
using FMODUnity;

namespace Prototype
{
	public static class EntityAPI
	{
		///Tags
		public const int Player = -1615495341;


		///Values
		public const int Transform = -180157682; // Transform
		public const int Sound = 961539200; // EventReference
		public const int TriggerReceiver = 1006843418; // TriggerEventReceiver


		///Tag Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayerTag(this IEntity obj) => obj.HasTag(Player);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddPlayerTag(this IEntity obj) => obj.AddTag(Player);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayerTag(this IEntity obj) => obj.DelTag(Player);


		///Value Extensions

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
		public static EventReference GetSound(this IEntity obj) => obj.GetValueUnsafe<EventReference>(Sound);

		public static ref EventReference RefSound(this IEntity obj) => ref obj.GetValueUnsafe<EventReference>(Sound);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetSound(this IEntity obj, out EventReference value) => obj.TryGetValueUnsafe(Sound, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddSound(this IEntity obj, EventReference value) => obj.AddValue(Sound, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasSound(this IEntity obj) => obj.HasValue(Sound);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelSound(this IEntity obj) => obj.DelValue(Sound);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetSound(this IEntity obj, EventReference value) => obj.SetValue(Sound, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TriggerEventReceiver GetTriggerReceiver(this IEntity obj) => obj.GetValueUnsafe<TriggerEventReceiver>(TriggerReceiver);

		public static ref TriggerEventReceiver RefTriggerReceiver(this IEntity obj) => ref obj.GetValueUnsafe<TriggerEventReceiver>(TriggerReceiver);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTriggerReceiver(this IEntity obj, out TriggerEventReceiver value) => obj.TryGetValueUnsafe(TriggerReceiver, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTriggerReceiver(this IEntity obj, TriggerEventReceiver value) => obj.AddValue(TriggerReceiver, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTriggerReceiver(this IEntity obj) => obj.HasValue(TriggerReceiver);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTriggerReceiver(this IEntity obj) => obj.DelValue(TriggerReceiver);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTriggerReceiver(this IEntity obj, TriggerEventReceiver value) => obj.SetValue(TriggerReceiver, value);
    }
}
