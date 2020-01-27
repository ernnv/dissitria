using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Combat.UI;

namespace Combat {
	public class BattleManager : MonoBehaviour {

		#region Singleton
		public static BattleManager instance;
		void Awake() {
			if (instance) {
				DestroyImmediate(gameObject);
			}
			else {
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
		}
		#endregion

		public GameObject basePanel;
		public CombatActionPanelUI actionPanelUI;
		public CombatTargetSelector targetSelector;
		public CombatHealthUpdater combatHealthUpdater; 

		enum BattleState { NONE, WAIT_FOR_NEXT_TURN, WAIT_FOR_USER_ACTION }

		public CombatActor CurrentActor { get; set; }

		List<CombatActor> combatActors;
		BattleState currentBattleState;

		// Determines which actor will take a turn.
		List<ActionPointsResolver> turnResolvers;

		public void StartCombat() {
			// Find all Actors on the scene.
			combatActors = FindObjectsOfType<CombatActor>().ToList();

			turnResolvers = new List<ActionPointsResolver>();

			foreach (var actor in combatActors) {
				// Set up the actors and their communication with other battle systems.
				//actor.InitializeStats();

				actor.OnDamaged += () => combatHealthUpdater.UpdateHealth(actor);
				actor.OnDeath += () => actor.statusList.Add(new Dead());

				actor.OnTurnBegin += () => {
					actionPanelUI.Populate(actor.actions);

				};
				actor.OnTurnEnd += () => {
					actionPanelUI.Clear();
					currentBattleState = BattleState.WAIT_FOR_NEXT_TURN;
				};

				// Add a new action points resolver for each actor
				turnResolvers.Add(new ActionPointsResolver(actor));
			}

			basePanel.SetActive(true);
		}

		void Update() {
			if (currentBattleState == BattleState.NONE) { }
			else if (currentBattleState == BattleState.WAIT_FOR_NEXT_TURN) { WaitForNextTurn(); }
			else if (currentBattleState == BattleState.WAIT_FOR_USER_ACTION) { }
		}

		void WaitForNextTurn() {
			// probably resolve buffs too here.
			foreach (var t in turnResolvers) {
				if (!t.canPassTime) { continue; }
				t.PassTime();
				if (t.isReadyToTakeTurn) {
					SelectActor(t.Actor);
					return;
				}
			}
		}

		void SelectActor(CombatActor actor) {
			CurrentActor = actor;
			currentBattleState = BattleState.WAIT_FOR_USER_ACTION;
			CurrentActor.OnTurnBegin();
		}



		class ActionPointsResolver {
			const float ActionPointsRequiredToTakeTurn = 100;
			float currentActionPoints;

			public ActionPointsResolver(CombatActor actor) { Actor = actor; }

			public CombatActor Actor;

			//float actorSpeed => Actor.CurrentCombatStats[Attributes.Speed];
			float actorSpeed => Actor.Speed;

			public bool isReadyToTakeTurn => currentActionPoints >= ActionPointsRequiredToTakeTurn;

			// Make sure dead or immobilized actors won't take any turns.
			public bool canPassTime => !Actor.statusList.Any(x => x is Dead);
			public void PassTime() { currentActionPoints += actorSpeed * Time.deltaTime; }
		}
	}

	public enum Stat { Stamina, Intellect, Dexterity, Agility }
	public enum Attributes { HP, MP, Damage, Defense, Speed }

	// NOTES:
	//
	// 'protected' means you can only access this from within the class.
	// this is to avoid accidentally setting these values from outside the class
	//
	// `abstract` means that the class cannot be used on its own.
	// a class that inherits from it has to implement all the methods/properties marked as `abstract`
	// this is called polymorphism.

}