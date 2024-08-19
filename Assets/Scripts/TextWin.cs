using UnityEngine;
using UnityEngine.UI;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class TextWin : NetworkBehaviour
{
    [SerializeField] private Text _text;

    public void Start()
    {
        if (StaticValues.IsWinX)
            _text.text = "X is a Winner!";
        else
            _text.text = "O is a Winner!";
    }
}
