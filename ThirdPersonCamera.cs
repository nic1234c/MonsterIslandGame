using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class ThirdPersonCamera : MonoBehaviour
	{
		
	   public static ThirdPersonCamera camera;
	   
	   [SerializeField] public Transform playerTransform;
	   [SerializeField] public Transform mainCameraTransform;
	   [SerializeField] public Transform cameraPivotTransform;
	 
	   private Transform currentTransform;
	   private Vector3 cameraTransformPosition;
	   private LayerMask collisionLayers;
	   private Vector3 cameraFollow;

	   private float defaultPosition;
	   private float lookRotation;
  
	   public float minPivot = -35;
	   public float maxPivot = 35;
	   private float pivotRotation;
	   
		private void Awake() {
			camera = this;
			cameraFollow = Vector3.zero;
			currentTransform = transform;
			defaultPosition = mainCameraTransform.localPosition.z;
			collisionLayers = ~(1 << 8 | 1 << 10);
		}
		
		public void FollowPlayer(float delta) 
		{
			Vector3 targetPosition = Vector3.SmoothDamp(currentTransform.position, playerTransform.position,ref cameraFollow, delta /  0.1f);
			currentTransform.position = targetPosition;
			CameraCollisions(delta);
		}
		
		public void CameraRotation(float delta, float mouseX, float mouseY)
		{
			lookRotation += (mouseX * 0.1f)/ delta;
			pivotRotation -= (mouseY * 0.03f) / delta;
			pivotRotation = Mathf.Clamp(pivotRotation, minPivot, maxPivot);
			
			Rotate(lookRotation,pivotRotation);	
		}

		
		private void CameraCollisions(float delta) {
			float targetPosition = GetDefaultPosition();
			Vector3 direction = mainCameraTransform.position - cameraPivotTransform.position;
			direction.Normalize();
			CheckCollision(delta,targetPosition,direction);	
		}
		
		private void Rotate(float lookRotation, float pivotRotation) {
			Vector3 rotation = Vector3.zero;
			rotation.y = lookRotation;
			Quaternion targetRotation = Quaternion.Euler(rotation);
			currentTransform.rotation = targetRotation;
			
			rotation = Vector3.zero;
			rotation.x = pivotRotation;
			SetTargetRotation(rotation,targetRotation);
		}
		
		private void SetTargetRotation(Vector3 rotation, Quaternion targetRotation) {
			targetRotation = Quaternion.Euler(rotation);
			cameraPivotTransform.localRotation = targetRotation;	
		}
		
		private void SetPosition(float targetPosition, float delta) {
			
			if(Mathf.Abs(targetPosition) < 0.2f)
			{
				targetPosition = -0.2f;
			}
			
			cameraTransformPosition.z = Mathf.Lerp(mainCameraTransform.localPosition.z,targetPosition, delta/0.2f);
			mainCameraTransform.localPosition = cameraTransformPosition;
		}
		
		private void CheckCollision(float delta, float targetPosition, Vector3 direction) {
			RaycastHit hit;
			if(Physics.SphereCast(cameraPivotTransform.position, 0.2f, direction, out hit, Mathf.Abs(targetPosition), collisionLayers)) {
				float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
				targetPosition = -(dis - 0.2f);
			}
			SetPosition(targetPosition,delta);
		}
		
		private float GetDefaultPosition() {
			return defaultPosition;
		}
	}
