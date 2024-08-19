using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class S : NetworkBehaviour
{
    private List<GameObject> _objects = new List<GameObject>();
    [SerializeField] private GameObject X, O;
    public bool _isX, _isMyTurn;

    private void Start()
    {
        if (StaticValues.val.IsX == false)
        {
            _isX = true;
            StaticValues.val.IsX = true;
            StaticValues.val.IsXTurn = true;
        }
    }

    private void Update()
    {
        if (_isX)
            _isMyTurn = StaticValues.val.IsXTurn;
        else
            _isMyTurn = !StaticValues.val.IsXTurn;

        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonDown(0) && _isMyTurn)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.transform.childCount == 0)
                SetRed(hit.collider.gameObject);
        }
    }

    [Command]
    public void SetRed(GameObject obj)
    {
        GameObject projectile = null;
        if (_isX && obj != null)
            projectile = Instantiate(X, obj.transform.position, Quaternion.identity);
        else if (!_isX && obj != null)
            projectile = Instantiate(O, obj.transform.position, Quaternion.identity);

        if (projectile != null)
        {
            NetworkServer.Spawn(projectile);
            _objects.Add(projectile);
            projectile.GetComponent<XorO>().GetParent(obj.transform, _objects.IndexOf(projectile));

            if (_objects.Count > 3)
            {
                GameObject _obj = _objects[0];
                NetworkServer.Destroy(_obj);
                _objects.RemoveAt(0);
                _objects[0].GetComponent<XorO>().BecomeCyan();
            }
        }

        StaticValues.val.IsXTurn = !StaticValues.val.IsXTurn;
    }
}