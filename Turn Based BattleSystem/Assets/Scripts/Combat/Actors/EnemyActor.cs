using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class EnemyActor : CombatActor {
		public override bool IsControlledByPlayer { get; protected set; } = false;
		public override bool isValidTarget { get; protected set; } = true;

		// We can later add logic to specify how an enemy finds his target.
		public CombatActor GetTarget() {
			var playerCharacters = FindObjectsOfType<PlayerActor>();
			var randomIndex = Random.Range(0, playerCharacters.Length);
			return playerCharacters[randomIndex];
		}

		// Temporary action logic for enemies
		public void PerformRandomAction(CombatActor target) {
			StartCoroutine(DoTemporaryActionLogic(target));
		}

		IEnumerator DoTemporaryActionLogic(CombatActor target) {
			// Perform the logic as normally
			yield return new WaitForSeconds(0.25f);

			var randomActionIndex = Random.Range(0, actions.Count);
			var randomAction = actions[randomActionIndex];

			randomAction.ApplyAction(this, target);

			yield return new WaitForSeconds(0.5f);

			// End turn once everything is done.
			OnTurnEnd();
		}
	}

}