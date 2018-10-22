using UnityEngine;

public class TestDriver : MonoBehaviour {
	VirtualInputChannel trigger, grip, stickX, stickY, press;

	public float maxGripDistance, maxAcceleration, maxAngularSpeed, maxJumpDistance;

	public Vector3 direction;
	public float speed;

	Transform trans, camTrans;
	VrPlayer vrPlayer;

	void OnEnable() {
		trans = GetComponent<Transform>();
		camTrans = Camera.main.transform;
		vrPlayer = GetComponentInChildren<VrPlayer>();

		trigger = new VirtualInputChannel("JOY_A_9");
		grip = new VirtualInputChannel("JOY_B_4", 1);
		stickX = new VirtualInputChannel("JOY_A_1");
		stickY = new VirtualInputChannel("JOY_A_2");
		press = new VirtualInputChannel("JOY_B_8", 1);

		speed = 0;

		maxGripDistance = 1;
		maxAcceleration = 10;
		maxJumpDistance = 1;
		maxAngularSpeed = 3;
	}

	public bool gripInput, isGripped, isStopped, isPressed;
	public Vector3 gripPosition, gripBodyForward;

	void Update() {
		var camForward = camTrans.forward;
		var bodyForward = camForward;
		bodyForward.y = 0;
		bodyForward.Normalize();

		if (trigger.GetButton()) {
			direction = camForward;
			isStopped = false;
		}

		if (press.GetButton()) {
			if (!isPressed) {
				isPressed = true;
				isStopped = !isStopped;
			}
		} else {
			isPressed = false;
		}

		gripInput = grip.GetButton();
		if (gripInput) {
			if (!isGripped) {
				isGripped = true;
				gripPosition = vrPlayer.lHandTrans.localPosition;
				gripBodyForward = bodyForward;
				isStopped = false;
			} else {
				var gripDelta = vrPlayer.lHandTrans.localPosition - gripPosition;
				float gripDistance = Vector3.Dot(gripBodyForward, gripDelta);
				float step = gripDistance > maxGripDistance ? 1 : gripDistance / maxGripDistance;
				speed += step * maxAcceleration * Time.deltaTime;
				if (speed < 0) {
					speed = 0;
				}

				float gripDistanceH = Vector3.Dot(Vector3.Cross(gripBodyForward, Vector3.up).normalized, gripDelta);
				float stepH = gripDistanceH > maxGripDistance ? 1 : gripDistanceH / maxGripDistance;
				float angle = stepH * maxAngularSpeed * Time.deltaTime;
				float cos = Mathf.Cos(angle), sin = Mathf.Sin(angle);
				direction = new Vector3(cos * direction.x - sin * direction.z, direction.y, sin * direction.x + cos * direction.z);
			}
		} else {
			isGripped = false;
		}

		if (!isStopped) {
			trans.position += direction * (speed * Time.deltaTime);
		}
	}
}
