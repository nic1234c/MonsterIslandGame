using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class PlayerManager : MonoBehaviour
	{
		InputHandler inputHandler;
		Animator animator;
		ThirdPersonCamera thirdPersonCamera;
		PlayerMove playerMovement;
		public bool isInteracting;	
	
		private void Awake()
		{
			thirdPersonCamera = ThirdPersonCamera.camera;
		}
		
		void Start()
		{	
			inputHandler = GetComponent<InputHandler>();
			animator = GetComponentInChildren<Animator>();
			playerMovement = GetComponent<PlayerMove>();
		} 
		
		void Update(){
			float delta = Time.deltaTime;
			UpdateInteracting();
			CheckInput(delta);
			SetMovement(delta);	
		}
		
		private void CheckInput(float delta) {
			inputHandler.Check(delta);
		}
		
		
		private void SetMovement(float delta) {
			playerMovement.PlayerMovement(delta);
			playerMovement.PlayerDodge(delta);
			playerMovement.PlayerInAirInteraction(delta,playerMovement.moveDir);	
		}
		
		private void SetCamera() {
			float delta = Time.fixedDeltaTime;	
			thirdPersonCamera.FollowPlayer(delta);
			thirdPersonCamera.CameraRotation(delta,inputHandler.mouseX, inputHandler.mouseY);
		}
		
		private void UpdateInteracting() {
			isInteracting = animator.GetBool("isInteracting");
		}
	
		private void LateUpdate()
		{
			SetCamera();
		}

	}

