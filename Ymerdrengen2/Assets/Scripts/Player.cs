using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator PlayerAnim;

    public Vector3 startPos;
    public Vector3 endPos;
    public bool isLerping;
    public float speed;
    private float startTime;
    private float journeyLength;

    public int maxYmer;
    public int numYmer;

    void Start()
    {
        startTime = Time.time;
        journeyLength = 1;
        startPos = endPos = transform.position;

        maxYmer = 3;
        numYmer = 0;
    }

    public void Move(MoveDirection dir)
    {
        PlayerAnim.SetBool("Move", true);

        if(PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("Ymerdreng_Jump_Anim_001"))
        {
            Debug.Log("IS JUMOPING WHEEEE");
            PlayerAnim.Play("Ymerdreng_Jump_Anim_001", -1, 0);
        }

        switch (dir) {


            case MoveDirection.LeftUp: {
                    startTime = Time.time;

                    startPos = transform.position; 
                    endPos = transform.position + new Vector3(0, 0, 1);
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                    //transform.Translate(0, 0, 1);
                    break;
                }
            case MoveDirection.RightUp: {
                    startTime = Time.time;

                    startPos = transform.position;
                    endPos = transform.position + new Vector3(1, 0, 0);
                    transform.localEulerAngles = new Vector3(0, 90, 0);

                    //transform.Translate(1, 0, 0);
                    break;
                }
            case MoveDirection.RightDown: {
                    startTime = Time.time;

                    startPos = transform.position;
                    endPos = transform.position + new Vector3(0, 0, -1);
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                    //transform.Translate(0, 0, -1);
                    break;
                }
            case MoveDirection.LeftDown: {
                    startTime = Time.time;

                    startPos = transform.position;
                    endPos = transform.position + new Vector3(-1, 0, 0);
                    transform.localEulerAngles = new Vector3(0, 270, 0);

                    //transform.Translate(-1, 0, 0);
                    break;
                }
            default: throw new System.Exception("ERROR: Enum had unrecognizable value.");
        }
    }

    public void loseYogurt()
    {

        foreach (Transform child in transform)
        {
            if(child.gameObject.tag == "PickUp")
                GameObject.Destroy(child.gameObject);
        }
    }

    public void Update()
    {
        if (PlayerAnim.GetBool("Move") && Time.time > startTime)
        {
            PlayerAnim.SetBool("Move", false);
        }

        //Debug.Log("LERPING");
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
        if(transform.position == endPos)
        {
            isLerping = false;

        }
        else
        {

            isLerping = true;
        }
    }
}
