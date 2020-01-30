using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmountTest : MonoBehaviour
{
	[Range(0, 1)] public float fill;

	void Update() {
		GetComponent<Image>().fillAmount = fill;
	}
}
