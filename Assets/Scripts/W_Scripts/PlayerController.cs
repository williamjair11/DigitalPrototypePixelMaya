using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.InputSystem.XInput;
[RequireComponent(typeof(CharacterController), typeof(EnergyController))]
[RequireComponent(typeof(IsGrounded))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GetGameObjectByRaycast _getObjectByRay;
    [SerializeField] private GameObject _rigthHand, _grabbedObject;
    private InteractiveObject _currentInteractiveObject;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float 
    _initialSpeedPlayer = 5f, 
    _runSpeed, 
    _velocitySpeed, 
    _slowSpeed,
    _gravityForce = 9.8f;

    [SerializeField] private float _jumpForce = 10f, _jumTimeCounter, _jumpMaxTime;
    [SerializeField] private bool _playerIsMoving, _isRunning, _isJumpping, _usingGravity;
    private bool _playerIsCrouch;    
    private Vector3 _lastPlayerPosition;

    [Header("Events")]
    [SerializeField] private UnityEvent 
    _attackEvent,
    _prepareAttackEvent,
    _specialAttackEvent,
    _interactionIsAvaible,
    _onInteractionIsntAvaible, 
    _onGrabObject,
    _onDropObject,
    _onLoockAtInteractiveSurface,
    _onStopLoockingInteractiveSurface, 
    _onRun, _onStopRunning;

    private bool _isInteractionObjectInRange;
    public bool IsInteractionObjectInRange{get => _isInteractionObjectInRange;}

    float _attackTimeCounter, _maxAttackTime;
    bool _isPreparingAttack, _isHoldingSomething;
    IsGrounded _isGrounded;
    [SerializeField] EnergyController _energyController;
    Camera _camera;
    GameObject _weapon = null;
    public GameObject CurrentWeapon
    {
        get => _weapon; set => _weapon = value;
    }
    Vector2 _movementVector;

    public Vector2 MovementVector{set => _movementVector = value;}
    public bool PlayerIsMoving
    {
        get => _playerIsMoving; 
        set{
                _playerIsMoving = value;
                if(!value && CurrentWeapon != null) 
                CurrentWeapon.GetComponent<Weapon>().SetWalkingState(false);
            }
    }

    public bool IsJumpping
    {
        get => _isJumpping; 
        set{
                _isJumpping = value;
                if(!_isJumpping && _characterController.isGrounded) _jumTimeCounter = 0;
            }
    }

    void Awake()
    {
        _characterController.GetComponent<CharacterController>();
        _energyController = GetComponent<EnergyController>();
        _isGrounded = GetComponent<IsGrounded>();
        _velocitySpeed = _initialSpeedPlayer;
        _camera = Camera.main;
    }
    void Update()
    {
        _characterController.Move(Vector3.down * _gravityForce * Time.deltaTime);
        savePosition();
        Run();
        CountPrepareAttackTime();
        if(_isJumpping)
        { 
            
            Jump();
            _jumTimeCounter += Time.deltaTime;
            if(_jumTimeCounter >= _jumpMaxTime)
            {
                _isJumpping = false;
                _jumTimeCounter = 0;
               
            }
        }
        else
        {
            
            _usingGravity = true;
        }
        if(_playerIsMoving) Move();
    }

    void FixedUpdate()
    {
        CheckObjectsInFront();
    }

    void CountPrepareAttackTime()
    {
        if(_isPreparingAttack && _attackTimeCounter < _maxAttackTime) 
        {
            _attackTimeCounter += Time.deltaTime;
        }
    }

    public void Move()
    {
        if(CurrentWeapon != null && !_isHoldingSomething) CurrentWeapon.GetComponent<Weapon>().SetWalkingState(true);
        if(GameManager.Instance.CurrentControlType != ControlType.Vr)
            NormalMove(_movementVector);
        else
            VrMove(_movementVector);
    }

    void NormalMove(Vector2 input)
    {
        _characterController.Move(transform.forward * input.y * _velocitySpeed * Time.deltaTime);
        _characterController.Move(transform.right * input.x * _velocitySpeed * Time.deltaTime);
    }

    void VrMove(Vector2 input)
    {
        _characterController.Move(_camera.transform.forward * input.y * _velocitySpeed * Time.deltaTime);
        _characterController.Move(_camera.transform.right * input.x * _velocitySpeed * Time.deltaTime);
    }
    public Vector3 savePosition()
    {
        if (_lastPlayerPosition != null && _lastPlayerPosition != transform.position)
        {
            if(_isGrounded)
            _lastPlayerPosition = transform.position;
        }

        return _lastPlayerPosition;
    }

    public void Jump() 
    {
        _characterController.Move(Vector3.up * _jumpForce * Time.deltaTime);
    }

    public void RunIsPressed() 
    {
            _isRunning = !_isRunning;
            if(_isRunning && _playerIsMoving) _onRun?.Invoke(); else _onStopRunning?.Invoke();
                if(_isRunning && !_isPreparingAttack) StartCoroutine(CalculteWasteOfEnergy());
            else
                if(!_isRunning && !_isPreparingAttack) StopCoroutine(CalculteWasteOfEnergy());

    }

    void Run()
    {
        if(!_playerIsMoving) _isRunning = false;
        if(_energyController.UpdatingEnergy && _energyController.CurrentEnergy <= 0)
        {
            _isRunning = false;
            _velocitySpeed = _slowSpeed;
            _onStopRunning?.Invoke();
            return;
        }
        if (_isRunning)
            _velocitySpeed = _runSpeed;
            
        else
        {
            _velocitySpeed = _initialSpeedPlayer;
            _onStopRunning?.Invoke();
        }
         
    }

    public void ReturnsToNormalSpeed() 
    {
        _velocitySpeed = _initialSpeedPlayer;
    }

    public void PrepareAttack()
    {
        if(CurrentWeapon != null && !_isHoldingSomething)
        {
            CurrentWeapon.GetComponent<DamageController>().CanMakeDamage = true;
            _isPreparingAttack = true;
            if(!_isRunning) StartCoroutine(CalculteWasteOfEnergy());
            _prepareAttackEvent?.Invoke();
            CurrentWeapon.GetComponent<Weapon>().PrepareAttack();
            if(_energyController.CurrentEnergy <= 0) PerformAttack();
        }
    }

    public void PerformAttack()
    {
        if((CurrentWeapon != null && !_isHoldingSomething) || _energyController.CurrentEnergy <= 0)
        {
            _isPreparingAttack = false;
            _attackEvent?.Invoke();
            CurrentWeapon.GetComponent<Weapon>().PerformAttack();
            if(!_isRunning && !_isPreparingAttack) StopCoroutine(CalculteWasteOfEnergy());
        }
    }

    public void PrepareSpecialAttack()
    {   
        if(CurrentWeapon != null && !_isHoldingSomething)
        {
            _isPreparingAttack = true;
            if(!_isRunning) StartCoroutine(CalculteWasteOfEnergy());
            _specialAttackEvent?.Invoke();
        }
    }

    public void PerformSpecialAttack()
    {
        if(CurrentWeapon != null && !_isHoldingSomething)
        {
            _isPreparingAttack = false;
            if(!_isRunning && !_isPreparingAttack) StopCoroutine(CalculteWasteOfEnergy());
            _specialAttackEvent?.Invoke();
        }
    }

    public void CheckObjectsInFront()
    {
        GameObject gObject = _getObjectByRay.ThrowRaycast();

        if(gObject != null && gObject.GetComponent<InteractiveObject>())
        {
            SetInteractiveObject(gObject.GetComponent<InteractiveObject>());
            _isInteractionObjectInRange = true;
            _onLoockAtInteractiveSurface?.Invoke();
        }
        else
        {
            if(gObject != null)
            {
                _onLoockAtInteractiveSurface?.Invoke();
            }
            if(_isInteractionObjectInRange)
            {
                SetNullInteractiveObject();
            }
        }
        if(gObject == null)
        {
            _onStopLoockingInteractiveSurface?.Invoke();
        }
    }

    public void SetInteractiveObject(InteractiveObject interactiveObject)
    {
        _interactionIsAvaible?.Invoke();
        _currentInteractiveObject = interactiveObject;
    }

    public void SetNullInteractiveObject()
    {
        SetInteractiveObject(null);
        _isInteractionObjectInRange = false;
        _onInteractionIsntAvaible?.Invoke();
    }

    public void Interact()
    {
        if(_isHoldingSomething) { DropObject(); return; }
        
        if(_currentInteractiveObject == null) return;
        switch (_currentInteractiveObject.InteractionType)
        {
            case InteractionType.Grab:
                GrabObject();
                break;
            case InteractionType.Found:
                _currentInteractiveObject.Interact();
                break;
            default:
                break;
        }
    }

    public void DropObject()
    {
        Vector3 newObjectPosition = _getObjectByRay.LastHitPoint;
        CurrentWeapon?.SetActive(true);
        _grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        _grabbedObject.transform.SetParent(transform.parent);
        if(Vector3.Distance(newObjectPosition, _grabbedObject.transform.position) < 3)
            _grabbedObject.transform.position = newObjectPosition;
        else
        _grabbedObject.transform.position = transform.position + transform.forward;
        _grabbedObject = null;
        _isHoldingSomething = false;
        _onDropObject?.Invoke();
    }

    private void GrabObject()
    {
        CurrentWeapon?.SetActive(false);
        _isHoldingSomething = true;
        _grabbedObject = _currentInteractiveObject.gameObject;
        _grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        _grabbedObject.transform.SetParent(_rigthHand.transform);
        _grabbedObject.transform.localPosition = Vector3.zero;
        _onGrabObject?.Invoke();
    }

    public void Crouch()
    {
        if(!_playerIsCrouch)
        {
            Vector3 crouchPosition = 
            new Vector3(_camera.transform.position.x, _camera.transform.position.y/2, _camera.transform.position.z);
            Debug.Log(crouchPosition);
            _camera.transform.position = crouchPosition;
        }
        else
        {
            GetUp();
        }
        _playerIsCrouch = !_playerIsCrouch;
    }

    public void GetUp()
    {
        Vector3 crouchPosition = 
        new Vector3(_camera.transform.position.x, _camera.transform.position.y*2, _camera.transform.position.z);
        _camera.transform.position = crouchPosition;
    }

    public void SetWeapon(Weapon weapon)
    {
        if(_isHoldingSomething) return;
        if(CurrentWeapon)
        {
            CurrentWeapon.GetComponent<DestroyWithDelay>().Destroy();
            CurrentWeapon = null;
        }
        CurrentWeapon = Instantiate(weapon.gameObject, _rigthHand.transform);
        CurrentWeapon.transform.localPosition = Vector3.zero;
    }

    public IEnumerator CalculteWasteOfEnergy()
    {
        float energy = 0;
        while(_isRunning || _isPreparingAttack)
        {
            energy = 0;
            yield return new WaitForSeconds(.3f);
            if(_isRunning) energy += .03f;
            if(_isPreparingAttack) energy += .03f; 
            _energyController.DecreaseEnergy(energy);
        }
    }

    
}
