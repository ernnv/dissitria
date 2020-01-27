using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class EnemyActor : CombatActor {
		public override bool IsControlledByPlayer { get; protected set; } = false;
		public override bool isValidTarget { get; protected set; } = true;
	}

}