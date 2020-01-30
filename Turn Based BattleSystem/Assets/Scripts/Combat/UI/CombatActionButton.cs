using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.UI {
	public class CombatActionButton : MonoBehaviour {
		public Button button;
		public Text text;

		static CombatActionButton CurrentlySelectedButton;

		public void Disable() {
			button.interactable = false;
		}
		public void Enable() {
			button.interactable = true;
		}

		public void SetData(CombatAction action) {
			text.text = action.Name;
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => SelectThis(action));
		}

		void SelectThis(CombatAction action) {

			if (CurrentlySelectedButton != null && CurrentlySelectedButton != this) { CurrentlySelectedButton.DeselectThis(); }
			CurrentlySelectedButton = this;

			button.image.color = new Color(0.8f, 1, 0.8f);
			CombatTargetSelector.instance.SetSelectedAction(action);
			CombatTargetSelector.instance.OnSelectionChanged += () => DeselectThis();
		}

		void DeselectThis() { button.image.color = Color.white; }
	}
}
