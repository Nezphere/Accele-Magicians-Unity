using UnityEngine;
using UnityEngine.XR;

public class VrPlayer : MonoBehaviour {
	public Transform lHandTrans, rHandTrans;
	public float damping = 100, rotationDamping = 100;

	public void Update() {
		var lHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
		var lHandRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
		lHandTrans.localPosition = Vector3.Lerp(lHandTrans.localPosition, lHandPosition, Time.deltaTime * damping);
		lHandTrans.localRotation = Quaternion.Slerp(lHandTrans.localRotation, lHandRotation, Time.deltaTime * rotationDamping);

		var rHandPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
		var rHandRotation = InputTracking.GetLocalRotation(XRNode.RightHand);
		rHandTrans.localPosition = Vector3.Lerp(rHandTrans.localPosition, rHandPosition, Time.deltaTime * damping);
		rHandTrans.localRotation = Quaternion.Slerp(rHandTrans.localRotation, rHandRotation, Time.deltaTime * rotationDamping);
	}
}
