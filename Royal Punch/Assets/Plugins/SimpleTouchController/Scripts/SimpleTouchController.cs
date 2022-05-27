using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SimpleTouchController : MonoBehaviour 
{

	[SerializeField] private Camera _main;
	// PUBLIC
	public delegate void TouchDelegate(Vector2 value);
	public event TouchDelegate TouchEvent;

	public delegate void TouchStateDelegate(bool touchPresent);
	public event TouchStateDelegate TouchStateEvent;

	// PRIVATE
	[SerializeField] private RectTransform joystickArea;
	private bool touchPresent = false;
	private Vector2 movementVector;

	private bool _isDragging = false;

    private void Update()
    {
		if (_isDragging)
        {
			var localPos = transform.InverseTransformPoint(Input.mousePosition) + new Vector3(200, -200, 0);
			joystickArea.localPosition = new Vector2(Mathf.Clamp(localPos.x, 60, 320), Mathf.Clamp(localPos.y, -320, -60));
			//joystickArea.position = Input.mousePosition;

        }
	}

    public Vector2 GetTouchPosition
	{
		get { return movementVector;}
	}


	public void BeginDrag()
	{
		_isDragging = true;
		touchPresent = true;
        TouchStateEvent?.Invoke(touchPresent);
    }

	public void EndDrag()
	{
		_isDragging = false;
		touchPresent = false;
		movementVector = joystickArea.anchoredPosition = Vector2.zero;

        TouchStateEvent?.Invoke(touchPresent);

    }

	public void OnValueChanged(Vector2 value)
	{
		if(touchPresent)
		{
			// convert the value between 1 0 to -1 +1
			movementVector.x = ((1 - value.x) - 0.5f) * 2f;
			movementVector.y = ((1 - value.y) - 0.5f) * 2f;

            TouchEvent?.Invoke(movementVector);
        }

	}

}
