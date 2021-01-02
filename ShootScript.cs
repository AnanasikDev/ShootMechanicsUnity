using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class ShootScript : MonoBehaviour
{
    public ShootMode mode;
    bool shoot = false;
    bool ready = true;
    public float ShootDistance = 100;
    public int DefaultBultsCount = 8;

    public int BultsInTheMagazine = 8;
    public int bultsLeft;
    public float MagazineReloadTime = 2;
    public float BultReloadTime = 0.5f;

    public float Spread = 0;

    public Transform ShootStartPoint;
    public GameObject Bult;

    public float BultSpeed = 1400;
    void Start()
    {
        bultsLeft = DefaultBultsCount;
        Bult.GetComponent<BultScript>().speed = BultSpeed;
        Bult.GetComponent<BultScript>().lifeTime = 10;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot = true;
            if (ready) StartCoroutine("Shoot");
        }
        else shoot = false;
    }
    public IEnumerator Shoot()
    {
        if (shoot)
        {
            ready = false;
            bultsLeft--;
            if (mode == ShootMode.Ray)
            {
                RaycastHit hit;
                bool h = Physics.Raycast(ShootStartPoint.position, ShootStartPoint.forward, out hit, ShootDistance);
            }
            else if (mode == ShootMode.Bult)
            {
                GameObject bult = Instantiate(Bult, ShootStartPoint.position, ShootStartPoint.rotation);
                if (Spread > 0)
                    bult.transform.Rotate(new Vector3(NormalDistribution(-Spread, Spread), Random.Range(-Spread, Spread), 0));
            }
            float extraTime = 0;
            if (bultsLeft <= 0)
            {
                extraTime = MagazineReloadTime;
            }
            
            yield return new WaitForSeconds(BultReloadTime + extraTime);
            ready = true;
            if (bultsLeft <= 0) bultsLeft = BultsInTheMagazine;
        }
        yield return null;
    }
    public enum ShootMode
    {
        Bult,
        Ray
    }
    public float NormalDistribution(float min, float max, float std_deviation = 2, float hor_mean = 0)
    {
        double mean = ((max + min) / 2.0) + hor_mean;
        double stdDev = std_deviation;
        System.Random rand = new System.Random(); //reuse this if you are generating many
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = System.Math.Pow(-2.0 * System.Math.Log(u1) * System.Math.Sin(2.0 * System.Math.PI * u2), 0.5f); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
        return (float)randNormal;
    }
}
