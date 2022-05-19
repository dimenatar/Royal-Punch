using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshPaint : MonoBehaviour
{
    [SerializeField] private Renderer _floor;
    [SerializeField] private Transform _enemy;
    [SerializeField] private Material _texture;

    private Texture2D _save;

    private void Start()
    {
        //PaintCircle();
       // PaintTriangle(70);
       // Invoke(nameof(ReturnNormal), 5);
    }

    public void ReturnNormal()
    {
        _texture.mainTexture = _save;
    }

    public void PaintTriangle(int percentage)
    {
        Texture2D texture = _texture.mainTexture as Texture2D;
        //_save = new Texture2D(texture.width, texture.height, );
        float width = texture.width * percentage / 100 / 2;

        int counter = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = -counter; j <= counter; j++)
            {
                texture.SetPixel(i, j, Color.red);
            }
            counter++;
        }
        texture.Apply();
    }

    public void PaintCircle()
    {
        //Texture2D texture = _floor.material.mainTexture as Texture2D;
        Texture2D texture = _texture.mainTexture as Texture2D;
        _save = texture;
        //Texture2D texture = _text.GetTexture("Albedo").;

        //Vector2 pixelUV = _enemy.position;

        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                texture.SetPixel(i, j, Color.black);
            }
        }

       // pixelUV.x *= texture.width;
       // pixelUV.y *= texture.height;

       // print(pixelUV);

        
        texture.Apply();
    }
}
