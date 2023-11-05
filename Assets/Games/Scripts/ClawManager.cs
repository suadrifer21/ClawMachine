using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawManager : MonoBehaviour
{    
    public static ClawManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private ClawState state;

    public delegate void OnClawStateChangeDelegate(ClawState state);
    public event OnClawStateChangeDelegate OnClawStateChenge;

    [SerializeField] private Rope rope;
    [SerializeField] private Rewards rewards;
    private List<GameObject> grabbedRewards = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(ClawOpen());
        rope.OnClawGrabChange += GrabChangeHandler;
    }
    private void GrabChangeHandler(bool isGrabbing)
    {
        if (isGrabbing)
        {
            StartCoroutine(ClawClosed());
        }
        else
        {
            StartCoroutine(CheckGrabbedObject());
        }
    }
    private IEnumerator ClawOpen()
    {
        yield return new WaitForSeconds(.5f);
        state = ClawState.Open;
        OnClawStateChenge?.Invoke(state);
    }
    private IEnumerator ClawClosed()
    {
        state = ClawState.Close;
        OnClawStateChenge?.Invoke(state);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(ClawRaising());        
    }
    private IEnumerator ClawRaising()
    {
        state = ClawState.Raising;
        OnClawStateChenge?.Invoke(state);
        rope.Retract();
        yield break;
    }
    private IEnumerator CheckGrabbedObject()
    {
        if (grabbedRewards.Count > 0)
            rewards.ShowRewardsPanel(grabbedRewards);
        yield return new WaitForSeconds(.5f);
        ClearGrabbedRewards();
        StartCoroutine(ClawOpen());
    }
    public void AddGrabbedRewards(GameObject g)
    {
        grabbedRewards.Add(g);
    }
    private void ClearGrabbedRewards()
    {
        for(int i = 0; i < grabbedRewards.Count; i++)
        {
            //grabbedRewards[i].SetActive(false);
            Destroy(grabbedRewards[i]);
        }
        grabbedRewards.Clear();
    }
}

public enum ClawState
{
    Open,
    Close,
    Raising
}
