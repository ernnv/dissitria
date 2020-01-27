using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.UI {

	public class CombatActionPanelUI : MonoBehaviour {

		public CombatActionButton ActionButtonPrefab;
		public List<CombatActionButton> buttons;

		public void Populate(List<CombatAction> actions) {
			Clear();
			foreach (var ac in actions) {
				var newButton = Instantiate(ActionButtonPrefab, transform);
				newButton.SetData(ac);
				buttons.Add(newButton);
			}
			AddDisabledButtonWithText("Items");
			AddDisabledButtonWithText("Flee");
		}

		public void Clear() {
			foreach (var b in buttons) { Destroy(b.gameObject); }
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