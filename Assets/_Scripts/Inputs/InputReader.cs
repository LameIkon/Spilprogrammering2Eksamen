using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
public sealed class InputReader : ScriptableObject
{

    [SerializeField] private KeyCode _up = KeyCode.W;
    [SerializeField] private KeyCode _down = KeyCode.S;
    [SerializeField] private KeyCode _left = KeyCode.A;
    [SerializeField] private KeyCode _right = KeyCode.D;


    public event Action<Vector2> _OnMoveEvent;

    public Vector2 Move()
    {
        Vector2 dir;

        if (Input.GetKey(_left) && Input.GetKey(_right))
        {
            dir.x = 0;
        }
        else if (Input.GetKey(_right))
        {
            dir.x = 1;
        }
        else if (Input.GetKey(_left))
        {
            dir.x = -1;
        }
        else 
        {
            dir.x = 0;
        }

        if (Input.GetKey(_up) && Input.GetKey(_down))
        {
            dir.y = 0;
        }
        else if (Input.GetKey(_up))
        {
            dir.y = 1;
        }
        else if (Input.GetKey(_down))
        {
            dir.y = -1;
        }
        else 
        {
            dir.y = 0;
        }


        return dir;
    }

}
