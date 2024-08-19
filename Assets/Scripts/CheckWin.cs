using Mirror;
using UnityEngine;

public class CheckWin : NetworkBehaviour
{
    [SerializeField] private Transform[] _slots = new Transform[9];

    [SyncVar] public bool isXWin = false, isOWin = false;

    public void Check()
    {
        for (int i = 0; i < _slots.Length; i += 3)
        {
            if (_slots[i].childCount > 0 && _slots[i + 1].childCount > 0 && _slots[i + 2].childCount > 0)
            {
                if (_slots[i].GetChild(0).tag == "X" && _slots[i + 1].GetChild(0).tag == "X" && _slots[i + 2].GetChild(0).tag == "X")
                {
                    Debug.Log("horizontal X");
                    isXWin = true;
                    break;
                }
                else if (_slots[i].GetChild(0).tag == "O" && _slots[i + 1].GetChild(0).tag == "O" && _slots[i + 2].GetChild(0).tag == "O")
                {
                    Debug.Log("vertical O");
                    isOWin = true;
                    break;
                }
            }
        }

        if (!isOWin && !isXWin)
        {
            for (int i = 0; i < 3; i++)
            {
                if (_slots[i].childCount > 0 && _slots[i + 3].childCount > 0 && _slots[i + 6].childCount > 0)
                {
                    if (_slots[i].GetChild(0).tag == "X" && _slots[i + 3].GetChild(0).tag == "X" && _slots[i + 6].GetChild(0).tag == "X")
                    {
                        Debug.Log("vertical X");
                        isXWin = true;
                        break;
                    }
                    else if (_slots[i].GetChild(0).tag == "O" && _slots[i + 3].GetChild(0).tag == "O" && _slots[i + 6].GetChild(0).tag == "O")
                    {
                        Debug.Log("vertical O");
                        isOWin = true;
                        break;
                    }
                }
            }

            if (!isOWin && !isXWin)
            {
                if (_slots[0].childCount > 0 && _slots[4].childCount > 0 && _slots[8].childCount > 0)
                {
                    if (_slots[0].GetChild(0).tag == "X" && _slots[4].GetChild(0).tag == "X" && _slots[8].GetChild(0).tag == "X")
                        isXWin = true;
                    else if (_slots[0].GetChild(0).tag == "O" && _slots[4].GetChild(0).tag == "O" && _slots[8].GetChild(0).tag == "O")
                        isOWin = true;
                }
                
                if (_slots[2].childCount > 0 && _slots[4].childCount > 0 && _slots[6].childCount > 0)
                {
                    if (_slots[2].GetChild(0).tag == "X" && _slots[4].GetChild(0).tag == "X" && _slots[6].GetChild(0).tag == "X")
                        isXWin = true;
                    else if (_slots[2].GetChild(0).tag == "O" && _slots[4].GetChild(0).tag == "O" && _slots[6].GetChild(0).tag == "O")
                        isOWin = true;
                }
            }
        }

        if (isOWin == true || isXWin == true)
            SetScene();
    }

    [Server]
    private void SetScene()
    {
        StaticValues.IsWinO = isOWin;
        StaticValues.IsWinX = isXWin;
        NetworkManager manager = FindObjectOfType<NetworkManager>();
        manager.ServerChangeScene("Win");
    }
}
