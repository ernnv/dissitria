using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Combat.UI {
	public class CombatTargetSelector : MonoBehaviour {

		bool AllowSelectingEnemies => SelectedAction.targets == CombatAction.ActionTargets.Enemies;
		bool AllowSelectingAllies => SelectedAction.targets == CombatAction.ActionTargets.Allies;



		public System.Action OnChangeSelect { get; set; }
		public CombatAction SelectedAction { get; set; }

		public void SetSelectedAction(CombatAction action) {
			if (OnChangeSelect != null) { OnChangeSelect(); }
			OnChangeSelect = null;
			SelectedAction = action;
		}

		public static CombatTargetSelector instance;
		void Awake() { instance = this; }

		void Update() {
			if (SelectedAction = null) { return; }

			// Cancel the selection.
			if (Input.GetMouseButtonDown(1)) {
				SetSelectedAction(null);
				return;
			}

			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var hits = Physics2D.OverlapPointAll(mousePos);

			// Search for enemies under the mouse
			foreach (var collider in hits) {
				if (collider.tag != "Enemy") { continue; }
				var target = collider.GetComponent<CombatActor>();

				Debug.Log("Targeting " + target);

				// If user left clicks, apply action and end turn.
				if (Input.GetMouseButtonDown(0)) {
					// To be later resolved with an CombatTurnActionResolver
					SelectedAction.ApplyAction(BattleManager.instance.CurrentActor, target);
					SetSelectedAction(null);
					BattleManager.instance.CurrentActor.OnTurnEnd();
					return;
				}

			}
		}

	}
}