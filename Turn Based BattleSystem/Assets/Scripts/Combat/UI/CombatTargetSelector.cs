using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Combat.UI {
	public class CombatTargetSelector : MonoBehaviour {

		bool AllowSelectingEnemies => SelectedAction.targets == CombatAction.ActionTargets.Enemies;
		bool AllowSelectingAllies => SelectedAction.targets == CombatAction.ActionTargets.Allies;



		public System.Action OnSelectionChanged { get; set; }
		public CombatAction SelectedAction { get; set; }

		public void SetSelectedAction(CombatAction action) {
			if (OnSelectionChanged != null && SelectedAction != action) { OnSelectionChanged(); }
			OnSelectionChanged = null;
			SelectedAction = action;
		}

		public static CombatTargetSelector instance;

		public float UpwardsAmount = 2.5f;

		SpriteRenderer spr;
		void Awake() {
			instance = this;
			spr = GetComponent<SpriteRenderer>();
		}

		void Update() {
			if (SelectedAction == null) {
				DisableSprite();
				return;
			}

			// Cancel the selection.
			if (Input.GetMouseButtonDown(1)) {
				SetSelectedAction(null);
				return;
			}

			// Get all colliders under the mouse
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var hits = Physics2D.OverlapPointAll(mousePos);

			CombatActor target = null;
			// Search for valid targets under the mouse
			foreach (var collider in hits) {

				// To be altered when we implement multiple target types (allies, self, etc)
				if (SelectedAction.targets == CombatAction.ActionTargets.Enemies && collider.tag != "Enemy") { continue; }
				else if (SelectedAction.targets == CombatAction.ActionTargets.Allies && collider.tag != "Hero") { continue; }

				target = collider.GetComponent<CombatActor>();

				// Enable sprite and Set Position
				EnableSprite();
				transform.position = target.transform.position + Vector3.up * UpwardsAmount;

				// If user left clicks, apply action and end turn.
				if (Input.GetMouseButtonDown(0)) {
					// To be later resolved with an CombatActionResolver (action to be injected under the animation).
					SelectedAction.ApplyAction(BattleManager.instance.CurrentActor, target);
					SetSelectedAction(null);
					BattleManager.instance.CurrentActor.OnTurnEnd();
					break;
				}

			}

			if (target == null) { DisableSprite(); }

		}

		void DisableSprite() { spr.enabled = false; }
		void EnableSprite() { spr.enabled = true; }

	}
}