using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawControl : MonoBehaviour
{
    [SerializeField] private float speed = .1f;
    [SerializeField] private Transform[] Limiters;
    private float minX, maxX, minZ, maxZ;
    private Vector3 mouse, clickPoint, destination;
    private bool isClawOpen, isMovingToClickPoint;

    private ClawManager clawManager;
    [SerializeField] private Rope rope;


    // Start is called before the first frame update
    void Start()
    {
        minX = Limiters[0].position.x;
        maxX = Limiters[1].position.x;
        minZ = Limiters[2].position.z;
        maxZ = Limiters[3].position.z;

        clawManager = ClawManager.Instance;
        clawManager.OnClawStateChenge += StateChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClawOpen)
        {
            //Keyboard Control
            float xDirection = Input.GetAxis("Horizontal");
            float zDirection = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(xDirection, 0, zDirection);

            transform.position += moveDirection * speed;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, Mathf.Clamp(transform.position.z, minZ, maxZ));


            if (Input.GetKeyDown(KeyCode.Space))
            {
                rope.Stretch();
            }


            //Mouse Control
            if (Input.GetMouseButtonDown(0))
            {
                mouse = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    clickPoint = hit.point;
                }

                if (clickPoint.x > minX && clickPoint.x < maxX && clickPoint.z > minZ && clickPoint.z < maxZ) 
                {
                    destination = new Vector3(clickPoint.x, transform.position.y, clickPoint.z);
                    isMovingToClickPoint = true;
                }
            }

            if (isMovingToClickPoint)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed);
                if (transform.position == destination) isMovingToClickPoint= false;
            }
        }
        else
        {
            isMovingToClickPoint = false;
        }
    }

    private void StateChangeHandler(ClawState state)
    {
        isClawOpen = state == ClawState.Open ? true : false;
    }

}
