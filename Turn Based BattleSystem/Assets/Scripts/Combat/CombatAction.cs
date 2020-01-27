using System.Collections.Generic;
using Combat.Actions;
using UnityEngine;

namespace Combat {
	[CreateAssetMenu(fileName = "New Action", menuName = "Actions/Empty", order = 0)]
	public class CombatAction : ScriptableObject {
		public string Name;
		public ActionTargets targets;

		public enum ActionTargets { Auto, Allies, Enemies }
		public List<CombatActionSequencePiece> ActionSequence;

		public void ApplyAction(CombatActor source, CombatActor target) {
			foreach (var action in ActionSequence) {
				action.Apply(source, target);
			}
		}

	}
}

namespace Combat.Actions {
	public enum AdjustType { Increase, Decrease }

	public abstract class CombatActionSequencePiece {
		public abstract void Apply(CombatActor source, CombatActor target);
	}

	public class ApplyDebuff : CombatActionSequencePiece {
		public Stat StatDebuff;
		public AdjustType AdjustingType;
		public int Amount;

		public override void Apply(CombatActor source, CombatActor target) {
			Debug.Log("Debuffs not implemented");
		}

	}

	public class ApplyDamage : CombatActionSequencePiece {
		public int Damage;

		public override void Apply(CombatActor source, CombatActor target) {
			target.TakeDamage(Damage);
		}
	}

}