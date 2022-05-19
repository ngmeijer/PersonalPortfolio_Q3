using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    public Vector3 position;
    public bool HasEnemy;

    private void Start()
    {
        position = transform.position;
    }
}