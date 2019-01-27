using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class MomController : Interactable, IInteractable
{
    public enum Mood
    {
        Happy,
        Angry
    }

    private float currentRate;

    public MomAttributes Attributes;

    public string ID => name;

    public Expectations[] Expectations;

    public GameObject GameObject => gameObject;

    public float ExtressLevel;

    [SerializeField]
    private List<RealExpectations> realExpectationList;

    private RealExpectations[] realExpectations;

    private void Start()
    {
        realExpectationList = new List<RealExpectations>();

        realExpectations = Expectations.Select(exp => {
            return new RealExpectations()
            {
                Interactable = exp.Interactable.GetComponent<IInteractable>(),
                Icon = exp.Icon,
                Name = exp.Name,
                TimeToExtress = exp.PatientMultiply * Attributes.PatienceTime
            };
        }).ToArray();

        ///StartCoroutine(StressController());
        StartCoroutine(TimerController());
    }

    public void Interact(IInteractor interactor)
    {
        var receivedItem = realExpectationList.Find(i => i.Interactable.ID == interactor.CurrentItem.ID);

        if (receivedItem is null)
        {
            ExtressLevel += Attributes.ExtressRate;
        }
        else
        {
            ExtressLevel -= Attributes.ExtressRate * 2f;

            ExtressLevel = ExtressLevel < 0 ? 0 : ExtressLevel;

            var item = interactor.CurrentItem;
            interactor.DropItem();

            Destroy(item.GameObject);

            realExpectationList.Remove(receivedItem);
        }
    }

    public IEnumerator StressController()
    {
        ExtressLevel += Attributes.ExtressRate * 0.1f;

        if(ExtressLevel > 100)
        {
            ExtressLevel = 100;
        }

        yield return new WaitForEndOfFrame();

        StressController();
    }

    public IEnumerator TimerController()
    {
        if(Time.time > currentRate)
        {
            currentRate = Time.time + Attributes.AskRate;

            if(realExpectationList.Count() < Attributes.MaxExpectationCount)
            {
                var index = Random.Range(0, realExpectations.Length - 1);
                var expectation = realExpectations[index];

                expectation.TimeToExtress = Attributes.PatienceTime * Expectations[index].PatientMultiply;

                realExpectationList.Add(expectation.Clone());
            }
        }

        foreach(var expectation in realExpectationList)
        {
            expectation.TimeToExtress -= 0.5f;

            if(expectation.TimeToExtress <= 0)
            {
                expectation.TimeToExtress = 0;

                ExtressLevel += Attributes.ExtressRate * 0.3f;
            }
        }

        if(ExtressLevel > 100)
        {
            ExtressLevel = 100;
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(TimerController());
    }
}

[Serializable]
[CreateAssetMenu(fileName = "MomProfile", menuName = "Game/MonController", order = 1)]
public class MomAttributes: ScriptableObject
{
    public float AskRate = 3.0f;
    public float ExtressRate = 20f;

    public float PatienceTime = 5f;
    public int MaxExpectationCount = 4;
}

[Serializable]
public class Expectations
{
    public GameObject Interactable;
    public Sprite Icon;
    public float PatientMultiply;
    public string Name;
}
[Serializable]
public class RealExpectations
{
    public IInteractable Interactable;
    public Sprite Icon;
    public string Name;
    public float TimeToExtress;

    public RealExpectations Clone()
    {
        return new RealExpectations()
        {
            Interactable = Interactable,
            Icon = Icon,
            Name = Name,
            TimeToExtress = TimeToExtress
        };
    }
}