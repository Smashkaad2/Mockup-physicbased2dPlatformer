using UnityEngine; // Esta vaina es lo que me da acceso a todas las librerias que estoy utilizando para fisicas y demas
using UnityEngine.InputSystem; 

public class Player : MonoBehaviour // Para poder usar funciones de unity como start y update, se debe heredar de la clase MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 5f;
    public float jumpForce = 10f; // Variable que permite controlar la fuerza del salto del jugador
    public float acceleration = 7f; // Variable que permite controlar la aceleracion del jugador
    public float deceleration = 5f; // Variable que permite controlar la desaceleracion del jugador
    public Rigidbody2D rb; // Almacenar rigibody en el jugador que permite ponerlo en el motor de fisicas 2D
    public Vector2 moveInput; // 

    void Start() // El start se ejeucuta al momento que el objeto inicia
    {
        rb = GetComponent<Rigidbody2D>(); //Busca asignarle rigibody al jugadro asegurandosse de que el objeto de este lo tenga, si no es asi explota el juego
    }

    // Update is called once per frame
    void Update() //actualiza cada frame el input recibido
    {   
        float targetSpeed = moveInput.x * moveSpeed; // Velocidad esperada, move input es la direcciòn del input y movespeed es la velocidad que pusimos al inicio a la que se movera el objeto jugador 
        float rate = Mathf.Abs(moveInput.x) > 0.01f ? acceleration : deceleration; // Si el input es mayor a 0.01, se aplica la aceleracion, si no, se aplica la desaceleracion
        float newX = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, rate * Time.deltaTime); // Calcula la nueva velocidad en X, moviendose hacia la velocidad objetivo a una tasa de aceleracion o desaceleracion dependiendo del input
         rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y); 

         if(Keyboard.current.spaceKey.wasPressedThisFrame){ // Si se presiona la tecla espacio, se aplica una fuerza hacia arriba al rigibody del jugador
             rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
         }

         Debug.Log($"input.x={moveInput.x:F2} | target={targetSpeed:F2} | rate={rate:F2} | velX(antes)={rb.linearVelocity.x:F2} | newX={newX:F2} | moveSpeed={moveSpeed} accel={acceleration} decel={deceleration}");
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
