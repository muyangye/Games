/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public GlobalStateManager globalManager;

    //Player parameters
    [Range (1, 2)] //Enables a nifty slider in the editor
    public int playerNumber = 1;
    //Indicates what player this is: P1 or P2
    public float moveSpeed = 5f;
    public bool canDropBombs = true;
    //Can the player drop bombs?
    public bool canMove = true;
    //Can the player move?
    public bool dead = false;
    //Is the player dead?
    public KeyCode up;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;
    public KeyCode drop;
    //The key for movement
    public int health = 5;
    //The player's current health
    public Text healthText;
    //Display the player's current health
    public int dropLimit = 40;
    //The maximum amount of bombs the player can drop in a game
    public int dropCount = 0;
    //Count how many bombs the player has dropped

    private int bombLimit = 3;
    //The number of bombs the player can drop at a time
    public List<Bomb> bombList = new List<Bomb>();
    //Keeps track of bombs the player dropped

    //Prefabs
    public GameObject bombPrefab;

    //Cached components
    private Rigidbody rigidBody;
    private Transform myTransform;
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        //Cache the attached components for better performance and less typing
        rigidBody = GetComponent<Rigidbody> ();
        myTransform = transform;
        animator = myTransform.Find ("PlayerModel").GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateMovement ();
        healthText.text = "Player " + playerNumber.ToString() + " Health: " + health.ToString();
        // Each frame, check if a bomb the player dropped has exploded
        for (int i = 0; i < bombList.Count; i++)
        {
            Bomb bomb = bombList[i];
            // If so, remove that bomb from the list
            if (bomb.exploded)
            {
                bombList.Remove(bomb);
            }
        }
    }

    private void UpdateMovement ()
    {
        animator.SetBool ("Walking", false);

        if (!canMove)
        { //Return if player can't move
            return;
        }

        UpdatePlayerMovement();
    }

    private void UpdatePlayerMovement ()
    {
        if (Input.GetKey (up))
        { //Up movement
            rigidBody.velocity = new Vector3 (rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            myTransform.rotation = Quaternion.Euler (0, 0, 0);
            animator.SetBool ("Walking", true);
        }

        if (Input.GetKey (left))
        { //Left movement
            rigidBody.velocity = new Vector3 (-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler (0, 270, 0);
            animator.SetBool ("Walking", true);
        }

        if (Input.GetKey (down))
        { //Down movement
            rigidBody.velocity = new Vector3 (rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            myTransform.rotation = Quaternion.Euler (0, 180, 0);
            animator.SetBool ("Walking", true);
        }

        if (Input.GetKey (right))
        { //Right movement
            rigidBody.velocity = new Vector3 (moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler (0, 90, 0);
            animator.SetBool ("Walking", true);
        }

        if (canDropBombs && Input.GetKeyDown (drop) && dropCount < dropLimit && bombList.Count < bombLimit)
        { //Drop bomb
            DropBomb ();
            dropCount += 1;
        }
    }

    /// <summary>
    /// Drops a bomb beneath the player
    /// </summary>
    private void DropBomb ()
    {
        if (bombPrefab)
        { //Check if bomb prefab is assigned first
            GameObject bomb = Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x), Mathf.RoundToInt(myTransform.position.y), Mathf.RoundToInt(myTransform.position.z))
            , bombPrefab.transform.rotation);
            Bomb b = bomb.GetComponent<Bomb>();
            bombList.Add(b);
        }
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Explosion"))
        {
            health -= 1;
            if (health == 0)
            {
                dead = true;
                globalManager.PlayerDied(playerNumber);
                Destroy(gameObject);
                healthText.text = "Player " + playerNumber.ToString() + " died!";
                // Debug.Log ("P" + playerNumber + " hit by explosion!");
            }
        }
    }
}
