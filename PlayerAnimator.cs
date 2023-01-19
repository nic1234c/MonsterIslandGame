using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public class PlayerAnimator : MonoBehaviour
	{	
		PlayerManager playerManager;
		public Animator animator;
		InputHandler inputHandler;
		PlayerMove playerMove;
		int vertical;
		int horizontal;
		public bool canRotate;
		
		public void Initialize() {
			animator = GetComponent<Animator>();
			vertical = Animator.StringToHash("Vertical");
			horizontal = Animator.StringToHash("Horizontal");	
			playerManager = GetComponentInParent<PlayerManager>();
			
			inputHandler = GetComponentInParent<InputHandler>();
			playerMove = GetComponentInParent<PlayerMove>();
		}
		
		public void UpdateMovementValues(float verticalMovement, float horizontalMovement)
		{
			SetVerticalPlayerValues(verticalMovement);
			SetHorizontalPlayerValues(horizontalMovement);
		}
		
		public void PlayAnimation(string targetAnim, bool isInteracting)
		{
			animator.applyRootMotion = isInteracting;
			animator.SetBool("isInteracting", isInteracting);
			animator.CrossFade(targetAnim, 0.2f);
		}
		
		public void SetVar(string targetAnim, bool val)
		{
			animator.SetBool(targetAnim, val);
		}
		
		public bool GetVar(string targetAnim)
		{
			return animator.GetBool(targetAnim);
		}
		
		private void OnAnimatorMove() {
			if(playerManager.isInteracting == false)
				return;
			
			float delta = Time.deltaTime;
			playerMove.GetComponent<Rigidbody>().drag = 0;
			Vector3 deltaPosition = animator.deltaPosition;
			deltaPosition.y = 0;
			Vector3 velocity = deltaPosition / delta;
			playerMove.GetComponent<Rigidbody>().velocity = velocity;
		}
		
				
		private void SetVerticalPlayerValues(float verticalMovement) {
			float v = 0;
			float vValue = 0.55f;
			if(verticalMovement > 0 && verticalMovement < vValue)
			{
				v = 0.5f;
			}
			else if(verticalMovement > vValue)
			{
				v = 1;
			}	
			else if(verticalMovement < 0 && verticalMovement > -vValue)
			{
				v = -0.5f;
			}
			else if (verticalMovement < -vValue) 
			{
				v = -1;
			}
			else 
			{
				v = 0;
			}
			animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);

		}
		
		private void SetHorizontalPlayerValues(float horizontalMovement) {
			
			float h = 0;
			float hValue = 0.55f;

			if(horizontalMovement > 0 && horizontalMovement < hValue)
			{
				h = 0.5f;
			}
			else if(horizontalMovement > hValue)
			{
				h = 1;
			}	
			else if(horizontalMovement < 0 && horizontalMovement > -hValue)
			{
				h = -0.5f;
			}
			else if (horizontalMovement < -hValue) 
			{
				h = -1;
			}
			else 
			{
				h = 0;
			}
			
			animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
		}
	}
	

