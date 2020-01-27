using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.UI {
	public class CombatHealthUpdater : MonoBehaviour {
		public CombatCharacterPortrait FriendlyCharacterPortraitPrefab;
		public CombatCharacterPortrait EnemyCharacterPortraitPrefab;

		[Space]

		public RectTransform Friendlies;
		public RectTransform Enemies;

		List<CombatCharacterPortrait> portraits = new List<CombatCharacterPortrait>();

		public void Initialize(List<CombatActor> actors) {
			Clear();

			foreach (var actor in actors) {
				CombatCharacterPortrait actorPortraitPrefab = null;

				if (actor.IsControlledByPlayer) { actorPortraitPrefab = Instantiate(FriendlyCharacterPortraitPrefab, Friendlies); }
				else { actorPortraitPrefab = Instantiate(EnemyCharacterPortraitPrefab, Enemies); }

				actorPortraitPrefab.Initialize(actor);
				portraits.Add(actorPortraitPrefab);
			}
		}

		void Clear() {
			foreach (var p in portraits) { Destroy(p.gameObject); }
			portraits.Clear();
		}

		public void UpdateHealth(CombatActor actor) {
			var targetPortrait = portraits.Find(x => x.Actor == actor);
			targetPortrait.UpdateHPText();
		}
	}
}