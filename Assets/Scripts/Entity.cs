using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    private float mSpeed = 1.0f;

    private float mRadiusSquared = 5.0f;

    private int mID = 0;

    private Vector3 velocity = new Vector3();
    private float maxVelocity = 1.0f;

    float minvelocity = 0.2f;

    private float maxCubeExtent = 10.0f;
    private float maxCubeExtentX = 20.0f;

    //-----------------------------------------------------------------------------
    // Functions
    //-----------------------------------------------------------------------------
    void Start()
    {
        velocity = transform.forward;

        float maxMagnitude = Random.Range( minvelocity, maxVelocity );

        velocity = Vector3.ClampMagnitude( velocity, maxVelocity );
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        List<Entity> theFlock = App.instance.theFlock;

        Vector3 separation = Separate( theFlock );
        Vector3 cohesion = Cohere( theFlock );
        Vector3 alignment = Align( theFlock );

        velocity += ( ( separation * App.instance.separationWeight ) + ( cohesion * App.instance.cohesionWeight ) + ( alignment * App.instance.alignmentWeight ) );

        velocity = Vector3.ClampMagnitude( velocity, maxVelocity );

        transform.position += velocity * Time.deltaTime;

        transform.forward = velocity.normalized;

        Reposition();
    }

    //-----------------------------------------------------------------------------
    private void Reposition()
    {
        Vector3 position = transform.position;

        if ( position.x >= maxCubeExtentX )
        {
            position.x = maxCubeExtentX - 0.2f;
            velocity.x *= -1;
        }
        else if ( position.x <= -maxCubeExtentX )
        {
            position.x = -maxCubeExtentX + 0.2f;
            velocity.x *= -1;
        }

        if ( position.y >= maxCubeExtent )
        {
            position.y = maxCubeExtent - 0.2f;
            velocity.y *= -1;
        }
        else if ( position.y <= -maxCubeExtent )
        {
            position.y = -maxCubeExtent + 0.2f;
            velocity.y *= -1;
        }

        if ( position.z >= maxCubeExtent )
        {
            position.z = maxCubeExtent - 0.2f;
            velocity.z *= -1;
        }
        else if ( position.z <= -maxCubeExtent )
        {
            position.z = -maxCubeExtent + 0.2f;
            velocity.z *= -1;
        }

        transform.forward = velocity.normalized;
        transform.position = position;
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

        if(count == 0)
        {
            return Vector3.zero;
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

        if ( count == 0 )
        {
            return Vector3.zero;
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

        if ( count == 0 )
        {
            return Vector3.zero;
        }

        cohesionVector /= count;
        cohesionVector = ( cohesionVector - transform.position );

        return cohesionVector.normalized;
    }
}
