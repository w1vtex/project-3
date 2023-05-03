using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5.0f; // Скорость передвижения объекта
    [SerializeField] private float jumpForce = 10.0f; // высота прыжка
    [SerializeField] private float horizontalMoveImpulse = 2.0f; // сила спринта  
    [SerializeField] private bool isFly = false;
    [SerializeField] private float hp = 100; 
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource kickEnemySound;

    [SerializeField] // для визуального показа в инпекторе
    private Vector2 inputMoveDirection;

    private bool isGround = false;


    private void Update()// Update вызывается один раз за кадр
    {
        InputDirection();// получаем значение ввода с клавиатуры 

        Vector2 moveDirection = isFly ? inputMoveDirection : Vector2.right * inputMoveDirection.x;
        MoveNotPhisic2D(moveDirection);

        Jump();
        HorizontalMoveImpulse();
        SetGravitation(!isFly);
    }

    private void InputDirection()
    {
        inputMoveDirection.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // Передвижение объекта по оси X с использованием клавиш "A" и "D"
        inputMoveDirection.y = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // Передвижение объекта по оси Z с использованием клавиш "W" и "S"
    }

    private void MoveNotPhisic2D(Vector2 direction)
    {
        transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);

        // альтернативная запись (можно оставить эту и убрать ту что выше)
        //transform.position += (Vector3)direction;// ровно transform.position = transform.position + (Vector3)direction;
    }

    private void SetGravitation(bool state)
    {
        rb.simulated = state;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            MoveImpulse2D(Vector2.up, jumpForce);
    }

    private void HorizontalMoveImpulse()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector2 inputMoveHorizontal = new Vector2(inputMoveDirection.x, 0);
            MoveImpulse2D(inputMoveHorizontal.normalized, horizontalMoveImpulse);
        }
    }

    private void MoveImpulse2D(Vector2 direction, float force)
    {
        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform")
        {
            isGround = true;
        }

        if(collision.gameObject.tag == "oposum")
        {
            Debug.Log("death");
            hp -= 10;
            Debug.Log(hp);
            kickEnemySound.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform")
        {
            isGround = false;
            jumpSound.Play();
        }
    }
}
