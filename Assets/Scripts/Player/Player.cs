using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerInputs playerInputs;//oyuncu girdileri
    [HideInInspector] public Health health;
    [HideInInspector] public Energy energy;

    public Abilities[] abilities;
    public int energyRegenerationAmount;
    int abilitiesIndex = 0;

    [SerializeField] Animator animator;//silah animator
    [SerializeField] LayerMask layer; //yer katman�
    [SerializeField] float walkSpeed, runSpeed, jumpPower, gravity;

    CharacterController characterController;//karakter kontrolc�s�

    float speed, velocityY;
    Vector3 velocity;//dikey h�z

    public static Player player;//singleton
    public static Action<int> ChangeAbilities;
    public static Action GameOver;
    private void Awake()
    {
        player = this;//singleton
        playerInputs = new PlayerInputs();//oyuncu girdileri i�in o s�n�ftan nesne olu�turma
        SetPlayerInput(true);//oyuncu girdilerini aktif etme
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();//bile�en �ekme
        health = GetComponent<Health>();
        energy = GetComponent<Energy>();
        speed = walkSpeed;//h�z ayarlama
    }

    void FixedUpdate()
    {
        MovementAndAnim(playerInputs.Player.Move.ReadValue<Vector2>().x, playerInputs.Player.Move.ReadValue<Vector2>().y);
    }
    /// <summary>
    /// ko�ma
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
    /// ate� etme
    /// </summary>
    /// <param name="obj"></param>
    void Shoot(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if (abilities[abilitiesIndex].cost <= energy.GetCurrentEnergy())
            {
                energy.DecreaseEnergy(abilities[abilitiesIndex].cost);
                animator.Play("Shoot");
                abilities[abilitiesIndex].UseAbility(Camera.main.transform);
            }
        }
    }
    /// <summary>
    /// z�plama
    /// </summary>
    /// <param name="obj"></param>
    void Jump(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if (GroundCheck())//karakter kontrol bile�enini isGrounded k�sm� istikrarl� �al��mad��� i�in b�yle bir kontrol kulland�m
            {
                velocityY = 0;
                velocityY += jumpPower;
            }
        }
    }
    /// <summary>
    /// skill de�i�tirme
    /// </summary>
    /// <param name="obj"></param>
    void SkillChange(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if (abilitiesIndex < abilities.Length - 1)
            {
                abilitiesIndex++;
            }
            else
            {
                abilitiesIndex = 0;
            }
            ChangeAbilities?.Invoke(abilitiesIndex);
        }
    }
    /// <summary>
    /// hareket ve animasyonu ayarlar
    /// kameraya g�re hareket eder
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

        animator.SetFloat("Blend", Mathf.Sqrt(Mathf.Pow(h, 2) + Mathf.Pow(v, 2)) * (speed > walkSpeed ? 2 : 1));
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
            playerInputs.Player.SkillChange.performed += SkillChange;
        }
        else if (playerInputs.Player.enabled == true)
        {
            playerInputs.Player.SkillChange.performed -= SkillChange;
            playerInputs.Player.Jump.performed -= Jump;
            playerInputs.Player.Shoot.performed -= Shoot;
            playerInputs.Player.Run.performed -= Run;
            playerInputs.Player.Run.canceled -= Run;
            Cursor.lockState = CursorLockMode.None;
            playerInputs.Player.Disable();
        }
    }
    /// <summary>
    /// raycast atarak oyuncunun yere de�me durumunu kontrol eder
    /// </summary>
    /// <returns>true ise yere de�iyor manas�na gelir</returns>
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
    /// disable oldu�unda oyuncu girdilerini kapat�r
    /// </summary>
    private void OnDisable()
    {
        GameOver?.Invoke();
        SetPlayerInput(false);
    }

}
