using Uif;
using UnInput = UnityEngine.Input;
using UnKeyCode = UnityEngine.KeyCode;

public enum VirtualInputChannelType {
	Axis,
	Button,
	Key
}

public sealed class VirtualInputChannel {
	public VirtualInputChannelType type = VirtualInputChannelType.Axis;
	public string name, negName = null;
	public UnKeyCode key, negKey = UnKeyCode.None;

	// VirtualChannelType.Axis
	public bool isAxisToAxisRemapped = false;
	public float axisToAxisRemapScale = 1;
	public EasingType axisToAxisRemapType = EasingType.Linear;
	public EasingPhase axisToAxisRemapPhase = EasingPhase.InOut;

	public float axisToButtonThreshold = 0.9f;

	// VirtualChannelType.Button || Key
	public float buttonToAxisValue = 1;

	public VirtualInputChannel() {}

	public VirtualInputChannel(UnKeyCode key, float buttonToAxisValue = 1) {
		type = VirtualInputChannelType.Key;
		this.key = key;
		this.buttonToAxisValue = buttonToAxisValue;
	}

	public VirtualInputChannel(UnKeyCode key, UnKeyCode negKey, float buttonToAxisValue = 1) {
		type = VirtualInputChannelType.Key;
		this.key = key;
		this.negKey = negKey;
		this.buttonToAxisValue = buttonToAxisValue;
	}

	public float GetAxis() {
		if (type == VirtualInputChannelType.Axis) {
			if (isAxisToAxisRemapped) {
				float value = UnInput.GetAxis(name);
				if (value < 0) {
					return -Easing.Ease(axisToAxisRemapType, axisToAxisRemapPhase, -value) * axisToAxisRemapScale;
				} else {
					return Easing.Ease(axisToAxisRemapType, axisToAxisRemapPhase, value) * axisToAxisRemapScale;
				}
			} else {
				return UnInput.GetAxis(name);
			}
		} else if (type == VirtualInputChannelType.Button) {
			if (negName != null) {
				bool pos = UnInput.GetButton(name), neg = UnInput.GetButton(negName);
				if (pos ^ neg) {
					return pos ? buttonToAxisValue : -buttonToAxisValue;
				} else {
					return 0;
				}
			} else {
				return UnInput.GetButton(name) ? buttonToAxisValue : 0;
			}
		} else {  // Key
			if (negKey != UnKeyCode.None) {
				bool pos = UnInput.GetKey(key), neg = UnInput.GetKey(negKey);
				if (pos ^ neg) {
					return pos ? buttonToAxisValue : -buttonToAxisValue;
				} else {
					return 0;
				}
			} else {
				return UnInput.GetKey(key) ? buttonToAxisValue : 0;
			}
		}
	}

	public bool GetButton() {
		if (type == VirtualInputChannelType.Axis) {
			return UnInput.GetAxisRaw(name) > axisToButtonThreshold;
		} else if (type == VirtualInputChannelType.Button) {
			return UnInput.GetButton(name);
		} else {
			return UnInput.GetKey(key);
		}
	}
}