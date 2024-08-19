using UnityEngine;
using Mirror;

public class XorO : NetworkBehaviour
{
    [ClientRpc]
    public void GetParent(Transform parent, int index)
    {
        if (index == 0)
            GetComponent<SpriteRenderer>().color = Color.cyan;

        transform.parent = parent;
        transform.localPosition = Vector2.zero;
        StaticValues.Check.Check();
    }

    [ClientRpc]
    public void BecomeCyan() => GetComponent<SpriteRenderer>().color = Color.cyan;
}
