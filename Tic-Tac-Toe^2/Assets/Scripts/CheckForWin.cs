using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForWin : MonoBehaviour
{

    [SerializeField] private List<GameObject> boardPieces;
    [SerializeField] PiecePlaced[] squares;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    // Start is called before the first frame update
    void Start()
    {
        boardPieces = gameObject.GetComponent<PlacePieces>().ShareBoardPieces();
    }

    public void CheckIfWin(int currentBoardPiece, string playerSymbol)
    {
        if (PossibleWin(playerSymbol, currentBoardPiece))
        {
            WhichSquareWasClicked whichSquare = boardPieces[currentBoardPiece].GetComponent<WhichSquareWasClicked>();
            //change the color of borders between the squares and change a bool to true so that a square can´t be won twice or thrice or fries???
            if (!whichSquare.HasBoardBeenWonBefore())
            {
                whichSquare.ChangeToWon(playerSymbol);
                Debug.Log("THREE IN A ROW!");
                if (playerSymbol == "X")
                {
                    whichSquare.TurnBoardBordersToVictoryColor(red);
                }
                else
                {
                    whichSquare.TurnBoardBordersToVictoryColor(blue);
                }
                if (PossibleWinWith3WinsInARow(playerSymbol))
                {
                    Debug.Log(playerSymbol + " Has won the match");
                    if (playerSymbol == "X")
                    {
                        gameObject.GetComponent<PlacePieces>().ChangeBoardBoardersToVictoryColor(red);
                    }
                    else
                    {
                        gameObject.GetComponent<PlacePieces>().ChangeBoardBoardersToVictoryColor(blue);
                    }
                }
            }

        }
    }

    private bool PossibleWin(string playerSymbol, int currentBoardPiece)
    {
        squares = boardPieces[currentBoardPiece].GetComponentsInChildren<PiecePlaced>();

        //Horizontally
        if (squares[0].WhatPieceIsPlaced() == playerSymbol && squares[1].WhatPieceIsPlaced() == playerSymbol && squares[2].WhatPieceIsPlaced() == playerSymbol)
        {
            return true; 
        }
        else if (squares[3].WhatPieceIsPlaced() == playerSymbol && squares[4].WhatPieceIsPlaced() == playerSymbol && squares[5].WhatPieceIsPlaced() == playerSymbol)
        {
            return true;
        }
        else if (squares[6].WhatPieceIsPlaced() == playerSymbol && squares[7].WhatPieceIsPlaced() == playerSymbol && squares[8].WhatPieceIsPlaced() == playerSymbol)
        {
            return true;
        }//Vertically
        else if (squares[0].WhatPieceIsPlaced() == playerSymbol && squares[3].WhatPieceIsPlaced() == playerSymbol && squares[6].WhatPieceIsPlaced() == playerSymbol)
        {
            return true;
        }
        else if (squares[1].WhatPieceIsPlaced() == playerSymbol && squares[4].WhatPieceIsPlaced() == playerSymbol && squares[7].WhatPieceIsPlaced() == playerSymbol)
        {
            return true;
        }
        else if (squares[2].WhatPieceIsPlaced() == playerSymbol && squares[5].WhatPieceIsPlaced() == playerSymbol && squares[8].WhatPieceIsPlaced() == playerSymbol)
        {
            return true;
        }//Diagonally
        else if (squares[0].WhatPieceIsPlaced() == playerSymbol && squares[4].WhatPieceIsPlaced() == playerSymbol && squares[8].WhatPieceIsPlaced() == playerSymbol)
        {
            return true;
        }
        else if (squares[2].WhatPieceIsPlaced() == playerSymbol && squares[4].WhatPieceIsPlaced() == playerSymbol && squares[6].WhatPieceIsPlaced() == playerSymbol)
        {
            return true;
        }

        return false;
    }

    private bool PossibleWinWith3WinsInARow(string playerSymbol)
    {
        bool[] hasBeenWon = new bool[9];
        WhichSquareWasClicked[] whichSquareWasClickeds = new WhichSquareWasClicked[9];
        for (int i = 0; i < boardPieces.Count; i++)
        {
            whichSquareWasClickeds[i] = boardPieces[i].GetComponent<WhichSquareWasClicked>();
        }
        for (int i = 0; i < boardPieces.Count; i++)
        {
            hasBeenWon[i] = whichSquareWasClickeds[i].HasBoardBeenWonBefore();
        }




        //Horizontal
        if (whichSquareWasClickeds[0].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[1].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[2].WhatPieceHasWonTheBoard() == playerSymbol)
        {
            return true;
        }
        else if (whichSquareWasClickeds[3].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[4].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[5].WhatPieceHasWonTheBoard() == playerSymbol)
        {
            return true;
        }
        else if (whichSquareWasClickeds[6].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[7].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[8].WhatPieceHasWonTheBoard() == playerSymbol)
        {
            return true;
        }//Vertical
        else if (whichSquareWasClickeds[0].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[3].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[6].WhatPieceHasWonTheBoard() == playerSymbol)
        {
            return true;
        }
        else if (whichSquareWasClickeds[1].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[4].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[7].WhatPieceHasWonTheBoard() == playerSymbol)
        {
            return true;
        }
        else if (whichSquareWasClickeds[2].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[5].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[8].WhatPieceHasWonTheBoard() == playerSymbol)
        {
            return true;
        }//Diagonal
        else if (whichSquareWasClickeds[0].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[4].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[8].WhatPieceHasWonTheBoard() == playerSymbol)
        {
            return true;
        }
        else if (whichSquareWasClickeds[2].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[4].WhatPieceHasWonTheBoard() == playerSymbol && whichSquareWasClickeds[6].WhatPieceHasWonTheBoard() == playerSymbol)
        {
            return true;
        }

        return false;
    }
}
