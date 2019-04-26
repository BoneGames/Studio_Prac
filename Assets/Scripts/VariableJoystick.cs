using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VariableJoystick : JoystickInput
{
    //public Transform playerPos;


    [SerializeField]
    private float moveThreshold = 1;
    public float MoveThreshold
    {
        get { return moveThreshold; }
        set { moveThreshold = Mathf.Abs(value); }
    }

    [SerializeField]
    private JoystickType joystickType = JoystickType.Fixed;
    private Vector2 fixedPosition = Vector2.zero;
    public enum JoystickType
        {
            Fixed, Floating, Dynamic, Player
        }

    public void SetMode (JoystickType joystickType)
    {
        this.joystickType = joystickType;
        if(joystickType == JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
            background.gameObject.SetActive(true);
        }
        else
        {
            background.gameObject.SetActive(false);
        }
    }

    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;
        SetMode(joystickType);
        if(joystickType == JoystickType.Player)
        {
            background.GetComponent<Image>().enabled = false;
            handle.GetComponent<Image>().enabled = false;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (joystickType != JoystickType.Fixed)
        {
            background.gameObject.SetActive(false);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if(joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            handle.anchoredPosition = Vector2.zero;
        }
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        base.HandleInput(magnitude, normalised, radius, cam);
        if(joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (joystickType == JoystickType.Player)
        {
            Camera cam = null;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                cam = canvas.worldCamera;
            }

            Vector3 workAround = Camera.main.WorldToScreenPoint(playerPos.position);
            Vector2 playerBase = new Vector2(workAround.x, workAround.y);

            Vector2 position = playerBase;
            Vector2 radius = background.sizeDelta * 0.5f;
            input = (eventData.position - position) / (radius * canvas.scaleFactor);
            FormatInput();
            HandleInput(input.magnitude, input.normalized, radius, cam);
            handle.anchoredPosition = input * radius * handlerRange;
        }
        else
        {
            base.OnDrag(eventData);
        }
    }



    private void Update()
    {
        if (joystickType == JoystickType.Player)
        {
            if(Input.GetMouseButton(0))
            {
                PointerEventData mouse1 = EventSystem.current.gameObject.GetComponent<StandaloneInputModuleCustom>().GetLastPointerEventDataPublic(-1);

                OnDrag(mouse1);
            }
        }
    }
}
