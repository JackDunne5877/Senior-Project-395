using UnityEngine;
using Photon.Pun;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviourPun
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			if (photonView.IsMine)
			{
				MoveInput(value.Get<Vector2>());
			}
		}

		public void OnLook(InputValue value)
		{
			if (photonView.IsMine)
			{
				if (cursorInputForLook)
				{
					LookInput(value.Get<Vector2>());
				}
			}
		}

		public void OnJump(InputValue value)
		{
			if (photonView.IsMine)
			{
				JumpInput(value.isPressed);
			}
		}

		public void OnSprint(InputValue value)
		{
			if (photonView.IsMine)
			{
				SprintInput(value.isPressed);
			}
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			if (photonView.IsMine)
			{
				move = newMoveDirection;
			}
		}

		public void LookInput(Vector2 newLookDirection)
		{
			if (photonView.IsMine)
			{
				look = newLookDirection;
			}
		}

		public void JumpInput(bool newJumpState)
		{
			if (photonView.IsMine)
			{
				jump = newJumpState;
			}
		}

		public void SprintInput(bool newSprintState)
		{
			if (photonView.IsMine)
			{
				sprint = newSprintState;
			}
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			if (photonView.IsMine)
			{
				SetCursorState(cursorLocked);
			}
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}