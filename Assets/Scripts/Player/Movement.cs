using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [HideInInspector] public PlayerInputs playerInputs;//oyuncu girdileri

    public Abilities[] abilities;
    int abilitiesIndex = 0;

    [SerializeField] Animator animator;//silah animator
    [SerializeField] LayerMask layer; //yer katmaný
    [SerializeField] float walkSpeed, runSpeed, jumpPower, gravity;

    CharacterController characterController;//karakter kontrolcüsü

    float speed, velocityY;
    Vector3 velocity;//dikey hýz

    public static Movement movement;//singleton

    private void Awake()
    {
        movement = this;//singleton
        playerInputs = new PlayerInputs();//oyuncu girdileri için o sýnýftan nesne oluþturma
        SetPlayerInput(true);//oyuncu girdilerini aktif etme
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();//bileþen çekme
        speed = walkSpeed;//hýz ayarlama
    }

    void FixedUpdate()
    {
        MovementAndAnim(playerInputs.Player.Move.ReadValue<Vector2>().x, playerInputs.Player.Move.ReadValue<Vector2>().y);
    }
    /// <summary>
    /// koþma
    /// </summary>
    /// <param name="obj"></param>
    void Run(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            speed = runSpeed;
        }
        if (obj.canceled)
        {
            speed = walkSpeed;
        }
    }
    /// <summary>
    /// ateþ etme
    /// </summary>
    /// <param name="obj"></param>
    void Shoot(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            animator.Play("Shoot");
            abilities[abilitiesIndex].UseAbility(Camera.main.transform);
        }
    }
    /// <summary>
    /// zýplama
    /// </summary>
    /// <param name="obj"></param>
    void Jump(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if (GroundCheck())//karakter kontrol bileþenini isGrounded kýsmý istikrarlý çalýþmadýðý için böyle bir kontrol kullandým
            {
                velocityY = 0;
                velocityY += jumpPower;
            }
        }
    }
    /// <summary>
    /// hareket ve animasyonu ayarlar
    /// kameraya göre hareket eder
    /// </summary>
    /// <param name="h">yatay</param>
    /// <param name="v">dikey</param>
    void MovementAndAnim(float h, float v)
    {
        if (characterController.isGrounded)
        {
            velocityY = 0;
        }
        else
        {
            velocityY -= gravity;
        }

        velocity.y = velocityY;
        characterController.Move(velocity * Time.deltaTime);


        Vector3 lookPoint = Camera.main.transform.forward;
        lookPoint.y = 0;

        transform.rotation = Quaternion.LookRotation(lookPoint);
        
        // Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPoint), 10 * Time.deltaTime);
        
        characterController.Move(v * speed * Time.deltaTime * transform.forward);
        characterController.Move(h * speed * Time.deltaTime * transform.right);

        animator.SetFloat("Blend", Mathf.Clamp(Mathf.Sqrt(Mathf.Pow(h, 2) + Mathf.Pow(v, 2)), 0, 1));
    }
    /// <summary>
    /// oyuncu girdilerini ayarlayan metot
    /// </summary>
    /// <param name="enable">enable true olursa oyuncu girdileri aktif olur</param>
    public void SetPlayerInput(bool enable)
    {
        if (enable)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerInputs.Player.Enable();
            playerInputs.Player.Run.performed += Run;
            playerInputs.Player.Run.canceled += Run;
            playerInputs.Player.Shoot.performed += Shoot;
            playerInputs.Player.Jump.performed += Jump;
        }
        else if (playerInputs.Player.enabled == true)
        {
            playerInputs.Player.Jump.performed -= Jump;
            playerInputs.Player.Shoot.performed -= Shoot;
            playerInputs.Player.Run.performed -= Run;
            playerInputs.Player.Run.canceled -= Run;
            Cursor.lockState = CursorLockMode.None;
            playerInputs.Player.Disable();
        }
    }
    /// <summary>
    /// raycast atarak oyuncunun yere deðme durumunu kontrol eder
    /// </summary>
    /// <returns>true ise yere deðiyor manasýna gelir</returns>
    bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1.1f, layer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// disable olduðunda oyuncu girdilerini kapatýr
    /// </summary>
    private void OnDisable()
    {
        SetPlayerInput(false);
    }

}
