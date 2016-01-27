using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using XboxCtrlrInput;

public class VibrationHandler : MonoBehaviour
{
	public enum Motor
	{
		Left,
		Right,
		Both
	}

	public float variablepowerp1 = 0f;
	public float variablepowerp2 = 0f;
	Motor motor;

	void VibrateOnBothMotors(int player)
	{
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 1)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 1f, 1f);
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 2)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.Two, 1f, 1f);
	}

	void VibrateOnLeftMotor(int player)
	{
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 1)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 1f, 0f);
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 2)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.Two, 1f, 0f);
	}

	void VibrateOnLeftMotorVariablePower(int player, float power)
	{
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 1)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, power, 0f);
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 2)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.Two, power, 0f);
	}

	void VibrateOnRightMotor(int player)
	{
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 1)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 0f, 1f);
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 2)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.Two, 0f, 1f);
	}

	void VibrateOff(int player)
	{
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 1)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 0f, 0f);
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 2)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.Two, 0f, 0f);
	}

	void Update()
	{
		if (variablepowerp1 > 0f)
			VibrateOnLeftMotorVariablePower(1, variablepowerp1);
		if (variablepowerp2 > 0f)
			VibrateOnLeftMotorVariablePower(2, variablepowerp2);
		variablepowerp1 -= Time.deltaTime;
		variablepowerp2 -= Time.deltaTime;
		Mathf.Clamp(variablepowerp1, 0f, 0.25f);
		Mathf.Clamp(variablepowerp2, 0f, 0.25f);
		if (variablepowerp1 < 0f)
			variablepowerp1 = 0f;
		if (variablepowerp2 < 0f)
			variablepowerp2 = 0f;
		if (variablepowerp1 > 0.25f)
			variablepowerp1 = 0.25f;
		if (variablepowerp2 > 0.25f)
			variablepowerp2 = 0.25f;
	}

	public void increaseVariablePower(int player)
	{
		if (player == 1) {
			variablepowerp1 += 1f;
		} else {
			variablepowerp2 += 1f;
		}
		return;
	}

	IEnumerator VibrateForSecs(int player, float secs, Motor motor)
	{
		while (secs > 0f) {
			switch (motor) {
				case Motor.Both:
					VibrateOnBothMotors(player);
					break;
				case Motor.Left:
					VibrateOnLeftMotor(player);
					break;
				case Motor.Right:
					VibrateOnRightMotor(player);
					break;
				default:
					break;
			}
			secs -= Time.deltaTime;
			yield return 0;
		}
		VibrateOff(player);
		yield break;
	}

	public void VibrateOnceForPlayer(int player)
	{
		if (!InputManager.vibration)
			return;
		StartCoroutine(VibrateForSecs(player, 0.2f, Motor.Both));
	}

	public void VibrateLongForPlayer(int player)
	{
		if (!InputManager.vibration)
			return;
		StartCoroutine(VibrateForSecs(player, 1f, Motor.Left));
	}

	void OnDisable()
	{
		VibrateOff(1);
		VibrateOff(2);
	}

}
