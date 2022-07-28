using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector3 direction;
    [Header("Speed is in seconds")]
    public float speed = 1;
    public float step = 2;
    Vector3 location;
    public Vector2Int position;
    private bool moving = false;
    private Controls controls;
    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();
        controls.Player.Enable();

        controls.Player.Up.performed += GoUp;
        controls.Player.Left.performed += GoLeft;
        controls.Player.Right.performed += GoRight;
        controls.Player.Down.performed += GoDown;
    }

    public void GoUp(InputAction.CallbackContext ctx)
    {
        TryMove(Vector2Int.up);
    }
    public void GoLeft(InputAction.CallbackContext ctx)
    {
        TryMove(Vector2Int.left);
    }
    public void GoRight(InputAction.CallbackContext ctx)
    {
        TryMove(Vector2Int.right);
    }
    public void GoDown(InputAction.CallbackContext ctx)
    {
        TryMove(Vector2Int.down);
    }

    public void TryMove(Vector2Int dir)
    {
        if (!moving)
        {
            Vector2Int desired = position + dir;
            if (Level.get.OnRoad(desired.x, desired.y))
            {
                direction = new Vector3(dir.x, 0, dir.y);
                StartCoroutine(Move(direction));
            }
        }
    }

    public IEnumerator Move(Vector3 direction)
    {
        moving = true;
        Vector3 start = transform.position;
        Vector3 finish = transform.position + direction * step;

        float time = 0;

        while (time<speed)
        {
            Vector3 pos = Vector3.Lerp(start, finish, time / speed);
            transform.position = pos;
            time += Time.deltaTime;
            yield return null;
        }

        moving = false;
        transform.position = finish;
    }


}
