using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RSF_AccessNextTile rsfAccessNextTile;

    private Vector2 _position;
    
    public void Move(Vector2 direction)
    {
        if (true || rsfAccessNextTile.Call(_position + direction))
        {
            _position += direction;
            transform.position = new Vector3(_position.x, _position.y, transform.position.z);
        }
    }
}