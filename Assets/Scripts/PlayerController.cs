using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float mYaw;
    float mPitch;

    bool m_AngleLocked, m_AimLocked;

    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockkeyCode = KeyCode.O;

    private GameManager gameManager;
   
    [SerializeField] User user;

    [Header("Rotation")]

    [SerializeField] GameObject mPitchController;
    [SerializeField] float mPitchSpeed;
    [SerializeField] float mYawSpeed;
    [SerializeField] float mMinPitch;
    [SerializeField] float mMaxPitch;

    [Header("Movement")]

    [SerializeField] CharacterController mCharacterController;
    [SerializeField] float mSpeed;
    public KeyCode mForwardKey = KeyCode.W;
    public KeyCode mLeftKey = KeyCode.A;
    public KeyCode mBackKey = KeyCode.S;
    public KeyCode mRightKey = KeyCode.D;
    public KeyCode mJumpKey = KeyCode.Space;
    public KeyCode mRunKey = KeyCode.LeftShift;
    [SerializeField] float mRunMultiplier;
    [SerializeField] bool mOnGround;
    [SerializeField] bool mContactAbove;

    [Header("Jump")]

    [SerializeField] float mHeightJump;
    [SerializeField] float mHalfLengthJump;
    [SerializeField] float mDownGravityMultiplier;
    float mVerticalSpeed;

    [Header("Attack")]

    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform gunPositioner;



    private void Awake()
    {
        mPitch = mPitchController.transform.rotation.eulerAngles.x;
        mYaw = transform.rotation.eulerAngles.y;
       
    }

    private void Start()
    {
        //weaponAnim.setIdle();
        gameManager = GameManager.gameManager;
    }

    private void Update()
    {
        CheckLockCursor();

        if (gameManager.gameState == GameManager.GameState.Playing)
        {
            if (Input.GetKey(KeyCode.Escape)) gameManager.setState(GameManager.GameState.MainMenu);

            float mouseAxisX = Input.GetAxis("Mouse X");
            float mouseAxisY = Input.GetAxis("Mouse Y");

            

            Rotate(mouseAxisX, mouseAxisY);

            Move();

            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }
        
    }

    private void Rotate(float mouseAxisX, float mouseAxisY)
    {
        if (!m_AngleLocked)
        {
            mYaw += mouseAxisX * mYawSpeed;
            mPitch += -mouseAxisY * mPitchSpeed;
        }

        mPitch = Mathf.Clamp(mPitch, mMinPitch, mMaxPitch);
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, mYaw, 0.0f));
        mPitchController.transform.localRotation = Quaternion.Euler(new Vector3(mPitch, 0.0f, 0.0f));
    }

    private void Move()
    {
        Vector3 lMovement = new Vector2();
        Vector3 forward = new Vector3(Mathf.Sin(mYaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(mYaw * Mathf.Deg2Rad)); //en funcion del yaw
        Vector3 right = new Vector3(Mathf.Sin((mYaw + 90.0f) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((mYaw + 90.0f) * Mathf.Deg2Rad)); //en funcion del yaw + 90

        if (Input.GetKey(mForwardKey)) lMovement += forward;
        else if (Input.GetKey(mBackKey)) lMovement -= forward;
        if (Input.GetKey(mRightKey)) lMovement += right;
        else if (Input.GetKey(mLeftKey)) lMovement -= right;

        float lCurrentRunMultiplier = 1.0f;

        if (Input.GetKey(mRunKey))
        {
            lCurrentRunMultiplier = mRunMultiplier;
        }

        lMovement.Normalize();
        lMovement *= mSpeed * lCurrentRunMultiplier * Time.deltaTime;

        float gravity = -2 * mHeightJump * mSpeed * mRunMultiplier * mSpeed * mRunMultiplier / (mHalfLengthJump * mHalfLengthJump);
        if (mVerticalSpeed < 0.0f) gravity *= mDownGravityMultiplier;
        mVerticalSpeed += gravity * Time.deltaTime;
        lMovement.y += mVerticalSpeed * Time.deltaTime + 0.5f * gravity * Time.deltaTime * Time.deltaTime;

        
        CollisionFlags colls = mCharacterController.Move(lMovement);

        mOnGround = (colls & CollisionFlags.Below) != 0;
        if (mContactAbove && mVerticalSpeed > 0.0f) mVerticalSpeed = 0.0f;
        if (mOnGround) mVerticalSpeed = 0.0f;

        if (Input.GetKeyDown(mJumpKey) && mOnGround) mVerticalSpeed = 2 * mHeightJump * mPitchSpeed * mRunMultiplier / mHalfLengthJump;

    }

      private void Attack()
    {
        //weaponAnim.setShoot();
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hitInfo, user.currentItem.distance))
        {
            //ShootParticles();
            /*Ray myRay = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            Debug.DrawRay(myRay.origin, myRay.direction, Color.red, 5);
            */ 

            if (hitInfo.transform != null && hitInfo.transform.gameObject.TryGetComponent<ICanTakeDmg>(out ICanTakeDmg dmg))
            {
                var distance = Vector3.Distance(transform.position, hitInfo.transform.position);
                dmg.TakeDmg(user.currentItem.damage);
            }

        }
    }


    /*private void ShootParticles()
    {
        Instantiate(currentItem.gunImpactParticles, gunPositioner.position, Quaternion.identity);
    }*/


    private void OnApplicationFocus(bool focus)
    {
        if (m_AimLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void CheckLockCursor()
    {
        if (Input.GetKeyDown(m_DebugLockAngleKeyCode)) m_AngleLocked = !m_AngleLocked;
        if (Input.GetKeyDown(m_DebugLockkeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
        }
    }
}
