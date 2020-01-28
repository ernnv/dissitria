using UnityEngine;

// Later use: write a manager to allow floating pop up queuing 
public class FloatingTextPopup : MonoBehaviour {

	public TMPro.TextMeshProUGUI DamageText;
	public float FloatingSpeed = 2;

	// Currently not using these. Later they can be useful for stacking up a variety of Popups.
	float totalLifeTime = 2;
	float currentLifeTime;
	System.Action OnDespawn;

	void Update() {
		currentLifeTime += Time.deltaTime;
		if (currentLifeTime >= totalLifeTime) {
			if (OnDespawn != null) { OnDespawn(); }
			Destroy(gameObject);
		}

		transform.position += FloatingSpeed * Vector3.up * Time.deltaTime;
	}



	#region Static functionality
	static FloatingTextPopup prefab;
	public static FloatingTextPopup SpawnNew(int Damage, Vector2 SpawnPosition) {

		string desiredText = Mathf.Abs(Damage).ToString();

		if (Damage > 0) { desiredText = "-" + desiredText; }
		else { desiredText = "+" + desiredText; }

		var newPopup = SpawnNew(desiredText, SpawnPosition);
		// change color here or w/e

		return newPopup;
	}

	public static FloatingTextPopup SpawnNew(string Text, Vector2 SpawnPosition) {
		if (prefab == null) { prefab = Resources.Load<FloatingTextPopup>("UI/Floating Text Popup"); }

		var newPopup = Instantiate(prefab, SpawnPosition, Quaternion.identity);
		newPopup.DamageText.text = Text;
		newPopup.transform.position = SpawnPosition;

		return newPopup;
	}
	#endregion


}
