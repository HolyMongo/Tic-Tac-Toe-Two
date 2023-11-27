using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacePieces : MonoBehaviour
{
    [SerializeField] private GameObject xPiece;
    [SerializeField] private GameObject oPiece;
    [SerializeField] private GameObject currentPiece;

    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private TextMeshProUGUI turnText;


    [SerializeField] private List<GameObject> boardPieces;
    [SerializeField] private int boardPieceToMoveTo;

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

    private void PlacePiece(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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

                    Debug.Log("Should Find Ray");
                    xTurnToPlace = !xTurnToPlace;
                    if (xTurnToPlace)
                    {
                        currentPiece = xPiece;
                        turnText.text = "Turn: X";
                    }
                    else
                    {
                        currentPiece = oPiece;
                        turnText.text = "Turn: O";
                    }
                    boardPieces[boardPieceToMoveTo].gameObject.GetComponent<WhichSquareWasClicked>().ChangeBackBoardColor(false);
                    hit.transform.gameObject.GetComponent<PiecePlaced>().PlacePiece(currentPiece, out boardPieceToMoveTo);
                    boardPieces[boardPieceToMoveTo].gameObject.GetComponent<WhichSquareWasClicked>().ChangeBackBoardColor(true);
                    turnNumber++;
                    turnNumberText.text = "Turn Number: " + turnNumber;
                }
            }
        }
    }
    private void Temporary()
    {
        Debug.Log("Click");
        if (Physics.Raycast(placingray, out hit, Mathf.Infinity, squareLayerMask))
        {
            if (!hit.transform.gameObject.GetComponent<PiecePlaced>().IsPiecePlaced())
            {
                Debug.Log("Should Find Ray");
                xTurnToPlace = !xTurnToPlace;
                if (xTurnToPlace)
                {
                    currentPiece = xPiece;
                    turnText.text = "Turn: X";
                }
                else
                {
                    currentPiece = oPiece;
                    turnText.text = "Turn: O";
                }
                hit.transform.gameObject.GetComponent<PiecePlaced>().PlacePiece(currentPiece, out boardPieceToMoveTo);
                boardPieces[boardPieceToMoveTo].gameObject.GetComponent<WhichSquareWasClicked>().ChangeBackBoardColor(true);
                turnNumber++;
                turnNumberText.text = "Turn Number: " + turnNumber;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Temporary();
        }
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
