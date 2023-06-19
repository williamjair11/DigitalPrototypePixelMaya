using UnityEngine;
public class PickupController : MonoBehaviour
{
    [SerializeField] Transform HoldArea;
    public GameObject heldObj;
    private Rigidbody heldObjRB;
    private Transform Obj;

    [SerializeField] private float pickupRange = 5f;
    [SerializeField] private float pickupForce = 150f;
    [SerializeField] private float fwdforce = 20f;
    [SerializeField] private float upforce = 30f;

    
    private InputController _inputController;

    public void Start()
    {
        _inputController = FindObjectOfType<InputController>();
    }
    public void Update()
    {
        if (_inputController.Interact()) 
        {
            Grab(); 
        }

        if (_inputController.ThrowObject() && heldObj != null)
        {
            ThrowObj();
        }

        if (heldObj!=null)
        {
            MoveObject();
        }  
        void MoveObject()
        {
            if(Vector3.Distance(heldObj.transform.position,HoldArea.position)>0.1f)
            {
                Vector3 moveDirection=(HoldArea.position-heldObj.transform.position).normalized;
                heldObjRB.AddForce(moveDirection * pickupForce);
            }
        }
    }
    void PickupObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB=pickObj.GetComponent<Rigidbody>();

            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
            Obj=pickObj.GetComponent<Transform>();
            heldObjRB.transform.parent = HoldArea;
            heldObj=pickObj;
        }
    }
   public void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.isKinematic = false;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObjRB.transform.parent = null;
        heldObj = null;
   }
    public void ThrowObj()
    {
        heldObjRB.useGravity = true;
        heldObjRB.AddForce(HoldArea.transform.forward*fwdforce,ForceMode.Impulse);
        heldObjRB.AddForce(HoldArea.transform.up * upforce, ForceMode.Impulse);      
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObjRB.transform.parent = null;
        heldObj = null;
    }
    public void Grab()
    {      
        if (heldObj == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
            {
                PickupObject(hit.transform.gameObject);
            }
        }
        else
        {
            DropObject();
        }
    }
    
}


