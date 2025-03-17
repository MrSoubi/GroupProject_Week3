using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RSF_AccessNextTile rsfAccessNextTile;

    private Vector2 _position;

    [Header("Output")]
    [SerializeField] RSE_OnPlayerMove rseOnPlayerMove;

    private void Start()
    {
        Invoke("LateStart", .1f);
    }

    void LateStart()
    {
        Vector3Int position = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));

        transform.position = position;
        rseOnPlayerMove.Call(position);
    }

    public void Move(Vector2 direction)
    {
        if (rsfAccessNextTile.Call(_position + direction))
        {
            _position += direction;
            Vector3Int newPosition = new Vector3Int(Mathf.RoundToInt(_position.x), Mathf.RoundToInt(_position.y), Mathf.RoundToInt(transform.position.z));

            transform.position = newPosition;
            rseOnPlayerMove.Call(newPosition);
        }
    }
}