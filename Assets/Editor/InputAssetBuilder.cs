using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InputAssetBuilder : MonoBehaviour {
	static SerializedProperty GetChildProperty(SerializedProperty parent, string name) {
		var child = parent.Copy();
		child.Next(true);
		do {
			if (child.name == name) return child;
		} while (child.Next(false));
		return null;
	}

	static bool AxisDefined(string axisName) {
		var serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
		var axesProperty = serializedObject.FindProperty("m_Axes");

		axesProperty.Next(true);
		axesProperty.Next(true);
		while (axesProperty.Next(false)) {
			var axis = axesProperty.Copy();
			axis.Next(true);
			if (axis.stringValue == axisName) return true;
		}
		return false;
	}

	class InputAxis {
		public string name = "";
		public string descriptiveName = "";
		public string descriptiveNegativeName = "";
		public string negativeButton = "";
		public string positiveButton = "";
		public string altNegativeButton = "";
		public string altPositiveButton = "";

		public float gravity;
		public float dead;
		public float sensitivity;

		public bool snap = false;
		public bool invert = false;

		public int type;

		public int axis;
		public int joyNum = 0;
	}

	static void AddAxis(InputAxis axis) {
		if (AxisDefined(axis.name)) return;

		var serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
		var axesProperty = serializedObject.FindProperty("m_Axes");

		axesProperty.arraySize++;
		serializedObject.ApplyModifiedProperties();

		var axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

		GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
		GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
		GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
		GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
		GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
		GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
		GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
		GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
		GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
		GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
		GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
		GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
		GetChildProperty(axisProperty, "type").intValue = axis.type;
		GetChildProperty(axisProperty, "axis").intValue = axis.axis;
		GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

		serializedObject.ApplyModifiedProperties();
	}

	static void ClearAxises() {
		var serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
		var axesProperty = serializedObject.FindProperty("m_Axes");
		axesProperty.ClearArray();
		serializedObject.ApplyModifiedProperties();
	}

	[MenuItem("K/Build Input Asset")]
	static void BuildInputAsset() {
		ClearAxises();

		for (int i = 1; i <= 30; i++) {
			AddAxis(new InputAxis {
				name = "JOY_A_" + i,
				dead = 0.001f,
				sensitivity = 1,
				type = 2,
				axis = i - 1
			});
		}

		for (int i = 1; i <= 20; i++) {
			AddAxis(new InputAxis {
				name = "JOY_B_" + i,
				positiveButton = "joystick button " + i,
				gravity = 1000,
				dead = 0.001f,
				sensitivity = 1000,
				type = 0
			});
		}
	}
}
