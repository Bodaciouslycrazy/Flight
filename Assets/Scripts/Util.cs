using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour {

	const float DeadZone = 0.25f;

    public static AudioSource AudioShot(AudioClip Clip, Vector3 Pos, float Vol = 1f)
    {
        GameObject Pref = Resources.Load("OneShot") as GameObject;
        GameObject Sound = Instantiate(Pref, Pos, Quaternion.Euler(0,0,0));
        AudioSource Source = Sound.GetComponent<AudioSource>();

        Source.clip = Clip;
        Source.volume = Vol;
        Source.Play();

        return Source;
    }

	public static AudioSource AudioShot(AudioClip Clip, Transform Follow, float Vol = 1f)
	{
		GameObject Pref = Resources.Load("OneShotFollow") as GameObject;
		GameObject Sound = Instantiate(Pref, Follow.position, Quaternion.Euler(0, 0, 0));
		AudioSource Source = Sound.GetComponent<AudioSource>();
		Sound.GetComponent<FollowObject>().Obj = Follow;

		Source.clip = Clip;
		Source.volume = Vol;
		Source.Play();

		return Source;
	}

	public static void UpdateButton(ref ButtonInfo Button)
	{
		bool LastFrame = Button.Down;

		Button.Down = Input.GetAxisRaw(Button.Name) >= 1;
		Button.Pressed = Button.Down && !LastFrame;
		Button.Released = !Button.Down && LastFrame;
	}

	public static void UpdateAxis(ref AxisInfo Axis)
	{
		float LastFrame = Axis.Value;

		Axis.Value = Input.GetAxisRaw(Axis.Name);
		if(Mathf.Abs(LastFrame) < DeadZone && Mathf.Abs(Axis.Value) > DeadZone)
		{
			Axis.Directional = (Axis.Value >= 0) ? 1 : -1;
		}
	}

	public struct ButtonInfo
	{
		public string Name;
		public bool Down;
		public bool Pressed;
		public bool Released;
	}

	public struct AxisInfo
	{
		public string Name;
		public float Value;
		public int Directional;
	}

	public static ButtonInfo NewButton(string Name)
	{
		ButtonInfo B = new ButtonInfo();
		B.Name = Name;
		B.Down = false;
		B.Pressed = false;
		B.Released = false;
		return B;
	}

	public static AxisInfo NewAxis(string Name)
	{
		AxisInfo A = new AxisInfo();
		A.Name = Name;
		A.Value = 0f;
		A.Directional = 0;
		return A;
	}
}
