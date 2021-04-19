using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles all the physics and inputs to drive a Car.
public class CarController : MonoBehaviour
{
	private float horizontalInput;
	private float verticalInput;
	private float steeringAngle;
	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;
	public float turboForce = 25;
	private Rigidbody myRb;
	public float rayLength;
	private bool isInAir = false;
	bool canTurbo = false;
	AudioSource myAudioSrc;

	bool gameOver = false;
	public void GetInput()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
		if(Input.GetKey(KeyCode.Space))
			Jump();
	}

	private void Steer()
	{
		steeringAngle = maxSteerAngle * horizontalInput;
		frontDriverW.steerAngle = steeringAngle/2;
		frontPassengerW.steerAngle = steeringAngle/2;
	}

	private void Accelerate()
	{
			if(verticalInput > 0 || verticalInput < 0)
			{
				myRb.drag = 0;
				myRb.angularDrag = 0;
				frontDriverW.motorTorque = verticalInput * motorForce;
				frontPassengerW.motorTorque = verticalInput * motorForce;
				if(canTurbo) Turbo();
			}
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider n_collider, Transform n_transform)
	{
		Vector3 pos = transform.position;
		Quaternion quat = transform.rotation;
		n_collider.GetWorldPose(out pos, out quat);
		n_transform.position = pos;
		n_transform.rotation = quat;
	}

	private void FixedUpdate()
	{
		if(!gameOver)
		{
			GetInput();
			Steer();
			Accelerate();
			UpdateWheelPoses();
			if(!IsGrounded())
				InAir();
		}
	}

	private void Awake() {
		myRb = GetComponent<Rigidbody>();
		myRb.drag = 10;
		myRb.angularDrag = 10;
		myAudioSrc = GetComponent<AudioSource>();
		FindObjectOfType<GameModeManager>().gameOverEvent += OnGameOver;
	}

	public void Turbo()
	{
		frontDriverW.motorTorque *= turboForce;
        frontPassengerW.motorTorque *= turboForce;
		canTurbo = false;
	}

	private void Jump()
	{
		if(IsGrounded())
		{
			isInAir = true;
			myRb.AddForce(transform.up * 1000, ForceMode.Acceleration);
			myAudioSrc.Play();
		}
	}

	public void ForceJump(float jumpForce)
	{
		if(IsGrounded())
		{
			isInAir = true;
			myRb.AddForce(transform.up * jumpForce, ForceMode.Acceleration);
			myAudioSrc.Play();
		}
	}

	private void InAir()
	{
		if(isInAir)
			myRb.AddForce(transform.up * -25, ForceMode.Acceleration);
	}

	private bool IsGrounded(){ 
 			return Physics.Raycast(transform.position, -Vector3.up, rayLength);
 	}

	 public void LoseControl()
	 {
		 ChangeStiffnes(frontDriverW,1);
		 ChangeStiffnes(frontPassengerW,1);
		 ChangeStiffnes(rearDriverW,1);
		 ChangeStiffnes(rearPassengerW,1);
		 StartCoroutine("GetControl");
	 }

	 private void ChangeStiffnes(WheelCollider myWheel, float amount)
	 {
		 WheelFrictionCurve frictionCurve = myWheel.sidewaysFriction;
		 frictionCurve.stiffness = amount;
		 myWheel.sidewaysFriction = frictionCurve;
	 }


	 IEnumerator GetControl()
	 {
		 yield return new WaitForSeconds(1.5f);
		 ChangeStiffnes(frontDriverW,10);
		 ChangeStiffnes(frontPassengerW,10);
		 ChangeStiffnes(rearDriverW,10);
		 ChangeStiffnes(rearPassengerW,10); 
	 }

	 private void OnTriggerEnter(Collider other) {
		 IPickUp pickup = other.GetComponent<IPickUp>();
		 if(pickup != null)
		 	pickup.PickedUp();
	 }

	 public void AddTurbo()
	 {
		 canTurbo = true;
		 Debug.Log("TURBO AVAILABLE!");
	 }

	 private void OnGameOver()
	 {
		 gameOver = true;
	 }
}
