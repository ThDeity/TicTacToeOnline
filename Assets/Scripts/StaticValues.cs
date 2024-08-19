using UnityEngine;
using Mirror;

public class StaticValues : NetworkBehaviour
{
    public static StaticValues val;
    public static CheckWin Check;
    public static bool IsWinX, IsWinO;
    [SyncVar] public bool IsX, IsXTurn;

    public void Awake()
    {
        val = GetComponent<StaticValues>();
        Check = FindObjectOfType<CheckWin>();
    }

    public bool GetTurn() { return IsXTurn; }

    public bool GetIsX() { return IsX; }
}
