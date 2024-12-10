using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleThrow : MonoBehaviour
{
    public float rotationSpeed = 0.5f;
    public float throwPower = 40;
    public GameObject bottleObj;
    public GameObject fireBottleObj;
    public Transform throwPoint;

    LineRenderer line;
    public int linePoints = 75;
    public float pointDistance = 0.03f;
    public LayerMask collideLayer;
    public Material mBlue, mRed;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.inventoryOpen == false && SaveScript.weaponID > 6)
        {
            float HorizontalRotation = Input.GetAxis("Mouse X") * 2;
            float VerticalRotation = Input.GetAxis("Mouse Y") * 2;

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, HorizontalRotation * rotationSpeed, VerticalRotation * rotationSpeed));

            if (Input.GetAxis("Mouse Y") > 0)
            {
                if (throwPower < 70)
                {
                    throwPower += 6 * Time.deltaTime;
                }
            }
            if (Input.GetAxis("Mouse Y") < 0)
            {
                if (throwPower > 20)
                {
                    throwPower -= 12 * Time.deltaTime;
                }
            }

            line.positionCount = linePoints;
            List<Vector3> points = new List<Vector3>();
            Vector3 startPos = throwPoint.position;
            Vector3 startVelocity = throwPoint.forward * throwPower;

            if (Input.GetMouseButton(1))
            {
                if(SaveScript.weaponID == 7)
                {
                    line.material = mBlue;
                }
                if (SaveScript.weaponID == 8)
                {
                    line.material = mRed;
                }
                for (float i = 0; i < linePoints; i += pointDistance)
                {
                    Vector3 newPoint = startPos + i * startVelocity;
                    newPoint.y = startPos.y + startVelocity.y + i + Physics.gravity.y / 2f * i * i;
                    points.Add(newPoint);

                    if (Physics.OverlapSphere(newPoint, 0.01f, collideLayer).Length > 0)
                    {
                        line.positionCount = points.Count;
                        break;
                    }
                }

                line.SetPositions(points.ToArray());
            }

            if (Input.GetMouseButtonUp(1))
            {
                line.positionCount = 0;
            }


            if (WeaponManager.emptyBottleThrow == true)
            {
                WeaponManager.emptyBottleThrow = false;
                GameObject createBottle = Instantiate(bottleObj, throwPoint.position, throwPoint.rotation);
                createBottle.GetComponentInChildren<Rigidbody>().velocity = throwPoint.transform.forward * throwPower;
                SaveScript.weaponAmts[7]--;
                SaveScript.change = true;
            }

            if (WeaponManager.fireBottleThrow == true)
            {
                WeaponManager.fireBottleThrow = false;
                GameObject createBottle = Instantiate(fireBottleObj, throwPoint.position, throwPoint.rotation);
                createBottle.GetComponentInChildren<Rigidbody>().velocity = throwPoint.transform.forward * throwPower;
                SaveScript.weaponAmts[7]--;
                SaveScript.itemAmts[3]--;
                SaveScript.change = true;
            }
        }
       
    }
}
