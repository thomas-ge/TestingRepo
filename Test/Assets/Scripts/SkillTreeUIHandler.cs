using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class SkillTreeUIHandler : MonoBehaviour {

	public GameObject buttonParent;
	public Image cursor;

	public List<Image> buttonArr;
	public List<int> skillUpgradeCurrent;
	private List<int> skillUpgradeMax = new List<int>(new int[] {
		5, 5, 5, 1, 1, 1, 5, 1,
		5, 5, 1,
		1, 1, 1, 1, 1, 1, 1, 1,
		5, 4, 4, 5, 1, 5, 3, 4,
		1, 1, 1, 4, 1, 1
	});

	private int currentIndex = 15;


	private void Awake() {
		// Save all skill buttons in an array
		for (int i = 0; i < buttonParent.transform.childCount; i++) {
			buttonArr.Add(buttonParent.transform.GetChild(i).GetComponent<Image>());
			skillUpgradeCurrent.Add(-1);
		}

		// Initially enable skills that can be learned
		for (int i = 0; i < 8; i++) {
			skillUpgradeCurrent[i] = 0;
		}
		skillUpgradeCurrent[9] = 0;
		for (int j = 11; j < 19; j++) {
			skillUpgradeCurrent[j] = 0;
		}
		skillUpgradeCurrent[23] = 0;
		
		// This "skill" is already activated – because it's not really a skill but the class the player chose
		skillUpgradeCurrent[currentIndex] = 1;

		DisplayActivations();
	}


	private void Update() {
		GetInput();

		DisplayCursor();
	}


	private void GetInput() {
		if (ReInput.players.GetPlayer(0).GetButtonDown("Up")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navUp.GetComponent<ButtonNav>().indexID;
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("Down")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navDown.GetComponent<ButtonNav>().indexID;
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("Left")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navLeft.GetComponent<ButtonNav>().indexID;
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("Right")) {
			currentIndex = buttonArr[currentIndex].GetComponent<ButtonNav>().navRight.GetComponent<ButtonNav>().indexID;
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("X")) {
			ActivateSkill();
		}
	}


	private void DisplayCursor() {
		cursor.transform.position = buttonArr[currentIndex].transform.position;
	}


	private void ActivateSkill() {
		if (skillUpgradeCurrent[currentIndex] >= 0) {
			if (skillUpgradeCurrent[currentIndex] < skillUpgradeMax[currentIndex]) {
				skillUpgradeCurrent[currentIndex]++;

				ActivateNext();
				DisplayActivations();
			} else {
				print("Already activated!");
			}
		} else {
			print("This skill is locked");
		}
	}


	private void ActivateNext() {
		// Activate the next button in line if it's predecessor is enbled
		if (skillUpgradeCurrent[3] == skillUpgradeMax[3] || skillUpgradeCurrent[4] == skillUpgradeMax[4] || skillUpgradeCurrent[5] == skillUpgradeMax[5]) {
			if (skillUpgradeCurrent[8] < 0) {
				skillUpgradeCurrent[8] = 0;
			}
		}

		if (skillUpgradeCurrent[7] == skillUpgradeMax[7] && skillUpgradeCurrent[10] < 0) {
			skillUpgradeCurrent[10] = 0;
		}

		for (int i = 11; i < 14; i++) {
			if (skillUpgradeCurrent[i] == skillUpgradeMax[i] && skillUpgradeCurrent[i+8] < 0) {
				skillUpgradeCurrent[i+8] = 0;
			}
		}

		if (skillUpgradeCurrent[14] == skillUpgradeMax[14] && skillUpgradeCurrent[22] < 0) {
			skillUpgradeCurrent[22] = 0;
		}

		if (skillUpgradeCurrent[16] == skillUpgradeMax[16] && skillUpgradeCurrent[24] < 0) {
			skillUpgradeCurrent[24] = 0;
		}

		for (int j = 17; j < 19; j++) {
			if (skillUpgradeCurrent[j] == skillUpgradeMax[j] && skillUpgradeCurrent[j+8] < 0) {
				skillUpgradeCurrent[j+8] = 0;
			}
		}

		if (skillUpgradeCurrent[23] == skillUpgradeMax[23] && skillUpgradeCurrent[30] < 0) {
			skillUpgradeCurrent[30] = 0;
		}

		// Activate the next button in line if it's predecessor is complete
		for (int k = 19; k < 22; k++) {
			if (skillUpgradeCurrent[k] == skillUpgradeMax[k] && skillUpgradeCurrent[k+8] < 0) {
				skillUpgradeCurrent[k+8] = 0;
			}
		}

		for (int l = 25; l < 27; l++) {
			if (skillUpgradeCurrent[l] == skillUpgradeMax[l] && skillUpgradeCurrent[l+6] < 0) {
				skillUpgradeCurrent[l+6] = 0;
			}
		}
	}


	private void DisplayActivations() {
		// Show the current level of activation
		switch(currentIndex) {
			case 0:
			case 1:
			case 2:
			case 6:
			case 8:
			case 9:
			case 19:
			case 20:
			case 21:
			case 22:
			case 24:
			case 25:
			case 26:
			case 30:
				buttonArr[currentIndex].transform.GetChild(1).GetComponent<Text>().text = skillUpgradeCurrent[currentIndex] + "/" + skillUpgradeMax[currentIndex];
				break;
			
			// Lock the other skills when activating these
			case 3:
				skillUpgradeCurrent[4] = -2;
				skillUpgradeCurrent[5] = -2;
				break;

			case 4:
				skillUpgradeCurrent[3] = -2;
				skillUpgradeCurrent[5] = -2;
				break;

			case 5:
				skillUpgradeCurrent[3] = -2;
				skillUpgradeCurrent[4] = -2;
				break;

			case 14:
				skillUpgradeCurrent[16] = -2;
				skillUpgradeCurrent[24] = -2;
				break;

			case 16:
				skillUpgradeCurrent[14] = -2;
				skillUpgradeCurrent[22] = -2;
				break;
		}

		// Enable all skill buttons that have a 0 – aka make it non-transparent
		for (int k = 0; k < buttonArr.Count; k++) {
			if (skillUpgradeCurrent[k] == -2) {
				buttonArr[k].GetComponent<Image>().color = new Color32(255, 255, 225, 30);
			}
			
			if (skillUpgradeCurrent[k] == 0) {
				buttonArr[k].GetComponent<Image>().color = new Color32(255, 255, 225, 255);
			}

			if (skillUpgradeCurrent[k] == 1) {
				buttonArr[k].GetComponent<Image>().color = new Color32(255, 255, 225, 255);
			}
		}

		// Set the selected skill to "done"
		if (skillUpgradeCurrent[currentIndex] == skillUpgradeMax[currentIndex]) {
			buttonArr[currentIndex].transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
		}
	}

}
