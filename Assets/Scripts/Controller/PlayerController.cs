using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequiredComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	public Interactable focus;
	public LayerMask movementMask;
	public Animator animator;


	Camera cam;
	PlayerMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
		
		if (Input.GetMouseButton(0))
        {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				// Declare Intractable
				Interactable interactable = hit.collider.GetComponent<Interactable>();

				//If Player hit object that is interactable
				if (interactable != null)
				{
					SetFocus(interactable);
				}
				else if (Physics.Raycast(ray, out hit, 100, movementMask))
				{
						// Move our player to what we hit
						motor.MoveToPoint(hit.point);
						DropFocus();
				}
				else
				{
					DropFocus();
				}

			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Attack();
		}
	}

	void SetFocus(Interactable newFocus)
    {
		if(newFocus != focus)
        {
			if (focus != null)
            {
				focus.OnDefocused();
            }

			focus = newFocus;
			motor.FollowTarget(newFocus);
        }
		
		newFocus.OnFocused(transform);
		
    }

	void DropFocus()
    {
		if (focus != null)
		{
			focus.OnDefocused();
		}
		
		focus = null;
		motor.StopFollowingTarget();
    }

	void Attack()
    {
		// Play Attack animation
		animator.SetTrigger("attack");
	
		//Detect enemies in range of attack

		//Damage Them
    }
}
