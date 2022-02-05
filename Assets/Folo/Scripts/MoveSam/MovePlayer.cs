using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private LayerMask onGround;//слой для прыжка

    [SerializeField] private Transform checkPointGround;//обьект который определяет землю

    [SerializeField] private float speed;//начальная скорость 

    [SerializeField] private float jampForse;//сила которая подбрасывает игрока
    [SerializeField] private float groundRadius;// радиус для определения земли

    private bool rightFace;

    private float moveInput;//показатель направления(вправо влево)


    private Rigidbody2D rd;//физика 2д

    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        rd.velocity = new Vector2(moveInput * speed, rd.velocity.y);

        if (moveInput > 0 && rightFace)
            Flip();
        else if (moveInput < 0 && !rightFace)
            Flip();
    }

    private void FixedUpdate()
    {
        if (OnGroundCheck() && Input.GetKey(KeyCode.Space))
        {
            rd.AddForce(Vector2.up * jampForse, ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        rightFace = !rightFace;
        transform.Rotate(0, 180, 0);
    }

    /// <summary>
    ///метод определяюший землю под ногами для прыжка 
    /// </summary>
    /// <returns></returns>
    private bool OnGroundCheck()
    {
        return Physics2D.OverlapCircle(checkPointGround.position, groundRadius, onGround);
    }
}