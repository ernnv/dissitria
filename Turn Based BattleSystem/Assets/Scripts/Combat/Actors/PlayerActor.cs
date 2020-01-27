using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class PlayerActor : CombatActor {
		public override bool IsControlledByPlayer { get; protected set; } = true;
		public override bool isValidTarget { get; protected set; } = true;
	}
}