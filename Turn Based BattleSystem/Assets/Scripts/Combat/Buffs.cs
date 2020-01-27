using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : Add buffs
namespace Combat {

	public abstract class Buff {
		public abstract bool IsPositive { get; }
	}
	public abstract class Debuff : Buff {
		public override bool IsPositive => false;
	}
	public class Confused : Debuff { }
	public class Dead : Debuff { }

}