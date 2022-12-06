using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SortInteractable : HapticInteractable
{
    public GameObject mySceneGenerator;

    public ObjectGenerator objectGenerator;

    public ObjectCategory objectCategory = ObjectCategory.Concrete;

    // Start is called before the first frame update
    void Start()
    {
        TMP_Text debug = GameObject.Find("DEBUG").GetComponent<TMP_Text>();
        mySceneGenerator = GameObject.Find("Scene Generator");
        objectGenerator = mySceneGenerator.GetComponent<ObjectGenerator>();
        int binNumber =
            objectGenerator.GetBinNumberOfCategory((int) objectCategory);
        debug.text =
            debug.text +
            "Object of category: " +
            objectCategory.ToString() +
            " is of bin number: " +
            binNumber.ToString() +
            "\n";
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    public ObjectCategory GetCategory()
    {
        return objectCategory;
    }
}
