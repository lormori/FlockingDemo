using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISlidersWidget : MonoBehaviour
{
    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    public Slider separationSlider = null;
    public Slider alignmentSlider = null;
    public Slider cohesionSlider = null;

    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    public void Setup()
    {
        separationSlider.value = App.instance.separationWeight;
        separationSlider.onValueChanged.AddListener( ( value ) => App.instance.separationWeight = value );

        alignmentSlider.value = App.instance.alignmentWeight;
        alignmentSlider.onValueChanged.AddListener( ( value ) => App.instance.alignmentWeight = value );

        cohesionSlider.value = App.instance.cohesionWeight;
        cohesionSlider.onValueChanged.AddListener( ( value ) => App.instance.cohesionWeight = value );
	}
}
