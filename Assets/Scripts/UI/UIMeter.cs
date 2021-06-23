using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMeter : MonoBehaviour
{
	private RectTransform meter;
	private float defaultHeight;
	private float maxMeterSize;

    // Start is called before the first frame update
    void Awake()
    {
		meter = GetComponent<RectTransform>();
		defaultHeight = meter.sizeDelta.y;
		maxMeterSize = meter.sizeDelta.x;
    }

	// Update is called once per frame
	public void UpdateMeter(float in_currentValue, float in_maxValue)
    {
		meter.sizeDelta = new Vector2(maxMeterSize * (in_currentValue / in_maxValue), defaultHeight);       
    }
}
