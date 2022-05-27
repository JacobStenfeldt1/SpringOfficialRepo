using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public CharacterController controller;
    public Transform cam; // genom att referera till kameran kan vi anv�nda dess euler vinklar f�r att ber�kna hilken vilken som spelaren ska vandrai
    public Transform groundCheck;

    public LayerMask groundmask; 

    public float TurnSmothTime = 0.1f;
    public float TurnSmothvelocity;
    public float speed = 6f; // Float �r enbart en annan datatyp 
    public float jumpspeed  = 3f;
    public float gravity = -9.82f;
    public float groundDistance = 0.2f;

    private Vector3 velocity; // Hur mycket den f�r�ndras vertikalt. Vad har vi f�r hastighet i nul�get? 
    private bool isGrounded; //bect�mmer om vi ska applicera vertikal r�relse baserat p� om vi st�r p� marken


    // Update is called once per frame
    void Update()
    {
        //Vad �r is grounded '
        animator = GetComponent<Animator>();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask); // N�r "is grounded �r tru" kollar vi vilken position spelaren har i relation till marken, 
        // Vi kollar inom ett omr�de p� 0.2 l�ngd enheter. Allt� s� m�ste marken befinna sig inom 0.2 l.e f�r att ber�knas som grounded
        //Sist kollar vi om det vi kolliserar med ing�r i v�r groundmask [allt som �r mark g�r med i v�r ground mask]
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f; // detta ser till att hastigheteh i y led saktas ner lite j�mnare
        }
        
        float horizontal = Input.GetAxisRaw("Horizontal"); // G�r mellan W/S -1 och 1
        float vertical = Input.GetAxisRaw("Vertical"); // G�r mellan -1 och 1 A/D
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);// Gl�mde = missta
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Normalized, g�r att player inte r�r sig snabbare om man h�ller ner tv� knappar samtidigt. 


        if (direction.magnitude >= 0.1f) // Om l�ngden av v�r riktnings vektor >= 0.1 kommer vi r�ra oss i den riktningen H�r lastar du in Animation transitions 
        {
            animator.SetBool("IsMoving", true);

            float TargetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;// Vi l�gger till kameras y rotation s� att spelaren kollar ditt  //" hur mycket ska vi r�ra oss p� y axeln f�r att titta i r�tt rikning" Co Tangens f�r vektorn anver �t vilket h�ll spelaren ska titta baserat p� om vektorn pekar rakt fram eller �t en vinkel
            // vektorn pelar �t sama h�ll som dess adderade / subtraherade komposanter(komposant uppdelning) a� spelaren f�ljker helt enkelt vilken knapp du trycker p� och v�nder sig dit�t 
            // Det �r en stor enhetscirkel d�r A och D motsvarar posetiva / negativa x axelen samt WS motsvarar posetiva / negativa y axelen. D�r emelan kan man skapa en vektor beroende p� tangens v�rde (Mot/n�r)
            // Rad 2 degres konverterar radianer till grader 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y , TargetAngle, ref TurnSmothvelocity, TurnSmothTime); // Vi kollar p� vilken euler vinkel vi har nu, sedan kollarvi p� vilken vinkel vi vill n�, d�r efter vilken hastighet som vi ska justera vinkeln med (w), sedan kollar vi p� hur l�ng tid denna f�r�dning f�r ta

            transform.rotation = Quaternion.Euler (0f, angle, 0f); // Gubben v�nder sig i relation till rotationen, Quaternions �r punkter och rotationer i 4D space s�r vi f�r en vinkel som korresponderar med en punkt p� en hypersv�r. Det vi sedan g�r ar att vi enbart kollar p� dess porition i 3D genom eulder ekvationen. Detta g�r att vi undviker gible lock. 
            Vector3 movedir = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward; // har z koordinaten 1. N�r man har quaternions som �r rotatiojner och vill ha en riktning, mulltiplicerar man med vector 3(0,0,1)  

            controller.Move(movedir.normalized * speed * Time.deltaTime); // Time . delta time g�r det oberoende p� framerate man m�ste normalizera dels f�r att vi r�r oss, men �ven f�r att inte sp� p� r�relsen pga komposant ber�kning 

        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        // L�gger till hopp
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
           velocity.y = Mathf.Sqrt(jumpspeed * -0.2f * gravity);
        }
        //Vad h�nder om ovanst�ende ifsats inte g�ller. D� kommer den att applicera en vektor med samma magnitud som gravitationen p� karakt�ren vilket ddrar den ner�t. 
        velocity.y += gravity * Time.deltaTime; //time.deltatime = framerate indipendent. 
        controller.Move(velocity * Time.deltaTime);
    }
}
