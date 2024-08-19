using System;
using _Scripts.Managers;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // public static Action<ResourceType> OnResourceGained;
    public float maxResourceAmount = 10f;

    private float _redResource;
    public float RedResource
    {
        get => _redResource;
        set
        {
            _redResource = Mathf.Clamp(value, 0f, maxResourceAmount - 1);
            GameManager.Instance.UIManager.UpdateResource(ResourceType.Red, value);
        }
    }

    private float _blueResource;
    public float BlueResource
    {
        get => _blueResource;
        set
        {
            _blueResource = Mathf.Clamp(value, 0f, maxResourceAmount - 1);
            GameManager.Instance.UIManager.UpdateResource(ResourceType.Blue, value);
        }
    }

    private float _greenResource;
    public float GreenResource
    {
        get => _greenResource;
        set
        {
            _greenResource = Mathf.Clamp(value, 0f, maxResourceAmount - 1);
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
            case ResourceType.Red:
                RedResource += amount;
                break;
            case ResourceType.Blue:
                BlueResource += amount;
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
            case ResourceType.Red:
                if (RedResource <= 0) RedResource = 0;
                else RedResource -= consumeRate * Time.deltaTime;
                break;
            case ResourceType.Blue:
                if (BlueResource <= 0) BlueResource = 0;
                else BlueResource -= consumeRate * Time.deltaTime;
                break;
        }
        // PrintResource();
    }

    void PrintResource()
    {
        print("GreenResource: " + GreenResource);
        print("PurpleResource: " + RedResource);
        print("YellowResource: " + BlueResource);
        print("-----------------------");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
