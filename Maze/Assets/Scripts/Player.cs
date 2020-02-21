using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.

    private Animator animator;                    //Used to store a reference to the Player's animator component.

    //Start overrides the Start function of MovingObject
    protected override void Start()
    {
        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();

        //Call the Start function of the MovingObject base class.
        base.Start();
    }

    private void Update()
    {
        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        int horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        int vertical = (int)(Input.GetAxisRaw("Vertical"));

        //Check if moving horizontally, if so set vertical to zero.
        if (horizontal != 0)
        {
            vertical = 0;
        }

        animator.SetInteger("velocityX", horizontal);
        animator.SetInteger("velocityY", vertical);

        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0)
        {
            //Call AttemptMove passing in the generic parameter enemy(?), since that is what Player may interact with if they encounter one (by attacking(?) it)
            //Pass in horizontal and vertical as parameters to specify the direction to move Player in.
            AttemptMove<Player>(horizontal, vertical);
        }
    }

    //AttemptMove overrides the AttemptMove function in the base class MovingObject
    //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        //Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
        base.AttemptMove<T>(xDir, yDir);

        //Hit allows us to reference the result of the Linecast done in Move.
        RaycastHit2D hit;

        //If Move returns true, meaning Player was able to move into an empty space.
        if (Move(xDir, yDir, out hit))
        {
            //something?
        }

        //Since the player has moved and lost food points, check if the game has ended.
        CheckIfGameOver();
    }


    //OnCantMove overrides the abstract function OnCantMove in MovingObject.
    //It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
    protected override void OnCantMove<T>(T component)
    {

    }


    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the tag of the trigger collided with is Exit.
        if (other.CompareTag("Exit"))
        {
            //Next level ?
        }

        //Check if the tag of the trigger collided with is Food.
        else if (other.CompareTag("Button"))
        {
            //Trigger button
        }
    }


    //CheckIfGameOver checks if the player is out of food points and if so, ends the game.
    private void CheckIfGameOver()
    {
        //hm
    }
}
