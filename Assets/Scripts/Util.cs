using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour {

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
}
