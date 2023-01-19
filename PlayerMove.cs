using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
	{
		PlayerManager playerManager;
		Transform camera;
		InputHandler inputHandler;
		public Vector3 moveDir;
		public Transform playerTransform;
		public PlayerAnimator playerAnimator;
		public new Rigidbody rigidbody;
		
		AudioSource audioSource;
	    AudioClips audioClips;
		
		[SerializeField]
		float movementSpeed = 7;
		[SerializeField]
		float rotationSpeed = 11;
		[SerializeField]
		float fallingSpeed = 70;
				
		LayerMask layers;
		public float inAirTimer;
		
		public bool isInvincible;
		public bool isInAir;
		public bool isOnGround;
		public bool step;
		Vector3 moveVector;
		Vector3 targetPosition;
		
		private void Awake()
		{
			playerManager = GetComponent<PlayerManager>();
			rigidbody = GetComponent<Rigidbody>();
			inputHandler = GetComponent<InputHandler>();
			playerAnimator = GetComponentInChildren<PlayerAnimator>();
			audioSource = GetComponent<AudioSource>();
			audioClips = GetComponentInParent<AudioClips>();
		}
		
		private void Start()
		{
			camera = Camera.main.transform;
			playerTransform = transform;
			playerAnimator.Initialize();
			isOnGround = true;
			layers = ~(1 << 8 | 1 << 11);
		}
		
		private void LateUpdate() {
			if(isInAir)
				inAirTimer = inAirTimer + Time.deltaTime;
		
		}
		
		private void PlayerRotation(float delta)
		{
			Vector3 targetDir = Vector3.zero;
			
			targetDir = camera.forward * inputHandler.vertical;
			targetDir += camera.right * inputHandler.horizontal;
			
			targetDir.Normalize();
			targetDir.y = 0;
			
			if(targetDir == Vector3.zero)
				targetDir = playerTransform.forward;
			
			Rotate(targetDir,delta);
		}
		
		private void Rotate(Vector3 targetDir,float delta) {
			Quaternion lookRotation = Quaternion.LookRotation(targetDir);
			Quaternion targetRotation = Quaternion.Slerp(playerTransform.rotation, lookRotation, rotationSpeed * delta);
			playerTransform.rotation = targetRotation;
		}
		
		public void PlayerMovement(float delta){
			if(inputHandler.dodgeFlag)
				return;
			
			HaltAction();
			SetMoveDir();
			Move();
			
			playerAnimator.UpdateMovementValues(inputHandler.moveAmount, 0);

			if(inputHandler.moveAmount != 0) {		
				
				if (!audioSource.isPlaying) {
					audioSource.clip = audioClips.GetClip("Movement");
					audioSource.pitch = 1.5f;
					audioSource.volume = 0.05f;
					audioSource.Play (0);
				}
				else {
					audioSource.volume = 1f;
				}
			}

			PlayerRotation(delta);
			
		}
		
		public void Move() {
			moveDir *= movementSpeed;
			Vector3 movement = Vector3.ProjectOnPlane(moveDir, moveVector);
			rigidbody.velocity = movement;
		}
		
		private void SetMoveDir() {
			moveDir = camera.forward * inputHandler.vertical;
			moveDir += camera.right * inputHandler.horizontal;
			moveDir.Normalize();
			moveDir.y = 0;
		}

		public void PlayerDodge(float delta){
			HaltAction();	
			if(inputHandler.dodgeFlag)
			{
				moveDir = camera.forward * inputHandler.vertical;
				moveDir += camera.right * inputHandler.horizontal;
				
				if(inputHandler.moveAmount > 0)
				{
					playerAnimator.PlayAnimation("Dodge",true);
					audioSource.clip = audioClips.GetClip("Dodge");
					audioSource.Play();
					moveDir.y = 0;
					Quaternion dodgeRot = Quaternion.LookRotation(moveDir);
					playerTransform.rotation = dodgeRot;
					
				}
				isInvincible = true;
			} 
			else {
				isInvincible = false;
			}
			
		}
		
		public void PlayerInAirInteraction(float delta, Vector3 moveDir) {
			isOnGround = false;
			RaycastHit hit;
			Vector3 origin = playerTransform.position;
			origin.y += 0.5f;
			
			if(Physics.Raycast(origin, playerTransform.forward, out hit, 0.4f))
			{
				moveDir = Vector3.zero;
			}
					
			ApplyForceOnFall();
			Vector3 dir = moveDir;
			dir.Normalize();
			origin = origin + dir * 0.2f;
			
			targetPosition = playerTransform.position;
		
			if(Physics.Raycast(origin, -Vector3.up, out hit, 1f, layers))
			{
				PlayerReturn(hit);
			}
			else {
				PlayerFalling();
			}
		
			PlayerOnGround();
		}
			
		public void PlayerFalling() {
			isOnGround = !isOnGround;
			if(!isInAir)
			{
				if(!playerManager.isInteracting)
				{
					playerAnimator.PlayAnimation("Fall",true);
				}
				PlayerInAir();
			}
		}
		
		public void PlayerInAir() {
			Vector3 rigVelocity = rigidbody.velocity;
			rigVelocity.Normalize();
			rigidbody.velocity = rigVelocity * (movementSpeed / 2f);
			SetInAirTimer(true);
		}
		
		public void PlayerReturn(RaycastHit hit) {
			moveVector = hit.normal;
			Vector3 tarPoint = hit.point;
			isOnGround = true;
			targetPosition.y = tarPoint.y;
			
			if(isInAir)
			{
				if(inAirTimer > 0.3f)
				{
					playerAnimator.PlayAnimation("Land",true);
					
				}
				else {
					playerAnimator.PlayAnimation("Movement", false);
				}
				SetInAirTimer(false);
			}
		}		
		
		public void SetInAirTimer(bool timerBool) {
			if(timerBool) {
				isInAir = true;
			}
			else {
				inAirTimer = 0;
				isInAir = false;
			}
		}
			
		public void PlayerOnGround() {
			if(isOnGround)
			{
				if(playerManager.isInteracting || inputHandler.moveAmount > 0)
				{
					playerTransform.position = Vector3.Lerp(playerTransform.position,targetPosition, Time.deltaTime);
				}
				else {
					playerTransform.position = targetPosition;
				}
			}
		}
		
		public void ApplyForceOnFall() {
		
			if(isInAir)
			{
				rigidbody.AddForce(Vector3.down * fallingSpeed);
				rigidbody.AddForce(moveDir * fallingSpeed / 5f);
			}
		}
		
		private void HaltAction() {
			if(playerManager.isInteracting)
				return;
		}
	
}

