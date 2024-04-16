using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingGrid : MonoBehaviour
{
    public float scrollSpeed = 0.05f; // Speed at which the texture scrolls
    public float rotationSpeed = 30f; // Speed at which the texture rotates in degrees per second
    private Vector2 offset = Vector2.zero; // Current texture offset
    private Vector2 inputDirection = Vector2.zero; // Current input direction
    private float rotation = 0f; // Current texture rotation in degrees
    private Material material; // Material to modify

    public void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = material.GetTextureOffset("_MainTex");
        rotation = material.GetFloat("_Rotation");
    }

    public void MoveForward()
    {
        inputDirection.y -= 1;
    }

    public void MoveBackward()
    {
        inputDirection.y += 1;
    }

    public void MoveLeft()
    {
        inputDirection.x += 1;
    }

    public void MoveRight()
    {
        inputDirection.x -= 1;
    }

    public void TurnLeft()
    {
        rotation += rotationSpeed * Time.deltaTime;
    }

    public void TurnRight()
    {
        rotation -= rotationSpeed * Time.deltaTime;
    }

    void Update()
    {   
        // Rotate the input direction by the current rotation
        float rad = rotation * Mathf.Deg2Rad; // Convert to radians
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        Vector2 rotatedDirection = new Vector2(
            inputDirection.x * cos - inputDirection.y * sin,
            inputDirection.x * sin + inputDirection.y * cos
        );

        offset += rotatedDirection * scrollSpeed * Time.deltaTime;

        // Apply the offset and rotation to the material's texture
        material.SetTextureOffset("_MainTex", offset);
        material.SetFloat("_Rotation", rotation);
        material.SetFloat("_OffsetX", offset.x);
        material.SetFloat("_OffsetY", offset.y);

        inputDirection = Vector2.zero;
    }

    void OnDisable()
    {
        // Reset the texture offset and rotation when the script is disabled
        material.SetTextureOffset("_MainTex", Vector2.zero);
        material.SetFloat("_Rotation", 0f);
    }
}
