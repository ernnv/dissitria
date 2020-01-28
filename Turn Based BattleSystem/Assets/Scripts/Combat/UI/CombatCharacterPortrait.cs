using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.UI {
	public class CombatCharacterPortrait : MonoBehaviour {
		public Text NameText;

		public Text HPText;

		[Space]

		public Image CharacterImage;
		public Image HPBar;
		public Gradient ColorPicker;

		[Space]

		public Slider TurnProgressBar;
		public Image SliderHandleImage;

		public CombatActor Actor { get; set; }


		public void Initialize(CombatActor actor) {
			Actor = actor;
			NameText.text = actor.Name;

			Debug.Log("Initialization to be changed to use data from Scriptable Objects");
			CharacterImage.sprite = actor.GetComponentInChildren<SpriteRenderer>().sprite;
			SliderHandleImage.sprite = actor.GetComponentInChildren<SpriteRenderer>().sprite;

			TurnProgressBar.SetValueWithoutNotify(0);
			TurnProgressBar.interactable = false;
			UpdateHPText();
		}

		public void UpdateHPText() {
			float HPPercentage = Actor.CurrentHP / Actor.MaxHP;

			// Make sure the percentage is between 0 and 1
			HPBar.fillAmount = Mathf.Clamp(HPPercentage, 0, 1);
			HPBar.color = ColorPicker.Evaluate(HPBar.fillAmount);

			// Make the amount is between 0 and MaxHP
			HPText.text = Mathf.CeilToInt(Actor.CurrentHP).ToString() + " / " + Actor.MaxHP.ToString();
		}

		public void UpdateProgressBar(float amount) {
			TurnProgressBar.value = amount;
		}

	}
}