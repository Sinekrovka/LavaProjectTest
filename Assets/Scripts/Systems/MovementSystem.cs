using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MovementSystem : MonoBehaviour
{
    
    public static MovementSystem Instance;
    private NavMeshAgent _playerAgent;
    private DefaultInputActions _inputActions;

    private bool _move;
    private bool _joystickMove;
    private Transform _cameraMovement;
    private Vector3 _startCameraParams;
    private Vector2 _joystickMovement;

    private Image _joystickBackground;
    private Image _joystickHandle;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        
        _inputActions = new DefaultInputActions();
        _inputActions.Enable();

        _inputActions.Player.Move.started += MovementPlayer;
        _inputActions.Player.Move.canceled += MovementPlayer;
        _inputActions.Player.Fire.started += ShowJoystick;
        
        _playerAgent = GameObject.FindWithTag("Player").GetComponent<NavMeshAgent>();
        _cameraMovement = _playerAgent.transform.Find("FollowtoCamera");
        _startCameraParams = _cameraMovement.localPosition;
        _cameraMovement.SetParent(null);

        _joystickBackground = GameObject.Find("Canvas/Joystick").GetComponent<Image>();
        _joystickHandle = GameObject.Find("Canvas/Joystick/JoystickPin").GetComponent<Image>();

    }

    private void MovementCamera()
    {
        _cameraMovement.position = _playerAgent.transform.position + _startCameraParams;
    }
    
    private void MovementPlayer(InputAction.CallbackContext value)
    {
        _move = value.started;
    }

    private void Update()
    {
        if (_move)
        {
            if (_joystickMove)
            {
                _playerAgent.destination = 
                    _playerAgent.transform.position + 
                    new Vector3(_joystickMovement.x, 0, _joystickMovement.y);
                AnimatedJoystick(1);
            }
            else
            {
                var newMov = _inputActions.Player.Move.ReadValue<Vector2>();
                _playerAgent.destination = 
                    _playerAgent.transform.position + new Vector3(newMov.x, 0, newMov.y);
            }
            MovementCamera();
        }
        else
        {
            AnimatedJoystick(0);
        }
    }

    public void JoystickPlayerMove(Vector2 position)
    {
        _joystickMovement = position.normalized;
        _joystickMove = position != Vector2.zero;
        _move = _joystickMove;
    }

    private void AnimatedJoystick(float finalAlpha)
    {
        _joystickBackground.DOKill();
        _joystickHandle.DOKill();

        _joystickBackground.DOFade(finalAlpha, 0.3f);
        _joystickHandle.DOFade(finalAlpha, 0.3f);
    }

    private void ShowJoystick(InputAction.CallbackContext value)
    {
        Vector3 positionJoystick = Input.mousePosition;
        _joystickBackground.GetComponent<RectTransform>().position = positionJoystick;
    }

    public Transform GetPlayer => _playerAgent.transform;
}
