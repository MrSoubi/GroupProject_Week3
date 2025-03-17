using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RSF_AccessNextTile rsfAccessNextTile;

    private Vector2 _position;

    [Header("References")]
    [SerializeField] PlayerGhostPowerUp ghostPowerUp;

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
        _position = (Vector2Int)position;
        rseOnPlayerMove.Call(position);
    }

    public void Move(Vector2 direction)
    {
        TilemapData.TileType tileDesireType = rsfAccessNextTile.Call(_position + direction);
        if (tileDesireType != TilemapData.TileType.None)
        {
            if (tileDesireType == TilemapData.TileType.Wall && !ghostPowerUp.IsActive()) return;

            _position += direction;
            Vector3Int newPosition = new Vector3Int(Mathf.RoundToInt(_position.x), Mathf.RoundToInt(_position.y), Mathf.RoundToInt(transform.position.z));

            transform.position = newPosition;
            rseOnPlayerMove.Call(newPosition);
        }
    }
}