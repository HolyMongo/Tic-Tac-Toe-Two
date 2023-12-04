using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlacePieces : MonoBehaviour
{
    [SerializeField] private GameObject xPiece;
    [SerializeField] private GameObject oPiece;
    [SerializeField] private GameObject currentPiece;

    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private TextMeshProUGUI turnText;

    bool firstTurn = true;

    [SerializeField] private List<GameObject> boardPieces;
    [SerializeField] private int boardPieceToMoveTo;
    [SerializeField] private int currentBoardPiece;
    [SerializeField] private List<GameObject> boardBorders;

    private bool xTurnToPlace;
    private int turnNumber;

    [SerializeField] GameObject test1;
    [SerializeField] GameObject test2;

    [SerializeField] private Vector3 mousePos;
    [SerializeField] private Vector3 mouseWorldPos;
    [SerializeField] private LayerMask squareLayerMask;
    private PlayerInputsActions inputActions;

    GameObject selectedSquare;
    Vector3 rayDir;
    RaycastHit hit;
    Ray placingray;

    void Start()
    {
        if (xTurnToPlace)
        {
            currentPiece = xPiece;
        }
        else
        {
            currentPiece = oPiece;
        }
        inputActions = new PlayerInputsActions();
        Debug.Log("Creates it!");
        inputActions.InGame.Enable();
        Debug.Log("Enables it!");
        inputActions.InGame.PlacePiece.performed += PlacePiece;
        Debug.Log("Adds Event!");
    }

    public List<GameObject> ShareBoardPieces()
    {
        return boardPieces;
    }

    public void ChangeBoardBoardersToVictoryColor(Material victoryMaterial)
    {
        for (int i = 0; i < boardBorders.Count; i++)
        {
            boardBorders[i].GetComponent<MeshRenderer>().material = victoryMaterial;
        }
    }

    private void PlacePiece(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       
        Debug.Log("Click");
        if (Physics.Raycast(placingray, out hit, Mathf.Infinity, squareLayerMask))
        {
            test1 = hit.transform.parent.transform.parent.gameObject;
            test2 = boardPieces[boardPieceToMoveTo];
            //hit.transform.gameObject.GetComponentInParent<Transform>().gameObject.GetComponentInParent<Transform>().gameObject
            if (hit.transform.parent.transform.parent.gameObject == boardPieces[boardPieceToMoveTo] || firstTurn)
            {
                firstTurn = false;
                if (!hit.transform.gameObject.GetComponent<PiecePlaced>().IsPiecePlaced())
                {
                    string whoseTurnIsIt;
                    Debug.Log("Should Find Ray");
                    xTurnToPlace = !xTurnToPlace;
                    if (xTurnToPlace)
                    {
                        currentPiece = xPiece;
                        whoseTurnIsIt = "X";
                    }
                    else
                    {
                        currentPiece = oPiece;
                        whoseTurnIsIt = "O";
                    }

                    turnText.text = "Turn: " + whoseTurnIsIt;



                    boardPieces[boardPieceToMoveTo].gameObject.GetComponent<WhichSquareWasClicked>().ChangeBackBoardColor(false);
                    hit.transform.gameObject.GetComponent<PiecePlaced>().PlacePiece(currentPiece, out boardPieceToMoveTo, whoseTurnIsIt);
                    Debug.Log("Test 1");
                    if (boardPieces[boardPieceToMoveTo].gameObject.GetComponent<WhichSquareWasClicked>().isBoardFull())
                    {
                        Debug.Log("Test 4");
                        boardPieceToMoveTo = currentBoardPiece;
                    }
                    gameObject.GetComponent<CheckForWin>().CheckIfWin(currentBoardPiece, whoseTurnIsIt);
                    boardPieces[boardPieceToMoveTo].gameObject.GetComponent<WhichSquareWasClicked>().ChangeBackBoardColor(true);
                    turnNumber++;
                    turnNumberText.text = "Turn Number: " + turnNumber;
                    currentBoardPiece = boardPieceToMoveTo;
                }
            }
        }
    }
    /*
    private void Temporary()
    {

        Debug.Log("Click");
        if (Physics.Raycast(placingray, out hit, Mathf.Infinity, squareLayerMask))
        {
            test1 = hit.transform.parent.transform.parent.gameObject;
            test2 = boardPieces[boardPieceToMoveTo];
            //hit.transform.gameObject.GetComponentInParent<Transform>().gameObject.GetComponentInParent<Transform>().gameObject
            if (hit.transform.parent.transform.parent.gameObject == boardPieces[boardPieceToMoveTo] || boardPieceToMoveTo == 0)
            {
                if (!hit.transform.gameObject.GetComponent<PiecePlaced>().IsPiecePlaced())
                {
                    string whoseTurnIsIt;
                    Debug.Log("Should Find Ray");
                    xTurnToPlace = !xTurnToPlace;
                    if (xTurnToPlace)
                    {
                        currentPiece = xPiece;
                        whoseTurnIsIt = "X";
                    }
                    else
                    {
                        currentPiece = oPiece;
                        whoseTurnIsIt = "O";
                    }
                    turnText.text = "Turn: " + whoseTurnIsIt;

                    boardPieces[boardPieceToMoveTo].gameObject.GetComponent<WhichSquareWasClicked>().ChangeBackBoardColor(false);
                    hit.transform.gameObject.GetComponent<PiecePlaced>().PlacePiece(currentPiece, out boardPieceToMoveTo, whoseTurnIsIt);
                    boardPieces[boardPieceToMoveTo].gameObject.GetComponent<WhichSquareWasClicked>().ChangeBackBoardColor(true);
                    turnNumber++;
                    turnNumberText.text = "Turn Number: " + turnNumber;
                }
            }
        }
    }
    */
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Temporary();
        //}
        mousePos = Input.mousePosition;
        mousePos.z = 32;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.y = -32;
        rayDir = (mouseWorldPos * 50) - Camera.main.transform.position;
        placingray = new Ray(Camera.main.transform.position, rayDir);
        Debug.DrawLine(Camera.main.transform.position, rayDir, color:Color.red);
        if (Physics.Raycast(placingray, out hit, Mathf.Infinity, squareLayerMask))
        {
            if (hit.transform != null)
            {

                if (hit.transform.gameObject != selectedSquare)
                {
                    if (selectedSquare != null)
                    {
                        selectedSquare.GetComponent<PiecePlaced>().HideAllowability();
                    }
                    selectedSquare = hit.transform.gameObject;
                    selectedSquare.GetComponent<PiecePlaced>().ShowAllowability();
                }
            }
        }
        else
        {
            if (selectedSquare != null)
            {
                selectedSquare.GetComponent<PiecePlaced>().HideAllowability();
                selectedSquare = null;
            }
        }
    }
}
