using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichSquareWasClicked : MonoBehaviour
{
    [SerializeField] private List<GameObject> squares = new List<GameObject>();
    [SerializeField] private GameObject backBoard;
    [SerializeField] private Material green;
    [SerializeField] private string whatPieceHasWonTheBoard;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private bool boardIsWon = false;
    [SerializeField] private GameObject[] boardBorders;
    [SerializeField] PiecePlaced[] piecePlaceds;


    public void ChangeBackBoardColor(bool active)
    {
        if (active)
        {
            backBoard.GetComponent<MeshRenderer>().material = green;
        }
        else
        {
            backBoard.GetComponent <MeshRenderer>().material = defaultMaterial;
        }
    }

    public bool isBoardFull()
    {
        Debug.Log("Test 2");
        piecePlaceds = gameObject.GetComponentsInChildren<PiecePlaced>();
        Debug.Log("Test 3");
        for (int i = 0; i < piecePlaceds.Length; i++)
        {
            if (!piecePlaceds[i].IsPiecePlaced())
            {
                Debug.Log("Return False");
                return false;
            }
        }
        return true;
    }

    public List<GameObject> GetSquares()
    {
        return squares;
    }
    public void ChangeToWon(string whatPieceWonTheBoard)
    {
        boardIsWon = true;
        whatPieceHasWonTheBoard = whatPieceWonTheBoard;
    }
    public bool HasBoardBeenWonBefore()
    {
        return boardIsWon;
    }

    public string WhatPieceHasWonTheBoard()
    {
        return whatPieceHasWonTheBoard;
    }
    public void TurnBoardBordersToVictoryColor(Material winningColor)
    {
        for (int i = 0; i < boardBorders.Length; i++)
        {
            boardBorders[i].gameObject.GetComponent<MeshRenderer>().material = winningColor;
        }
    }
}
