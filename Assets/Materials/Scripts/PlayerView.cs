using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public float maxRotationDegreeSecond = 75f;
    public float mouseRotaionSpeed = 100f;

    [Range(0, 45)]
    public float maxPitchUpAngle = 45f;

    [Range(0, 45)]
    public float maxPitchDownAngle = 5f;
    void start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        MouseInput();
    }
    private void MouseInput()
    {
        Vector3 rotation = new Vector3 (-Input.GetAxis ("Mouse Y"), Input.GetAxis ("Mouse X"), 0 ) ;
        RotateView(rotation * mouseRotaionSpeed);
    }
    private void RotateView (Vector3 rotation)
    {
        //Rotate Player
        transform.Rotate( rotation * Time.deltaTime);
        //Limit Player rotation pitch
        float playerPitch = LimitPitch();
        //Apply clamped pitch and clear roll
        transform.rotation = Quaternion.Euler(playerPitch, transform.eulerAngles.y, 0);

    }
    private float LimitPitch()
    {
        float playerPitch = transform.eulerAngles.x;
        float maxPitchUp = 360 - maxPitchUpAngle;
        float maxPitchDown = maxPitchDownAngle;
        if (playerPitch > 180 && playerPitch < maxPitchUp)
        {
            //Limit pitch up
            playerPitch = maxPitchUp;

        }else if (playerPitch < 180 && playerPitch > maxPitchDown)
        {
            //limit pitch down 
            playerPitch = maxPitchDown;
        }
        return playerPitch;
    }
}
