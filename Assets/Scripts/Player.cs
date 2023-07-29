using System;
using DefaultNamespace;
using UnityEngine;


public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterVisEventArgs> OnSelectedCounterVis;
    public event EventHandler OnPickSomething;


    public class OnSelectedCounterVisEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private Transform playerGrabTransform;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    private BaseCounter selectedCounter;
    private float rotateSpeed = 10f;
    private bool isWalking;
    private float playerRadius = 0.7f;
    private float playerHeight = 2f;
    private float playerDistance = 0;
    private Vector3 lastMoveDir;
    private KitchenObjects kitchenObject;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("there is more than one player");
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteract;
        gameInput.OnInteractAltAction += GameInput_OnInteractAlt;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void GameInput_OnInteractAlt(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlt(this);
        }
    }

    private void GameInput_OnInteract(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }


    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float rayCastDistance = 2f;
        if (moveDir != Vector3.zero)
        {
            lastMoveDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastMoveDir, out RaycastHit raycastHit, rayCastDistance,
                counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        playerDistance = speed * Time.deltaTime;

        // Perform a capsule cast to check for obstacles in the movement directions
        bool canMoveForward = CanMoveInDirection(moveDir);
        bool canMoveRight = CanMoveInDirection(Quaternion.Euler(0, 90, 0) * moveDir);
        bool canMoveLeft = CanMoveInDirection(Quaternion.Euler(0, -90, 0) * moveDir);


        // Check if movement is possible in any available direction
        if (canMoveForward)
        {
            transform.position += moveDir * playerDistance;
        }

        if (canMoveRight)
        {
            transform.position += Quaternion.Euler(0, 90, 0) * moveDir * playerDistance;
        }

        if (canMoveLeft)
        {
            transform.position += Quaternion.Euler(0, -90, 0) * moveDir * playerDistance;
        }

        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private bool CanMoveInDirection(Vector3 direction)
    {
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius,
            direction, playerDistance);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterVis?.Invoke(this,
            new OnSelectedCounterVisEventArgs { selectedCounter = selectedCounter });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return playerGrabTransform;
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        //if you want to add animations when grabbing add them here
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickSomething?.Invoke(this, EventArgs.Empty);
        }
    }


    public KitchenObjects GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}