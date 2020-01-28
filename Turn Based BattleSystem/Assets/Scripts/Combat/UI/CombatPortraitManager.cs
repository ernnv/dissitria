using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.UI {
	public class CombatPortraitManager : MonoBehaviour {
		public CombatCharacterPortrait FriendlyCharacterPortraitPrefab;
		public CombatCharacterPortrait EnemyCharacterPortraitPrefab;

		[Space]

		public RectTransform Friendlies;
		public RectTransform Enemies;

		List<CombatCharacterPortrait> portraits = new List<CombatCharacterPortrait>();

		public void Initialize(List<CombatActor> actors, System.Action OnComplete) {
			// Clear all existing portraits
			Clear();

			// Temporary..
			foreach (var actor in actors) { actor.gameObject.SetActive(false); }

			// Start coroutine and tell it to execute the `OnComplete` action once it's done
			StartCoroutine(InitializePortraitsOneByOne(actors, OnComplete));
		}

		IEnumerator InitializePortraitsOneByOne(List<CombatActor> actors, System.Action OnComplete) {



			// Do what you want to do on the coroutine
			yield return new WaitForSeconds(0.25f);

			foreach (var actor in actors) {

				actor.gameObject.SetActive(true);

				CombatCharacterPortrait actorPortraitPrefab = null;

				if (actor.IsControlledByPlayer) { actorPortraitPrefab = Instantiate(FriendlyCharacterPortraitPrefab, Friendlies); }
				else { actorPortraitPrefab = Instantiate(EnemyCharacterPortraitPrefab, Enemies); }

				actorPortraitPrefab.Initialize(actor);
				portraits.Add(actorPortraitPrefab);
				yield return new WaitForSeconds(0.5f);
			}

			// Perform action after the coroutine runs
			OnComplete();
		}


		void Clear() {
			foreach (var p in portraits) { Destroy(p.gameObject); }
			portraits.Clear();
		}

		public void UpdateHealth(CombatActor actor) {
			var targetPortrait = portraits.Find(x => x.Actor == actor);
			targetPortrait.UpdateHPText();
		}

		public void UpdateProgressBar(CombatActor actor, float amount) {
			var targetPortrait = portraits.Find(x => x.Actor == actor);
			targetPortrait.UpdateProgressBar(amount);
		}
	}
}