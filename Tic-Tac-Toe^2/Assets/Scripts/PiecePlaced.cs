using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePlaced : MonoBehaviour
{
    [SerializeField] private bool hasPiecePlaced = false;
    [SerializeField] private string whichPieceIsPlaced;

    [SerializeField] private Material allowed;
    [SerializeField] private Material notAllowed;

    public void PlacePiece(GameObject piece, out int nextBoard, string whatPieceIsPlaced)
    {
        whichPieceIsPlaced = whatPieceIsPlaced;
        nextBoard = 0;
        if (!hasPiecePlaced)
        {
            gameObject.GetComponent<MeshRenderer>().material = notAllowed;
            hasPiecePlaced = true;
            AddPieceToBoard(piece);
            if (gameObject.GetComponentInParent<Transform>().gameObject.GetComponentInParent<WhichSquareWasClicked>())
            {
                WhichSquareWasClicked whichSquare = gameObject.GetComponentInParent<Transform>().gameObject.GetComponentInParent<WhichSquareWasClicked>();
                for (int i = 0; i < whichSquare.GetSquares().Count; i++)
                {
                    if (gameObject == whichSquare.GetSquares()[i])
                    {
                        nextBoard = i;
                    }
                }
            }
        }
    }

    public string WhatPieceIsPlaced()
    {
        return whichPieceIsPlaced;
    }
    public bool IsPiecePlaced()
    {
        return hasPiecePlaced;
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
        //GameObject pieceToPlace = piece;
        //Object.Instantiate(pieceToPlace);
        //pieceToPlace.transform.parent = gameObject.transform;
        //Debug.Log("After Setting Parent");
        //pieceToPlace.transform.localScale = Vector3.one;
        Object.Instantiate(piece, gameObject.transform);
    }
}
