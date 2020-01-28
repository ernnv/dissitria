using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.UI {

	public class CombatActionPanelUI : MonoBehaviour {

		public CombatActionButton ActionButtonPrefab;
		public List<CombatActionButton> buttons;

		void Awake() => Clear();

		public void PopulateActionPanel(CombatActor actor) {

			Clear();
			AddDisabledButtonWithText(actor.Name);

			foreach (var ac in actor.actions) {
				var newButton = Instantiate(ActionButtonPrefab, transform);
				newButton.SetData(ac);
				buttons.Add(newButton);

				// We don't want the enemy buttons to be interactable;
				if (!actor.IsControlledByPlayer) { newButton.button.interactable = false; }
			}

			AddDisabledButtonWithText("Items");
			AddDisabledButtonWithText("Flee");
		}

		public void Clear() {
			//foreach (var b in buttons) { Destroy(b.gameObject); } // for later, maybe.
			foreach (Transform child in transform) { Destroy(child.gameObject); }
			buttons.Clear();
		}


		// Temporary method.. we should move Flee and Items on their own actions later.
		void AddDisabledButtonWithText(string buttonText) {
			var FleeButton = Instantiate(ActionButtonPrefab, transform);
			FleeButton.Disable();
			FleeButton.text.text = buttonText;
		}
	}
}