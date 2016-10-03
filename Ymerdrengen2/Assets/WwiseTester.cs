using UnityEngine;
using System.Collections;

public class WwiseTester : MonoBehaviour {

    public string WwiseSoundIdentifier;
    public string WwiseSwitchGroup;
    public string WwiseSwitchName;
    public AudioSource SupportSource;

    // Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
            if (SupportSource != null) SupportSource.Play();

            //AkSoundEngine.SetSwitch(WwiseSwitchGroup, WwiseSwitchName, gameObject);
            AkSoundEngine.PostEvent(WwiseSoundIdentifier, gameObject);
        }
	}
}
