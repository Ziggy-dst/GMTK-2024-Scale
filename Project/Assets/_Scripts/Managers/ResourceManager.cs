using System;
using _Scripts.Managers;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // public static Action<ResourceType> OnResourceGained;

    private int _purpleResource;
    public int PurpleResource
    {
        get => _purpleResource;
        set
        {
            _purpleResource = value;
            GameManager.Instance.UIManager.UpdateResource(value);
        }
    }

    private int _yellowResource;
    public int YellowResource
    {
        get => _yellowResource;
        set
        {
            _yellowResource = value;
            GameManager.Instance.UIManager.UpdateResource(value);
        }
    }

    private int _greenResource;
    public int GreenResource
    {
        get => _greenResource;
        set
        {
            _greenResource = value;
            GameManager.Instance.UIManager.UpdateResource(value);
        }
    }

    public void GainResource(ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Green:
                GreenResource += amount;
                PrintResource();
                break;
            case ResourceType.Purple:
                PurpleResource += amount;
                PrintResource();
                break;
            case ResourceType.Yellow:
                YellowResource += amount;
                PrintResource();
                break;
        }
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
