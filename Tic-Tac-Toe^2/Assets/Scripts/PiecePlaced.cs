using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePlaced : MonoBehaviour
{
    private bool hasPiecePlaced = false;

    [SerializeField] private Material allowed;
    [SerializeField] private Material notAllowed;

    public void PlacePiece(GameObject piece)
    {
        if (!hasPiecePlaced)
        {
            gameObject.GetComponent<MeshRenderer>().material = notAllowed;
            hasPiecePlaced = true;
            AddPieceToBoard(piece);
        }
    }

    public void ShowAllowability()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void HideAllowability()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void AddPieceToBoard(GameObject piece)
    {
        Object.Instantiate(piece, gameObject.transform);
    }
}
