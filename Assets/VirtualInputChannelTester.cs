using UnityEngine;

public class VirtualInputChannelTester : MonoBehaviour {
	VirtualInputChannel horizontalChannel;

	void OnEnable() {
		horizontalChannel = new VirtualInputChannel(KeyCode.D, KeyCode.A);
	}

	void Update() {
		float horizontalInput = horizontalChannel.GetAxis();
		if (horizontalInput != 0) {
			Debug.Log(horizontalInput);
		}
	}
}
