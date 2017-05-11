using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerBoard : MonoBehaviour
{
    public GameObject blackPiece;
    public GameObject whitePiece;

    public int boardX = 8, boardZ = 8;
    public float pieceRadius = 0.5f;

    public Piece[,] pieces;
    private int halfBoardX, halfBoardZ;
    private float pieceDiameter;
    private Vector3 bottomleft;

    // Use this for initialization
    void Start()
    {
        halfBoardX = boardX / 2;
        halfBoardZ = boardZ / 2;
        pieceDiameter = pieceRadius * 2f;
        bottomleft = transform.position - Vector3.right * halfBoardX
                                        - Vector3.forward * halfBoardZ;
        CreateGrid();
    }

    void CreateGrid()
    {
        // Intialize the 2D array
        pieces = new Piece[boardX, boardZ];

        #region Genrate white Pieces
        // Loop through board columns and skip 2 each time
        for(int x = 0; x < boardX; x += 2)
        {
            // Loop through first 3 rows
            for(int z = 0; z < 3; z++)
            {
                bool evenRow = z % 2 == 0;
                int gridX = evenRow ? x : x + 1;
                int gridZ = z;
                // Generate the piece
                GenratePiece(whitePiece, gridX, gridZ);
            }
        }
        #endregion

        #region Genrate Black Pieces
        // Loop through board columns and skip 2 each time
        for (int x = 0; x < boardX; x += 2)
        {
            // Loop through first 3 rows
            for (int z = boardZ - 3; z < boardZ; z++)
            {
                bool evenRow = z % 2 == 0;
                int gridX = evenRow ? x : x + 1;
                int gridZ = z;
                // Generate the piece
                GenratePiece(blackPiece, gridX, gridZ);
            }
        }

        #endregion
    }

    void GenratePiece(GameObject piecePrefab, int x, int z)
    {
        // Create an instance of piece
        GameObject clone = Instantiate(piecePrefab);
        // Set the parent to be this transform
        clone.transform.SetParent(transform);
        //GetComponent the piece component from the clone
        Piece piece = clone.GetComponent<Piece>();
        // Place the piece
        PlacePiece(piece, x, z);
    }

    void PlacePiece(Piece piece, int x, int z)
    {
        // Calculate offset for piece based on coordinate
        float xOffset = x * pieceDiameter + pieceRadius;
        float zOffset = z * pieceDiameter + pieceRadius;
        // Set piece's new grid coordinate
        piece.gridX = x;
        piece.gridZ = z;
        // Move piece physically to board coordinate
        piece.transform.position = bottomleft + Vector3.right * xOffset
                                              + Vector3.forward * zOffset;
        // Set piece in array slot
        pieces[x, z] = piece;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
