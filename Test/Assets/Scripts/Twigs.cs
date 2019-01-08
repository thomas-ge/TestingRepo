using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Twigs : MonoBehaviour {

	public Slider twigsSlider;

	private bool isIncreasing = false;

	[Range(0, 100)]
	public float currentValue = 0;
	private float moveSpeed = 80.0f;


	private void Update() {
		if (isIncreasing) {
			IncreaseValue();
		} else {
			DecreaseValue();
		}

		twigsSlider.value = currentValue;
	}

	
	private void IncreaseValue() {
		currentValue += moveSpeed * Time.deltaTime;

		if (currentValue >= 100) {
			isIncreasing = false;
		}
	}

	
	private void DecreaseValue() {
		currentValue -= moveSpeed * Time.deltaTime;

		if (currentValue <= 0) {
			isIncreasing = true;
		}
	}
}
