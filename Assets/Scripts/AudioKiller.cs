using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioKiller : MonoBehaviour {

    public AudioSource Source;

	void Update () {
        if (!Source.isPlaying)
            Destroy(gameObject);
	}
}
