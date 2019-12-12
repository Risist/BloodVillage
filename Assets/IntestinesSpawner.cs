using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntestinesSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject uiCreation;

    public int qualitySpawn;

    [System.Serializable]
    public class Arm
    {
        public int length;
        public float angle = 0;
        public float initialScale = 1.0f;
        public float scaleLoss = 1.0f;
        [System.NonSerialized] public GameObject last;
        [System.NonSerialized] public float scale;
    }

    public List<Arm> arms = new List<Arm>(); 

    void InitialSpawn(Arm arm)
    {
        {
            arm.last = Instantiate(prefab);
            arm.last.transform.LookAt(transform);

            var follow = arm.last.GetComponent<FollowObject>();
            follow.aim = transform;
            follow.angleOffset = arm.angle;
            arm.scale = arm.scaleLoss;
        }
        for (int i = 1; i < arm.length; ++i)
        {
            AddNewArmSegment(arm);
        }
    }

    public void AddNewArm(Arm arm)
    {
        arms.Add(arm);
        arm.last = Instantiate(prefab);
        arm.last.transform.LookAt(transform);

        var follow = arm.last.GetComponent<FollowObject>();
        follow.aim = transform;
        follow.angleOffset = arm.angle; 
        arm.scale = arm.scaleLoss;
    }
    public void AddNewArmSegment(Arm arm)
    {
        var obj = Instantiate(prefab);
        obj.transform.LookAt(arm.last.transform);
        var follow = obj.GetComponent<FollowObject>();
        follow.aim = arm.last.transform;
        arm.last = obj;
        follow.distance *= arm.scale;

        obj.transform.localScale *= arm.scale;
        arm.scale *= arm.scaleLoss;
        
    }

    void SpawnQ0()
    {
        {
            var arm = new Arm();
            arm.angle = 0;
            arm.initialScale = Random.Range(0.75f, 1.25f);
            arm.scaleLoss = Random.Range(0.7f, 0.999f);
            arm.length = Random.Range(3, 5);
            arms.Add(arm);
        }
        int n = Random.Range(0, 1);
        for (int i = 0; i < n; ++i)
        {
            var arm = new Arm();
            arm.angle = 10f * Random.Range(1, 7);
            arm.initialScale = Random.Range(0.75f, 1.25f);
            arm.scaleLoss = Random.Range(0.65f, 0.999f);
            arm.length = Random.Range(2, 3-i);
            arms.Add(arm);

            var arm2 = new Arm();
            arm2.angle = -arm.angle;
            arm2.initialScale = arm.initialScale;
            arm2.scaleLoss = arm.scaleLoss;
            arm2.length = arm.length;
            arms.Add(arm2);
        }
    }
    void SpawnQ1()
    {
        {
            var arm = new Arm();
            arm.angle = 0;
            arm.initialScale = Random.Range(0.75f, 1.25f);
            arm.scaleLoss = Random.Range(0.7f, 0.999f);
            arm.length = Random.Range(4, 6);
            arms.Add(arm);
        }
        int n = Random.Range(1, 2);
        for (int i = 0; i < n; ++i)
        {
            var arm = new Arm();
            arm.angle = 10f * Random.Range(1, 7);
            arm.initialScale = Random.Range(0.8f, 1.25f);
            arm.scaleLoss = Random.Range(0.9f, 0.999f);
            arm.length = Random.Range(4, 7 - i);
            arms.Add(arm);

            var arm2 = new Arm();
            arm2.angle = -arm.angle;
            arm2.initialScale = arm.initialScale;
            arm2.scaleLoss = arm.scaleLoss;
            arm2.length = arm.length;
            arms.Add(arm2);
        }
    }
    void SpawnQ2()
    {
        {
            var arm = new Arm();
            arm.angle = 0;
            arm.initialScale = Random.Range(0.75f, 1.25f);
            arm.scaleLoss = Random.Range(0.7f, 0.999f);
            arm.length = Random.Range(5, 7);
            arms.Add(arm);
        }
        int n = Random.Range(2, 5);
        for (int i = 0; i < n; ++i)
        {
            var arm = new Arm();
            arm.angle = 10f * Random.Range(1, 10);
            arm.initialScale = Random.Range(0.9f, 1.9f);
            arm.scaleLoss = Random.Range(0.925f, 0.999f);
            arm.length = Random.Range(5, 10 - i);
            arms.Add(arm);

            var arm2 = new Arm();
            arm2.angle = -arm.angle;
            arm2.initialScale = arm.initialScale;
            arm2.scaleLoss = arm.scaleLoss;
            arm2.length = arm.length;
            arms.Add(arm2);
        }
    }
    void Start()
    {

        if (qualitySpawn == 0)
            SpawnQ0();
        else if (qualitySpawn == 1)
            SpawnQ1();
        else
            SpawnQ2();

        for (int i = 0; i < arms.Count; ++i)
            InitialSpawn(arms[i]);
    }

}
