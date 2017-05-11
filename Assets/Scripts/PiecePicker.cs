using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GGL;


public class PiecePicker : MonoBehaviour
{
    public float pieceHeight = 5f;
    public float rayDistance = 1000f;
    public LayerMask selectionIngoreLayer;

    private Piece selectedPiece;
    private CheckerBoard board;

    // Use this for initialization
    void Start()
    {
        //
        board = FindObjectOfType<CheckerBoard>();
        //
        if(board == null)
        {
            Debug.LogError("There is no CheckerBoard in the scene!");
        }
    }

    void CheckSelection()
    {
        // Create a ray from camera mouse position to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GizmosGL.color = Color.red;
        GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance);

        RaycastHit hit;
        // Check if the player hits the mouse button
        if(Input.GetMouseButtonDown(0))
        {
            // Cast a ray to detect piece
            if(Physics.Raycast(ray, out hit, rayDistance))
            {
                // Set the selected piece to be the hit object
                selectedPiece = hit.collider.GetComponent<Piece>();
                //if the user did not hit a piece
                if(selectedPiece == null)
                {
                    Debug.Log("Cannot pick up object: " + hit.collider.name);
                }
            }
        }
    }

    void MoveSelection()
    {
        // Check if a piece has been selected
        if(selectedPiece != null)
        {
            // Create a new ray from camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GizmosGL.color = Color.yellow;
            GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance);
            RaycastHit hit;
            // Raycast to only hit objects that arent piece
            if(Physics.Raycast(ray, out hit, rayDistance, ~selectionIngoreLayer))
            {
                // Obtain the hit point
                GizmosGL.color = Color.blue;
                GizmosGL.AddSphere(hit.point, 0.5f);

                Vector3 piecePos = hit.point + Vector3.up * pieceHeight;
                selectedPiece.transform.position = piecePos;
            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckSelection();
        MoveSelection();
    }
}
