using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AxisOptions { Both, Horizontal, Vertical }

public class JoystickInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    #region Variables
    [SerializeField]
    private float handlerRange = 1;
    [SerializeField]
    private float deadZone = 0;
    [SerializeField]
    private AxisOptions axisOptions = AxisOptions.Both;
    [SerializeField]
    private bool snapX = false;
    [SerializeField]
    private bool snapY = false;
    [SerializeField]
    protected RectTransform background = null;
    [SerializeField]
    protected RectTransform handle = null;
    #endregion

    #region References
    private RectTransform baseRect = null;
    private Canvas canvas;
    private Camera cam;
    public Vector2 input = Vector2.zero;
    #endregion

    #region Properties

    public float Horizontal
    {
        get { return (snapX) ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x; }
    }
    public float Vertical
    {
        get { return (snapY) ? SnapFloat(input.y, AxisOptions.Vertical) : input.y; }
    }

    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    public float handleRange
    {
        get { return handlerRange; }
        set { handlerRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }

    public AxisOptions AxisOptions { get; set; }
    public bool SnapX { get; set; }
    public bool SnapY { get; set; }

    #endregion

    protected virtual void Start()
    {
        handleRange = handlerRange;
        DeadZone = deadZone;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if(!canvas)
        {
            Debug.LogError("The joystick in't childed to the canvas, scrub");
        }
        Vector2 center = new Vector2(.5f, .5f);
        background.pivot = handle.anchorMin = handle.anchorMax = handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    private void FormatInput()
    {
        if (axisOptions == AxisOptions.Horizontal)
            input = new Vector2(input.x, 0);
        else if (axisOptions == AxisOptions.Vertical)
            input = new Vector2(0, input.y);
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        else
        input = Vector2.zero;
    }

    protected Vector2 ScreenPointToAnchoredPosition (Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            return localPoint - (background.anchorMax * baseRect.sizeDelta);
        }
        return Vector2.zero;
    }

    private float SnapFloat(float value, AxisOptions snapAxis)
    {
        if(value == 0)
            return value;
        if(axisOptions == AxisOptions.Both)
        {
            float angle = Vector2.Angle(input, Vector2.up);
            if(snapAxis == AxisOptions.Horizontal)
            {
                if (angle < 22.5f || angle > 157.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            else if(snapAxis == AxisOptions.Vertical)
            {
                if (angle > 67.5f || angle < 112.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            return value;
        }
        else
        {
            if (value > 0)
                return 1;
            if (value < 0)
                return -1;
        }
        return 0;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            cam = canvas.worldCamera;
        }

        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta * 0.5f;
        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        FormatInput();
        HandleInput(input.magnitude, input.normalized, radius, cam);
        handle.anchoredPosition = input * radius * handlerRange;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
         OnDrag(eventData);
    }


    private void Update()
    {
        //Debug.Log(input);
    }
}
