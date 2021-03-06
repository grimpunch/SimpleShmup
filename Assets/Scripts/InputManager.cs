﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using XboxCtrlrInput;

public static class InputManager
{
	#region buttons - Player 1

    public static bool Pause_P1 {
        get {
            if (XCI.GetNumPluggedCtrlrs() <= 0) { // #linuxproof
                return Input.GetKeyUp(KeyCode.Escape);
            }
            else {
                return(Input.GetKeyUp(KeyCode.Escape) || XCI.GetButtonUp(XboxButton.Start, 1) ? true : false);
            }
        }
    }

	public static bool Fire1_P1 {
		get {
            if (XCI.GetNumPluggedCtrlrs() <= 0) { // #linuxproof
                return Input.GetButton("Fire1_P1");
            }
            else {
			// Return true if pressing Fire1_P1 on keyboard, or pressing A on controller 1.
			return (Input.GetButton("Fire1_P1") || XCI.GetButton(XboxButton.A, 1) ? true : false);
            }
		}
	}

	public static bool Fire2_P1 {
		get {
            if (XCI.GetNumPluggedCtrlrs() <= 0) { // #linuxproof
                return Input.GetButton("Fire2_P1");
            }
            else {
			// Return true if pressing Fire2_P1 on keyboard, or pressing X on controller 1.
			return (Input.GetButton("Fire2_P1") || XCI.GetButton(XboxButton.X, 1) ? true : false);
            }
		}
	}

	public static bool Fire3_P1 {
		get {
            if (XCI.GetNumPluggedCtrlrs() <= 0) { // #linuxproof
                return Input.GetButton("Fire3_P1");
            }
            else {
			// Return true if pressing Fire3_P1 on keyboard, or pressing B on controller 1.
			return (Input.GetButton("Fire3_P1") || XCI.GetButton(XboxButton.B, 1) ? true : false);
            }
		}
	}

	#endregion

	#region movement axes - Player 1

	public static float Horizontal_P1 {
		get {
			if (Input.GetAxis("Horizontal_P1") > 0 || Input.GetAxis("Horizontal_P1") < 0
			    || XCI.GetNumPluggedCtrlrs() <= 0) {
				return Input.GetAxis("Horizontal_P1");
			}
			if (XCI.GetDPad(XboxDPad.Left, 1) || XCI.GetDPad(XboxDPad.Right, 1)) {
				float x = 0.0f;
				if (XCI.GetDPad(XboxDPad.Left, 1))
					x = -1f;
				if (XCI.GetDPad(XboxDPad.Right, 1))
					x = 1f;
				return x;
			} 
			if (XCI.GetAxis(XboxAxis.LeftStickX, 1) > 0.0f || XCI.GetAxis(XboxAxis.LeftStickX, 1) < 0.0f) {
				return XCI.GetAxis(XboxAxis.LeftStickX, 1);
			} else {
				return Input.GetAxis("Horizontal_P1");
			}
		}
	}

	public static float Vertical_P1 {
		get {
			if (Input.GetAxis("Vertical_P1") > 0 || Input.GetAxis("Vertical_P1") < 0
			    || XCI.GetNumPluggedCtrlrs() <= 0) {
				return Input.GetAxis("Vertical_P1");
			}
			if (XCI.GetDPad(XboxDPad.Down, 1) || XCI.GetDPad(XboxDPad.Up, 1)) {
				float x = 0.0f;
				if (XCI.GetDPad(XboxDPad.Down, 1))
					x = -1f;
				if (XCI.GetDPad(XboxDPad.Up, 1))
					x = 1f;
				return x;
			} 
			if (XCI.GetAxis(XboxAxis.LeftStickY, 1) > 0.0f || XCI.GetAxis(XboxAxis.LeftStickY, 1) < 0.0f) {
				return XCI.GetAxis(XboxAxis.LeftStickY, 1);
			} else {
				return Input.GetAxis("Vertical_P1");
			}
		}
	}

	#endregion


	#region movement axes - Player 2

	public static float Horizontal_P2 {
		get {
			if (Input.GetAxis("Horizontal_P2") > 0 || Input.GetAxis("Horizontal_P2") < 0
			    || XCI.GetNumPluggedCtrlrs() <= 0) {
				return Input.GetAxis("Horizontal_P2");
			}
			if (XCI.GetDPad(XboxDPad.Left, 2) || XCI.GetDPad(XboxDPad.Right, 2)) {
				float x = 0.0f;
				if (XCI.GetDPad(XboxDPad.Left, 2))
					x = -1f;
				if (XCI.GetDPad(XboxDPad.Right, 2))
					x = 1f;
				return x;
			} 
			if (XCI.GetAxis(XboxAxis.LeftStickX, 2) > 0.0f || XCI.GetAxis(XboxAxis.LeftStickX, 2) < 0.0f) {
				return XCI.GetAxis(XboxAxis.LeftStickX, 2);
			} else {
				return Input.GetAxis("Horizontal_P2");
			}
		}
	}

	public static float Vertical_P2 {
		get {
			if (Input.GetAxis("Vertical_P2") > 0 || Input.GetAxis("Vertical_P2") < 0
			    || XCI.GetNumPluggedCtrlrs() <= 0) {
				return Input.GetAxis("Vertical_P2");
			}    
			if (XCI.GetDPad(XboxDPad.Down, 2) || XCI.GetDPad(XboxDPad.Up, 2)) {
				float x = 0.0f;
				if (XCI.GetDPad(XboxDPad.Down, 2))
					x = -1f;
				if (XCI.GetDPad(XboxDPad.Up, 2))
					x = 1f;
				return x;
			} 
			if (XCI.GetAxis(XboxAxis.LeftStickY, 2) > 0.0f || XCI.GetAxis(XboxAxis.LeftStickY, 2) < 0.0f) {
				return XCI.GetAxis(XboxAxis.LeftStickY, 2);
			} else {
				return Input.GetAxis("Vertical_P2");
			}
		}
	}

	#endregion

	#region buttons - Player 2

	public static bool Fire1_P2 {
		get {
			// Return true if pressing Fire1_P2 on keyboard, or pressing A on controller 2.
			return (Input.GetButton("Fire1_P2") || XCI.GetButton(XboxButton.A, 2) ? true : false);
		}
	}

	public static bool Fire2_P2 {
		get {
			// Return true if pressing Fire2_P2 on keyboard, or pressing X on controller 2.
			return (Input.GetButton("Fire2_P2") || XCI.GetButton(XboxButton.X, 2) ? true : false);
		}
	}

	public static bool Fire3_P2 {
		get {
			// Return true if pressing Fire3_P2 on keyboard, or pressing B on controller 2.
			return (Input.GetButton("Fire3_P2") || XCI.GetButton(XboxButton.B, 2) ? true : false);
		}
	}

	#endregion

	public static bool vibration = true;

	public static void vibratePadForPlayer(int player)
	{

        if (Application.platform == RuntimePlatform.LinuxPlayer) {
			return; // no good vibes for linux :'(
		}

		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 1)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 50f, 50f);
		if (XboxCtrlrInput.XCI.IsPluggedIn(player) && player == 2)
			XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.Two, 50f, 50f);
	}

}