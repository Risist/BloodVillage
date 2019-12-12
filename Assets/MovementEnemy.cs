using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : Movement
{
    [Header("ai settings")]
    public float angleIncrease = 5.0f;
    public float[] chances;

    float angle;
    Timer tStayInState = new Timer();

    float IdleChance()
    {
        return chances[(int)EState.EIdle];
    }
    void IdleInit()
    {
        tStayInState.cd = Random.Range(1.0f, 1.5f);
    }
    void Idle()
    {
        if (timerChangeDir.IsReadyRestart())
        {
            ApplyDir(Random.insideUnitCircle);
            timerChangeDir.cd = Random.Range(minDirChange, maxDirChange);
        }
        MoveToDir(new Vector3(lastDir.x, 0, lastDir.y));
    }

    float SpinChance()
    {
        return chances[(int)EState.ESpin];
    }
    void SpinInit()
    {
        tStayInState.cd = Random.Range(0.75f, 2.0f);
    }
    void Spin()
    {
        angle += angleIncrease;
        MoveToDir(Quaternion.Euler(0, angle, 0) * Vector3.forward);
    }

    float FollowChance()
    {
        float chance = chances[(int)EState.EFollow];
        //chance *= ( 1 + AttentionPath.instance.GetDirectionTo(transform.position, 0).magnitude *0.25f);
        return chance;
    }
    void FollowInit()
    {
        tStayInState.cd = Random.Range(0.75f, 1.5f);
    }
    void Follow()
    {
        MoveToDir(AttentionPath.instance.GetBestDirection(transform.position));
    }

    delegate float ChanceFun();
    ChanceFun[] chanceFuns;

    enum EState
    {
        EIdle,
        ESpin,
        EFollow,
        ECount,
    }
    EState currentState = EState.EIdle;
    EState GetNewState()
    {
        float sum = 0;
        foreach (var it in chanceFuns)
            sum += it();

        float randed = Random.Range(0, sum);

        float current = 0;
        for (int i = 0; i < chanceFuns.Length; ++i)
        {
            float chance = chanceFuns[i]();
            if (randed >= current && randed < current + chance)
                return (EState)i;
            else
                current += chance;
        }

        return EState.EIdle;
    }

    new private void Start()
    {
        base.Start();
        chanceFuns = new ChanceFun[(int)EState.ECount];
        chanceFuns[(int)EState.EIdle] = IdleChance;
        chanceFuns[(int)EState.EFollow] = FollowChance;
        chanceFuns[(int)EState.ESpin] = SpinChance;
    }
    new private void FixedUpdate()
    {
        if(tStayInState.IsReadyRestart())
        {
            currentState = GetNewState();
            switch (currentState)
            {
                case EState.EIdle:
                    IdleInit();
                    break;
                case EState.ESpin:
                    SpinInit();
                    break;
                case EState.EFollow:
                    FollowInit();
                    break;
            }
        }

        switch(currentState)
        {
            case EState.EIdle:
                Idle();
                break;
            case EState.ESpin:
                Spin();
                break;
            case EState.EFollow:
                Follow();
                break;
        }
    }
}
