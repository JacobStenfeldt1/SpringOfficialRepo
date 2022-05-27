using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public CharacterController controller;
    public Transform cam; // genom att referera till kameran kan vi använda dess euler vinklar för att beräkna hilken vilken som spelaren ska vandrai
    public Transform groundCheck;

    public LayerMask groundmask; 

    public float TurnSmothTime = 0.1f;
    public float TurnSmothvelocity;
    public float speed = 6f; // Float är enbart en annan datatyp 
    public float jumpspeed  = 3f;
    public float gravity = -9.82f;
    public float groundDistance = 0.2f;

    private Vector3 velocity; // Hur mycket den förändras vertikalt. Vad har vi för hastighet i nuläget? 
    private bool isGrounded; //bectämmer om vi ska applicera vertikal rörelse baserat på om vi står på marken


    // Update is called once per frame
    void Update()
    {
        //Vad är is grounded '
        animator = GetComponent<Animator>();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask); // När "is grounded är tru" kollar vi vilken position spelaren har i relation till marken, 
        // Vi kollar inom ett område på 0.2 längd enheter. Alltå så måste marken befinna sig inom 0.2 l.e för att beräknas som grounded
        //Sist kollar vi om det vi kolliserar med ingår i vår groundmask [allt som är mark går med i vår ground mask]
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f; // detta ser till att hastigheteh i y led saktas ner lite jämnare
        }
        
        float horizontal = Input.GetAxisRaw("Horizontal"); // Går mellan W/S -1 och 1
        float vertical = Input.GetAxisRaw("Vertical"); // Går mellan -1 och 1 A/D
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);// Glömde = missta
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Normalized, gör att player inte rör sig snabbare om man håller ner två knappar samtidigt. 


        if (direction.magnitude >= 0.1f) // Om längden av vår riktnings vektor >= 0.1 kommer vi röra oss i den riktningen Här lastar du in Animation transitions 
        {
            animator.SetBool("IsMoving", true);

            float TargetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;// Vi lägger till kameras y rotation så att spelaren kollar ditt  //" hur mycket ska vi röra oss på y axeln för att titta i rätt rikning" Co Tangens för vektorn anver åt vilket håll spelaren ska titta baserat på om vektorn pekar rakt fram eller åt en vinkel
            // vektorn pelar åt sama håll som dess adderade / subtraherade komposanter(komposant uppdelning) aå spelaren följker helt enkelt vilken knapp du trycker på och vänder sig ditåt 
            // Det är en stor enhetscirkel där A och D motsvarar posetiva / negativa x axelen samt WS motsvarar posetiva / negativa y axelen. Där emelan kan man skapa en vektor beroende på tangens värde (Mot/när)
            // Rad 2 degres konverterar radianer till grader 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y , TargetAngle, ref TurnSmothvelocity, TurnSmothTime); // Vi kollar på vilken euler vinkel vi har nu, sedan kollarvi på vilken vinkel vi vill nå, där efter vilken hastighet som vi ska justera vinkeln med (w), sedan kollar vi på hur lång tid denna förädning får ta

            transform.rotation = Quaternion.Euler (0f, angle, 0f); // Gubben vänder sig i relation till rotationen, Quaternions är punkter och rotationer i 4D space sär vi får en vinkel som korresponderar med en punkt på en hypersvär. Det vi sedan gör ar att vi enbart kollar på dess porition i 3D genom eulder ekvationen. Detta gör att vi undviker gible lock. 
            Vector3 movedir = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward; // har z koordinaten 1. När man har quaternions som är rotatiojner och vill ha en riktning, mulltiplicerar man med vector 3(0,0,1)  

            controller.Move(movedir.normalized * speed * Time.deltaTime); // Time . delta time gör det oberoende på framerate man måste normalizera dels för att vi rör oss, men även för att inte spä på rörelsen pga komposant beräkning 

        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        // Lägger till hopp
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
           velocity.y = Mathf.Sqrt(jumpspeed * -0.2f * gravity);
        }
        //Vad händer om ovanstående ifsats inte gäller. Då kommer den att applicera en vektor med samma magnitud som gravitationen på karaktären vilket ddrar den neråt. 
        velocity.y += gravity * Time.deltaTime; //time.deltatime = framerate indipendent. 
        controller.Move(velocity * Time.deltaTime);
    }
}
