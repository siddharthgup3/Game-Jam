using UnityEngine;

public class JPPlayerCam : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private float lookSensitivity = 1.5f;

    [SerializeField] public float speed;

    [SerializeField] private float smoothing = 1.5f;

    [SerializeField] private Vector2 smoothedVelocity;
    [SerializeField] private Vector2 currentLookingPos;

    public float playersYRotation;

    void Start()
    {
        LockCursors();
    }

    private void OnEnable()
    {
        currentLookingPos.x = player.transform.eulerAngles.y;
    }

    void Update()
    {

        playersYRotation = player.transform.eulerAngles.y;

        RotateCamera();
        ClampCamera();

    }
    private void RotateCamera()
    {

        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPos += smoothedVelocity;

        transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);

        player.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, player.transform.up);
    }
    private void ClampCamera()
    {
        //print("Debug: Clamp Dat Camera!");
    }
    private void LockCursors()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}