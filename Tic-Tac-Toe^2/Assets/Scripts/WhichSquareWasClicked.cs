using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichSquareWasClicked : MonoBehaviour
{
    [SerializeField] private List<GameObject> squares = new List<GameObject>();
    [SerializeField] private GameObject backBoard;
    [SerializeField] private Material green;
    [SerializeField] private Material defaultMaterial;
   
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

    public List<GameObject> GetSquares()
    {
        return squares;
    }
}
