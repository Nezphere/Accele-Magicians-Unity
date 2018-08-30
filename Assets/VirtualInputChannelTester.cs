using UnityEngine;

public class VirtualInputChannelTester : MonoBehaviour {
	public Transform axisAnchor;

	VirtualInputChannel keyChannel, axisChannel;

	void OnEnable() {
		keyChannel = new VirtualInputChannel(KeyCode.D, KeyCode.A);
		axisChannel = new VirtualInputChannel { type = VirtualInputChannelType.Axis, name = "JOY_A_9" };
//		axisChannel.isAxisToAxisRemapped = true;
//		axisChannel.axisToAxisRemapType = Uif.EasingType.Cubic;
//		axisChannel.axisToAxisRemapPhase = Uif.EasingPhase.In;
	}

	void Update() {
		float keyInput = keyChannel.GetAxis();
		if (keyInput != 0) {
			Debug.Log(keyInput);
		}

		float axisInput = axisChannel.GetAxis();
		if (axisInput != 0) {
			Debug.Log("axis " + axisInput);
		}
		axisAnchor.position = new Vector3(axisInput, 1, -1);
	}
}
