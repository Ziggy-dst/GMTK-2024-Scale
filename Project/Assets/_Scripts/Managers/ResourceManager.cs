using System;
using _Scripts.Managers;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // public static Action<ResourceType> OnResourceGained;

    private float _purpleResource;
    public float PurpleResource
    {
        get => _purpleResource;
        set
        {
            _purpleResource = value;
            GameManager.Instance.UIManager.UpdateResource(ResourceType.Purple, value);
        }
    }

    private float _yellowResource;
    public float YellowResource
    {
        get => _yellowResource;
        set
        {
            _yellowResource = value;
            GameManager.Instance.UIManager.UpdateResource(ResourceType.Yellow, value);
        }
    }

    private float _greenResource;
    public float GreenResource
    {
        get => _greenResource;
        set
        {
            _greenResource = value;
            GameManager.Instance.UIManager.UpdateResource(ResourceType.Green, value);
        }
    }

    public void GainResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Green:
                GreenResource += amount;
                break;
            case ResourceType.Purple:
                PurpleResource += amount;
                break;
            case ResourceType.Yellow:
                YellowResource += amount;
                break;
        }
        // PrintResource();
    }

    public void ConsumeResource(ResourceType resourceType, float consumeRate)
    {
        switch (resourceType)
        {
            case ResourceType.Green:
                if (GreenResource <= 0) GreenResource = 0;
                else GreenResource -= consumeRate * Time.deltaTime;
                break;
            case ResourceType.Purple:
                if (PurpleResource <= 0) PurpleResource = 0;
                else PurpleResource -= consumeRate * Time.deltaTime;
                break;
            case ResourceType.Yellow:
                if (YellowResource <= 0) YellowResource = 0;
                else YellowResource -= consumeRate * Time.deltaTime;
                break;
        }
        PrintResource();
    }

    void PrintResource()
    {
        print("GreenResource: " + GreenResource);
        print("PurpleResource: " + PurpleResource);
        print("YellowResource: " + YellowResource);
        print("-----------------------");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
