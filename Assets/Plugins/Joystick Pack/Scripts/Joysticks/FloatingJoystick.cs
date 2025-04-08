using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    private Vector3 posJoystick;

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(true);
        posJoystick = background.transform.localPosition;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        background.transform.localPosition = posJoystick;
    }
}