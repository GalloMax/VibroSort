using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BinCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public ObjectCategory binCategory = ObjectCategory.Concrete;

    public Scoreboard sb;

    private bool vibrate = false;

    public float[]
        hapticBuffer_freq =
            new float[] { 1f, 1f, 0f, 0f, 0f, 0.5f, 1f, 0.5f, 0.5f, 0f, 0f };

    public float[]
        hapticBuffer_amp =
            new float[] { 1f, 1f, 0f, 0f, 0f, 0.5f, 1f, 0.5f, 0.5f, 0f, 0f };

    private int duration = 0;

    private int hapticFrameMilli = 250; // Each frame is 50 milliseconds.

    private float hapticsStartTime = 0;

    private TMP_Text debug; 

    public int binNumber;

    void Start()
    {
        debug = GameObject.Find("DEBUG").GetComponent<TMP_Text>();

        
    }

    private void FixedUpdate()
    {

        switch (binNumber)
        {
            case 0:
                hapticBuffer_amp = new float[] { 0f, 1f, 0f };
                hapticBuffer_freq = new float[] { 0f, 1f, 0f };
                break;
            case 1:
                hapticBuffer_amp = new float[] { 0f, 1f, 0f, 0.5f, 0f };
                hapticBuffer_freq = new float[] { 0f, 1f, 0f, 1f, 0f };
                break;
            case 2:
                hapticBuffer_amp = new float[] { 0f, 1f, 0f, 0.5f, 0f, 1f, 0f };
                hapticBuffer_freq = new float[] { 0f, 1f, 0f, 1f, 0f, 1f, 0f };
                break;
            case 3:
                hapticBuffer_amp =
                    new float[] { 0f, 1f, 0f, 0.5f, 0f, 1f, 0f, 0.5f, 0f };
                hapticBuffer_freq =
                    new float[] { 0f, 1f, 0f, 1f, 0f, 1f, 0f, 1f, 0f };
                break;
            case 4:
                hapticBuffer_amp =
                    new float[] {
                        0f,
                        1f,
                        0f,
                        0.5f,
                        0f,
                        1f,
                        0f,
                        0.5f,
                        0f,
                        1f,
                        0f
                    };
                hapticBuffer_freq =
                    new float[] { 0f, 1f, 0f, 1f, 0f, 1f, 0f, 1f, 0f, 1f, 0f };
                break;
        }



        duration = hapticBuffer_amp.Length * hapticFrameMilli;
        
        if (vibrate)
        {
            if ((Time.time - hapticsStartTime) * 1000 < duration)
            {
                debug.text += "in first haptics if statement\n";
                int currentHapticFrame_left =
                    (
                    (int)((Time.time - hapticsStartTime) * 1000) %
                    duration
                    ) /
                    hapticFrameMilli;
                OVRInput
                    .SetControllerVibration(hapticBuffer_freq[currentHapticFrame_left],
                    hapticBuffer_amp[currentHapticFrame_left],
                    OVRInput.Controller.LTouch);
            }
        }

        if (vibrate)
        {
            if ((Time.time - hapticsStartTime) * 1000 < duration)
            {
                debug.text += "in second haptics if statement\n";
                int currentHapticFrame_right =
                    (
                    (int)((Time.time - hapticsStartTime) * 1000) %
                    duration
                    ) /
                    hapticFrameMilli;
                OVRInput
                    .SetControllerVibration(hapticBuffer_freq[currentHapticFrame_right],
                    hapticBuffer_amp[currentHapticFrame_right],
                    OVRInput.Controller.RTouch);
            } else {
                vibrate = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Haptic"))
        {
            // Do haptic based on binCategory
            vibrate = true;
            hapticsStartTime = Time.time;
            debug.text = "on trigger enter\n";
            sb.DecreaseObjectsLeft();
            if (
                other
                    .gameObject
                    .GetComponent<SortInteractable>()
                    .GetCategory() ==
                binCategory
            )
            {
                sb.IncreaseCorrectScore();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Haptic"))
        {
            sb.IncreaseObjectsLeft();
            if (
                other
                    .gameObject
                    .GetComponent<SortInteractable>()
                    .GetCategory() ==
                binCategory
            )
            {
                sb.DecreaseCorrectScore();
            }
        }
    }
}
