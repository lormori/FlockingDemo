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
        
        velocity += FlockingBehaviour();

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
    private Vector3 FlockingBehaviour()
    {
        List<Entity> theFlock = App.instance.theFlock;

        Vector3 cohesionVector = new Vector3();
        Vector3 separateVector = new Vector3();
        Vector3 forward = new Vector3();

        int count = 0;

        for ( int i = 0; i < theFlock.Count; i++ )
        {
            if ( mID != theFlock[ i ].ID )
            {
                float distance = ( transform.position - theFlock[ i ].transform.position ).sqrMagnitude;

                if ( distance > 0 && distance < mRadiusSquared )
                {
                    separateVector += theFlock[ i ].transform.position - transform.position;
                    forward += theFlock[ i ].transform.forward;
                    cohesionVector += theFlock[ i ].transform.position;

                    count++;
                }
            }
        }

        if ( count == 0 )
        {
            return Vector3.zero;
        }

        // revert vector
        // separation step
        separateVector /= count;
        separateVector *= -1;

        // forward step
        forward /= count;

        // cohesione step
        cohesionVector /= count;
        cohesionVector = ( cohesionVector - transform.position );

        Vector3 flockingVector =    ( ( separateVector.normalized * App.instance.separationWeight ) + 
                                    ( cohesionVector.normalized * App.instance.cohesionWeight ) + 
                                    ( forward.normalized * App.instance.alignmentWeight ) );

        return flockingVector;
    }
}
