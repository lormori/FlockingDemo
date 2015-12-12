using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    private float mSpeed = 1.0f;

    private float mRadiusSquared = 350.0f;

    private int mID = 0;

    private float separationWeight = 0.11f;
    private float alignmentWeight = 0.1f;
    private float cohesionWeight = 0.11f;

    private Vector3 velocity = new Vector3();
    private float maxVelocity = 1.0f;

    float minvelocity = 0.2f;

    //-----------------------------------------------------------------------------
    // Functions
    //-----------------------------------------------------------------------------
    void Start()
    {
        velocity = transform.forward;

        float maxMagnitude = Random.Range( minvelocity, maxVelocity );

        velocity = Vector3.ClampMagnitude( velocity, maxVelocity );
    }

    void Update()
    {
        List<Entity> theFlock = App.instance.theFlock;

        Vector3 separation = Separate( theFlock );
        Vector3 cohesion = Cohere( theFlock );
        Vector3 alignment = Align( theFlock );

        velocity += ( ( separation * separationWeight ) + ( cohesion * cohesionWeight ) + ( alignment * alignmentWeight ) );

        velocity = Vector3.ClampMagnitude( velocity, maxVelocity );

        transform.position += velocity * Time.deltaTime;

        transform.forward = velocity.normalized;
    }

    //-----------------------------------------------------------------------------
    public void SetID( int ID )
    {
        mID = ID;
    }

    //-----------------------------------------------------------------------------
    public int ID
    {
        get { return mID; }
    }

    //-----------------------------------------------------------------------------
    // Flocking Behavior
    //-----------------------------------------------------------------------------
    private Vector3 Separate( List<Entity> theFlock )
    {
        Vector3 separateVector = new Vector3();

        int count = 0;

        for(int i = 0; i < theFlock.Count; i++)
        {
            float distance = ( transform.position - theFlock[ i ].transform.position ).sqrMagnitude;

            if(distance > 0 && distance < mRadiusSquared)
            {
                separateVector += theFlock[ i ].transform.position - transform.position;
                count++;
            }
        }

        separateVector /= count;

        // revert vector
        separateVector *= -1;

        return separateVector.normalized;
    }

    //-----------------------------------------------------------------------------
    private Vector3 Align( List<Entity> theFlock )
    {
        Vector3 forward = new Vector3();

        int count = 0;

        for ( int i = 0; i < theFlock.Count; i++ )
        {
            if(mID != theFlock [i].ID)
            {
                float distance = ( transform.position - theFlock[ i ].transform.position ).sqrMagnitude;

                if ( distance > 0 && distance < mRadiusSquared )
                {
                    forward += theFlock[ i ].transform.forward;
                    count++;
                }
            }
        }

        forward /= count;

        return forward.normalized;
    }

    //-----------------------------------------------------------------------------
    private Vector3 Cohere( List<Entity> theFlock )
    {
        Vector3 cohesionVector = new Vector3();

        int count = 0;

        for ( int i = 0; i < theFlock.Count; i++ )
        {
            if ( mID != theFlock[ i ].ID )
            {
                float distance = ( transform.position - theFlock[ i ].transform.position ).sqrMagnitude;

                if ( distance > 0 && distance < mRadiusSquared )
                {
                    cohesionVector += theFlock[ i ].transform.position;
                    count++;
                }
            }
        }

        cohesionVector /= count;
        cohesionVector = ( cohesionVector - transform.position );

        return cohesionVector.normalized;
    }
}
