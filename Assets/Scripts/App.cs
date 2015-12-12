using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class App : MonoBehaviour
{
    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    public Entity templatePrefab = null;

    public List<Entity> theFlock = new List<Entity>();

    public static App instance = null;

    private int numberOfEntities = 20;

    //-----------------------------------------------------------------------------
    // Functions
    //-----------------------------------------------------------------------------
    void Start ()
    {
        instance = this;
	}

    //-----------------------------------------------------------------------------
    private void InstantiateFlock()
    {
        for ( int i = 0; i < numberOfEntities; i++ )
        {
            Entity flockEntity = Instantiate( templatePrefab );

            flockEntity.SetID( i );

            theFlock.Add( flockEntity );
        }
    }

    //-----------------------------------------------------------------------------
	void Update()
    {
	
	}
}
