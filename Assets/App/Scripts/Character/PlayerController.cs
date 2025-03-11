using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RSF_AccessNextTile rsfAccessNextTile;

    private Vector2Int _position;
    
    public void Move(Vector2Int direction)
    {
        if (rsfAccessNextTile.Call(_position + direction))
        {
            _position += direction;
            transform.position = new Vector3(_position.x, _position.y, transform.position.z);
        }
    }
}