﻿using UnityEngine;

namespace GodTouches
{
    /// <summary>
	/// GodTouch
    /// </summary>
	public static class GodTouch
    {
		/// <summary>
		/// Androidフラグ
		/// </summary>
		static bool IsAndroid = Application.platform == RuntimePlatform.Android;
		/// <summary>
		/// iOSフラグ
		/// </summary>
		static bool IsIOS = Application.platform == RuntimePlatform.IPhonePlayer;
		/// <summary>
		/// エディタフラグ
		/// </summary> 
		static readonly bool IsEditor = !IsAndroid && !IsIOS;

		/// <summary>
		/// デルタポジション判定用・前回のポジション
		/// </summary>
		static Vector3 prebPosition;
		 
        /// <summary>
		/// タッチ情報を取得(エディタとスマホを考慮)
        /// </summary>
        /// <returns>タッチ情報</returns>
		public static GodTouchPhase GetPhase()
        { 
			if (IsEditor)
            {
				if (Input.GetMouseButtonDown(0)) 
				{
					prebPosition = Input.mousePosition;
					return GodTouchPhase.Began;
				}
				else if (Input.GetMouseButton(0))     
				{
					return GodTouchPhase.Moved;
				}
				else if (Input.GetMouseButtonUp(0)) 
				{
					return GodTouchPhase.Ended;
				}
            }
            else 
            {
				if (Input.touchCount > 0) return (GodTouchPhase)((int)Input.GetTouch(0).phase);
            } 
			return GodTouchPhase.None;
        }

        /// <summary>
		/// タッチポジションを取得(エディタとスマホを考慮)
        /// </summary>
        /// <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
		public static Vector3 GetPosition()
        { 
			if (IsEditor)
			{
				if (GetPhase() != GodTouchPhase.None) return Input.mousePosition;
            }
            else
            {
				if (Input.touchCount > 0) return Input.GetTouch(0).position;
            }
            return Vector3.zero;
        }

		/// <summary>
		/// タッチデルタポジションを取得(エディタとスマホを考慮)
		/// </summary>
		/// <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
		public static Vector3 GetDeltaPosition()
		{ 
			if (IsEditor)
			{
				var phase = GetPhase();
				if(phase != GodTouchPhase.None)
				{
					var now = Input.mousePosition;
					var delta = now - prebPosition;
					prebPosition = now; 
					return delta;
				}
			} 
			else
			{
				if (Input.touchCount > 0) return Input.GetTouch(0).deltaPosition;
			}
			return Vector3.zero;
		}
    }

	/// <summary>
	/// タッチ情報。UnityEngine.TouchPhase に None の情報を追加拡張。
	/// </summary>
	public enum GodTouchPhase 
	{
		None       = -1,
		Began      = 0, 
		Moved      = 1, 
		Stationary = 2, 
		Ended      = 3, 
		Canceled   = 4 
	}
}