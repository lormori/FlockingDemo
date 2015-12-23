using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class App : MonoBehaviour
{
    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    public Entity templatePrefab = null;

    public float separationWeight = 0.8f;
    public float alignmentWeight = 0.5f;
    public float cohesionWeight = 0.7f;

    public UISlidersWidget sliderWidget = null;

    [HideInInspector]
    public List<Entity> theFlock = new List<Entity>();

    public static App instance = null;

    private int numberOfEntities = 200;

    float minRotation = 0.2f;
    float maxRotation = 0.8f;

    //-----------------------------------------------------------------------------
    // Functions
    //-----------------------------------------------------------------------------
    void Start ()
    {
        instance = this;

        sliderWidget.Setup();

        InstantiateFlock();
    }

    //-----------------------------------------------------------------------------
    private void InstantiateFlock()
    {
        for ( int i = 0; i < numberOfEntities; i++ )
        {
            Entity flockEntity = Instantiate( templatePrefab );

            flockEntity.transform.rotation = Random.rotation;
            //lnew Vector3( Random.Range( minRotation, maxRotation ), Random.Range( minRotation, maxRotation ), Random.Range( minRotation, maxRotation ) );

            flockEntity.SetID( i );

            theFlock.Add( flockEntity );
        }
    }

    //-----------------------------------------------------------------------------
	void Update()
    {
	
	}
}
