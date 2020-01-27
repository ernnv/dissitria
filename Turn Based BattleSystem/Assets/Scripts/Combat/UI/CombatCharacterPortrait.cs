using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.UI {
	public class CombatCharacterPortrait : MonoBehaviour {
		public Text NameText;

		public Text CurrentHPText;
		public Text MaxHPText;

		[Space]

		public Image CharacterImage;
		public Image HPBar;

		public Gradient ColorPicker;

		public CombatActor Actor { get; set; }


		public void Initialize(CombatActor actor) {

			Actor = actor;

			NameText.text = actor.Name;
			MaxHPText.text = actor.MaxHP.ToString();
			CurrentHPText.text = actor.CurrentHP.ToString();

			UpdateHPText();
		}

		public void UpdateHPText() {
			float HPPercentage = Actor.CurrentHP / Actor.MaxHP;

			// Make sure the percentage is between 0 and 1
			HPBar.fillAmount = Mathf.Clamp(HPPercentage, 0, 1);
			HPBar.color = ColorPicker.Evaluate(HPBar.fillAmount);

			// Make the amount is between 0 and MaxHP
			CurrentHPText.text = Mathf.CeilToInt(Mathf.Clamp(Actor.CurrentHP, 0, Actor.MaxHP)).ToString();
		}

	}
}