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
			skillUpgradeCurrent.Add(0);
		}
		
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
			DisplayActivations();
		}
	}


	private void DisplayCursor() {
		cursor.transform.position = buttonArr[currentIndex].transform.position;
	}


	private void ActivateSkill() {
		if (skillUpgradeCurrent[currentIndex] < skillUpgradeMax[currentIndex]) {
			skillUpgradeCurrent[currentIndex]++;
		} else {
			print("Already activated!");
		}
	}


	private void DisplayActivations() {
		if (skillUpgradeCurrent[currentIndex] == skillUpgradeMax[currentIndex]) {
			buttonArr[currentIndex].GetComponent<Image>().color = new Color32(255, 255, 225, 255);
			print("Skill learned!");
		}
	}

}
