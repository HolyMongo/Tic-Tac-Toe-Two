using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePieces : MonoBehaviour
{
    [SerializeField] private GameObject xPiece;
    [SerializeField] private GameObject oPiece;
    [SerializeField] private GameObject currentPiece;


    [SerializeField] private List<GameObject> boardPieces;
    private int boardPieceToMoveTo;

    private bool xTurnToPlace;
    private int turnNumber;

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
        inputActions.InGame.Enable();
        inputActions.InGame.PlacePiece.performed += PlacePiece;
    }

    private void PlacePiece(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        /*
        float radius = 0.5f;
        selectedSquare = Physics.OverlapSphere(mouseWorldPos, radius, squareLayerMask);
        Debug.Log("test!");
        if (selectedSquare[0] != null)
        {
            Debug.Log("test2!");
            xTurnToPlace = !xTurnToPlace;
            if (xTurnToPlace)
            {
                currentPiece = xPiece;
            }
            else
            {
                currentPiece = oPiece;
            }
            selectedSquare[0].gameObject.GetComponent<PiecePlaced>().PlacePiece(currentPiece);
        }
        */
        Debug.Log("Click");
        if (Physics.Raycast(placingray, out hit, Mathf.Infinity, squareLayerMask))
        {
            Debug.Log("Should Find Ray");
            xTurnToPlace = !xTurnToPlace;
            if (xTurnToPlace)
            {
                currentPiece = xPiece;
            }
            else
            {
                currentPiece = oPiece;
            }
            hit.transform.gameObject.GetComponent<PiecePlaced>().PlacePiece(currentPiece);
        }
    }

    void Update()
    {
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
            Debug.Log("Should unselect square");
            if (selectedSquare != null)
            {
                selectedSquare.GetComponent<PiecePlaced>().HideAllowability();
                selectedSquare = null;
            }
        }
    }
}
