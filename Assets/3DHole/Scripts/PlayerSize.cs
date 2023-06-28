using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    [Header(" Settings ")]
    [Tooltip("Increase the limit of blocks eaten to increase the size")]
    [SerializeField] private float scaleIncreaseThreshold;
    [Tooltip("Increase the size of the player multiplier")]
    [SerializeField] private float scaleStep;
    private float scaleValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void IncreaseScale()
    {
        // Set the Y component to 0 to ignore scaling on the Y axis
        Vector3 scaleIncrement = new Vector3(scaleStep, 0f, scaleStep); 
        transform.localScale += scaleIncrement;
    }
    public void CollectibleCollected(float objectSize)
    {
        scaleValue += objectSize;

        if (scaleValue >= scaleIncreaseThreshold)
        {
            IncreaseScale();
            scaleValue = scaleValue % scaleIncreaseThreshold;
        }
    }
}
