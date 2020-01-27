using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Combat {

	public abstract class CombatActor : MonoBehaviour {

		// These are properties because they're mean to be overriden by other classes.
		public abstract bool IsControlledByPlayer { get; protected set; }
		public abstract bool isValidTarget { get; protected set; }

		// These are properties so they won't be displayed on the inspector.. 
		// Their only use are from BattleManager
		public List<Buff> statusList { get; set; } = new List<Buff>();

		/* FOR LATER USE:
				
				public Dictionary<Stat, int> CurrentRPGStats { get; set; }
				public Dictionary<Attributes, int> CurrentCombatStats { get; set; }

				// Editable Data:
				// They are not public to avoid setting them accidentally.
				[OdinSerialize, ShowInInspector] protected Dictionary<Stat, int> RPGStats = new Dictionary<Stat, int>();
				[OdinSerialize, ShowInInspector] protected Dictionary<Attributes, int> CombatStats = new Dictionary<Attributes, int>();


				public void InitializeStats() {
					CurrentRPGStats = new Dictionary<Stat, int>();
					CurrentCombatStats = new Dictionary<Attributes, int>();

					foreach (var rpgStat in RPGStats) { CurrentRPGStats.Add(rpgStat.Key, rpgStat.Value); }
					foreach (var combatStat in CombatStats) { CurrentCombatStats.Add(combatStat.Key, combatStat.Value); }
				}
		*/
		public List<CombatAction> actions = new List<CombatAction>();

		// Actions to allow easy communication from other scripts, and possible set up for effects.
		public System.Action OnTurnBegin { get; set; }
		public System.Action OnTurnEnd { get; set; }
		public System.Action OnDamaged { get; set; }
		public System.Action OnDeath { get; set; }

		public string Name;

		public float CurrentHP;
		public float MaxHP;
		public float Speed;

		public virtual void TakeDamage(int Damage) {
			CurrentHP -= Damage;
			OnDamaged();
			// We will call OnDeath once the hp animation is complete.
		}
	}

}