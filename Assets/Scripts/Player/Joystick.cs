using UnityEngine;
public class Joystick : MonoBehaviour
{
    public RectTransform center;
    public RectTransform knob;
    public float range;
    public bool fixedJoystick;
	
    [HideInInspector]
    public Vector2 direction;
    public bool active;
    
    void Start()
    {
        ShowHide(false);
        active = false;
        direction = Vector2.one;
    }
	
    void Update(){
        Vector2 pos = Input.mousePosition;
		
        if(Input.GetMouseButtonDown(0)){
            ShowHide(true);
            active = true;
            knob.position = pos;
            center.position = pos;
        }
        
        else if(Input.GetMouseButton(0)){
            knob.position = pos;
            knob.position = center.position + Vector3.ClampMagnitude(knob.position - center.position, center.sizeDelta.x * range);
			
            if(knob.position != Input.mousePosition && !fixedJoystick){
                Vector3 outsideBoundsVector = Input.mousePosition - knob.position;
                center.position += outsideBoundsVector;
            }

            if ((knob.position - center.position).normalized != Vector3.zero)
                direction = (knob.position - center.position).normalized;
        }
        
        else
            active = false;

        if (!active)
        {
            ShowHide(false);
        }
    }
	
    void ShowHide(bool state)
    {
        center.gameObject.SetActive(state);
        knob.gameObject.SetActive(state);
    }
}