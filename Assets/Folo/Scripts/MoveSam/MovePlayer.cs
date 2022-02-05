using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private LayerMask onGround;//слой для прыжка

    [SerializeField] private Transform checkPointGround;//обьект который определяет землю


    [SerializeField] private float startSpeed;//начальная скорость 
    [SerializeField] private float nextSpeed;// конечная скорость

    [SerializeField] private float startTimer;//время за которое игрок ускоряется 

    [SerializeField] private float jampForse;//сила которая подбрасывает игрока
    [SerializeField] private float groundRadius;// радиус для определения земли

    private bool rightFace;

    private float moveInput;//показатель направления(вправо влево)
    private float speed;//обшая скорость
    private float addSpeed;//ускорение

    private float timer;//изменяемое время


    private Rigidbody2D rd;//физика 2д

    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        timer = startTimer;
        addSpeed = (nextSpeed - startSpeed) / startTimer;
    }

    private void Update()
    {
        rd.velocity = new Vector2(NewSpeed(startTimer, nextSpeed, startSpeed), rd.velocity.y);

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

    /// <summary> 
    ///метод для получения обшей скорости со всеми изменениями
    /// </summary>
    /// <param name="timerStart"></param> стартовое время для разгона
    /// <param name="maxSpeed"></param> максимально доступная скорость, развииваемая игроком к конце таймера
    /// <param name="startSpeed"></param> начальная скорость с которой начинается ускорение
    /// <returns></returns>
    public float NewSpeed(float timerStart, float maxSpeed, float startSpeed)
    {
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput == 0)
        {
            timer = timerStart;
            speed = startSpeed;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (speed < maxSpeed)
            {
                speed += addSpeed * Time.deltaTime;
            }
            else
            {
                speed = maxSpeed;
            }
        }
        else
        {
            speed = maxSpeed;
        }

        return speed * moveInput;
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