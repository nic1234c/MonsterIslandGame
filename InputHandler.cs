using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class InputHandler : MonoBehaviour
	{
		public float horizontal;
		public float vertical;
		public float moveAmount;
		public float mouseX;
		public float mouseY;
		
		PlayerControls inputActions;
		PlayerAttackAction playerAttackAction;
		PlayerHealth playerHealth;
		Inventory playerInventory;
		UIHandler uiHandler;
		Vector2 movementInput;
		Vector2 cameraInput;
		
		public bool b_Input;
		public bool e_Input;
		public bool r_Input;
		public bool f_Input;
		public bool q_Input;
		public bool one_Input;
		public bool two_Input;
		public bool three_Input;
		public bool enter_Input;
		
		public bool dodgeFlag;

		private void Awake(){
			playerAttackAction = GetComponent<PlayerAttackAction>();
			playerHealth = GetComponent<PlayerHealth>();
			playerInventory = GetComponentInParent<Inventory>();
			uiHandler = GetComponentInParent<UIHandler>();
		}
		
		private void LateUpdate() {
			dodgeFlag = false;
			e_Input = false;
			r_Input = false;
			f_Input = false;
			q_Input = false;
			one_Input = false;
			two_Input = false;
			three_Input = false;
			enter_Input = false;
		}
		public void OnEnable()
		{
			if(inputActions == null)
			{
				inputActions = new PlayerControls();
				inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
				inputActions.PlayerMovement.Camera.performed += c => cameraInput = c.ReadValue<Vector2>();
			}
			
			inputActions.Enable();
		}
		
		private void OnDisable(){
			inputActions.Disable();
		}
		
		public void Check(float delta) {
			MoveInput(delta);
			DodgeInput();
			MeleeAttackInput();
			HealInventoryInput();
			AttackSpellInput();
			ToggleInventoryMenuInput();
			ChangeAttackSpellInput();
			ChangeHealSpellInput();
			ChangeWeaponInput();
			ConfirmName();
		}
		
		private void MoveInput(float delta)
		{
			horizontal = movementInput.x;
			vertical = movementInput.y;
			moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
			mouseX = cameraInput.x;
			mouseY = cameraInput.y;
		}
		
		private void DodgeInput()
		{
			b_Input = inputActions.PlayerActions.Dodge.triggered;
			
			if(b_Input){
				dodgeFlag = true;
			}
		}
		
		private void MeleeAttackInput(){
			inputActions.PlayerActions.AttackOne.performed += i => e_Input = true;
			if(e_Input)
			{
				playerAttackAction.HandleLightAttack(playerInventory.weapon);
			}
		}

		private void HealInventoryInput() {
			inputActions.PlayerActions.Heal.performed += i => r_Input = true;

			if(r_Input)
			{
				playerHealth.HealAction(playerInventory.healSpell);
			} 
		}
		
		private void AttackSpellInput() {
			inputActions.PlayerActions.Spell.performed += i => f_Input = true;
		
			if(f_Input) {
				playerAttackAction.HandleSpellAttack(playerInventory.attackSpell);
			}
		}
		
		private void ToggleInventoryMenuInput() {
			inputActions.UIActions.Toggle.performed += i => q_Input = true;
		
			if(q_Input && !playerInventory.isOpen) {
				playerInventory.ShowInventoryMenu();
			} else if(q_Input && playerInventory.isOpen) {
				playerInventory.HideInventoryMenu();
			}
		}
		
		private void ChangeAttackSpellInput() {
			inputActions.PlayerActions.ChangeAttackSpell.performed += i => one_Input = true;
			
			if(one_Input) {
				playerInventory.ChangeAttackSpell();
			}
		}
		
		private void ChangeHealSpellInput() {
			inputActions.PlayerActions.ChangeHealSpell.performed += i => two_Input = true;
			
			if(two_Input) {
				playerInventory.ChangeHealSpell();
			}
		}
		
		private void ChangeWeaponInput() {
			inputActions.PlayerActions.ChangeWeapon.performed += i => three_Input = true;
			
			if(three_Input) {
				playerAttackAction.WeaponChange();
				playerInventory.ChangeWeapon();
			}
		}
		
		
		private void ConfirmName() {
			inputActions.UIActions.Confirm.performed += i => enter_Input = true;
			
			if(enter_Input) {
				uiHandler.SetHighScore();
			}
		}
		
		
	}

