using UnityEngine;
using UnityEngine.UI;

public class TargetPointer : MonoBehaviour
{
	public Transform Target;
	public RectTransform PointerUI;
	[SerializeField] private RectTransform _text;
	public Sprite PointerIcon;
	public Sprite OutOfScreenIcon;
	public float InterfaceScale = 100;

	[SerializeField] private GameObject _pointerCanvas;
	[SerializeField] private GameObject _outOfScreenCanvas;

	private Vector3 _startPointerSize;
	private Camera _mainCamera;


	private void Awake()
	{
		_startPointerSize = PointerUI.sizeDelta;
		_mainCamera = Camera.main;
	}
	private void LateUpdate()
	{
		Vector3 realPos = _mainCamera.WorldToScreenPoint(Target.position);
		Rect rect = new Rect(0, 0, Screen.width, Screen.height);

		Vector3 outPos = realPos;
		float direction = 1;

		PointerUI.GetComponent<Image>().sprite = OutOfScreenIcon;
		_pointerCanvas.gameObject.SetActive(true);
		_outOfScreenCanvas.gameObject.SetActive(false);

		if (!IsBehind(Target.position))
		{
			if (rect.Contains(realPos))
			{
				PointerUI.GetComponent<Image>().sprite = PointerIcon;
				_pointerCanvas.gameObject.SetActive(false);
				_outOfScreenCanvas.gameObject.SetActive(true);
			}
		}
		else
		{
			realPos = -realPos;
			outPos = new Vector3(realPos.x, 0, 0);
			if (_mainCamera.transform.position.y < Target.position.y)
			{
				direction = -1;
				outPos.y = Screen.height;			
			}
		}
		
		float offset = PointerUI.sizeDelta.x / 2;
		outPos.x = Mathf.Clamp(outPos.x, offset, Screen.width - offset);
		outPos.y = Mathf.Clamp(outPos.y, offset, Screen.height - offset);

		Vector3 pos = realPos - outPos;

		RotatePointer(direction * pos);

		PointerUI.sizeDelta = new Vector2(_startPointerSize.x / 100 * InterfaceScale, _startPointerSize.y / 100 * InterfaceScale);
		PointerUI.anchoredPosition = outPos;
	}
	private bool IsBehind(Vector3 point)
	{
		Vector3 forward = _mainCamera.transform.TransformDirection(Vector3.forward);
		Vector3 toOther = point - _mainCamera.transform.position;
		if (Vector3.Dot(forward, toOther) < 0) return true;
		return false;
	}
	private void RotatePointer(Vector2 direction)
	{
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		PointerUI.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		_text.rotation = Quaternion.identity;
	}
}
