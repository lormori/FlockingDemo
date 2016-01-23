using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------

    private float mCameraSpeed = 5.0f;

    //-----------------------------------------------------------------------------
    // Functions
    //-----------------------------------------------------------------------------

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMouseMovement();

        UpdateKeyPressed();
    }

    private void UpdateKeyPressed()
    {
        Vector3 velocity = Vector3.zero;

        float speedCoefficient = ( mCameraSpeed * Time.deltaTime );

        if (Input.GetKey("a"))
        {
            velocity += -transform.right * speedCoefficient;
        }

        if ( Input.GetKey( "d" ) )
        {
            velocity += transform.right * speedCoefficient;
        }

        if ( Input.GetKey( "w" ) )
        {
            velocity += transform.forward * speedCoefficient;
        }

        if ( Input.GetKey( "s" ) )
        {
            velocity += -transform.forward * speedCoefficient;
        }

        transform.position += velocity;
    }

    private void UpdateMouseMovement()
    {
        if(Input.GetMouseButton(1))
        {
            Vector3 rotation = transform.eulerAngles;

            rotation.y += Input.GetAxis( "Mouse X" ) * mCameraSpeed;
            rotation.x -= Input.GetAxis( "Mouse Y" ) * mCameraSpeed;

            transform.rotation = Quaternion.Euler( rotation );
        }
    }
}
