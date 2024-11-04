using UnityEngine;
using UnityEngine.UI;

public class JetpackController : MonoBehaviour
{
    public MovimientoJugador Player;
    public float jumpForce;

    public float jetpackForce;
    public float jetpackDuration;
    private float jetpackFuel;
    public float jetpackCD;
    private float jetpackCDWaited;
    private bool usingJetpack;
    public Image fuelImage;


    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        ReloadJetpackFuel();
    }
    void Update()
    {
        Player.CheckGrounded();
        CheckInputs();
        JetpackBehaviour();
    }
    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Player.grounded)
            {
                Jump();
            }
            else if (JetpackHaveFuel())
            {
                usingJetpack = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && usingJetpack)
        {
            usingJetpack = false;
        }

    }
    
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void JetpackBehaviour()
    {
        if (usingJetpack)
        {
            rb.AddForce(Vector3.up * jetpackForce * Time.deltaTime, ForceMode.Force);

            jetpackFuel -= Time.deltaTime;
            if (!JetpackHaveFuel())
                usingJetpack = false;
        }
        else if (Player.grounded)
        {
            if (!JetpackHaveFuel())
            {
                jetpackCDWaited += Time.deltaTime;
                if(jetpackCDWaited >= jetpackCD)
                    ReloadJetpackFuel();
            }
            else
            {
                ReloadJetpackFuel();
            }
        }

        fuelImage.fillAmount = jetpackFuel;

    }
    private bool JetpackHaveFuel()
    {
        return jetpackFuel > 0;
    }
    private void ReloadJetpackFuel()
    {
        jetpackCDWaited = 0;
        jetpackFuel = jetpackDuration;
    }
}
