using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class filledScript : MonoBehaviour {

    Image img;

	// Use this for initialization
	void Start () {
        img = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	public void SetFillAmount (float f) {
        img.fillAmount = f;
	}

    public void disableFiller()
    {
        img.enabled = false;
    }

    public void setPos(Vector3 pos, Direction dir)
    {
        img.enabled = true;
        transform.position = pos + new Vector3(0, 0.01f, 0);
        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0, 0, 180));
        switch (dir)
        {
            case Direction.Up:
                transform.position -= new Vector3(0, 0, 0.5f);
                transform.Rotate(new Vector3(90, 0, 0));
                break;
            case Direction.Down:
                transform.position -= new Vector3(0, 0, -0.5f);
                transform.Rotate(new Vector3(90, 0, 180));
                break;
            case Direction.Right:
                transform.position -= new Vector3(0.5f, 0, 0);
                transform.Rotate(new Vector3(90, 0, 90));
                break;
            case Direction.Left:
                transform.position -= new Vector3(-0.5f, 0, 0);
                transform.Rotate(new Vector3(270, 0, 270));
                break;
        }
    }
}
