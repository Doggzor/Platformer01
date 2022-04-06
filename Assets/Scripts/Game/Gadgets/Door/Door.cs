using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Dungeon
{
    public class Door : MonoBehaviour
    {
        private bool isFading = false;
        private Tilemap tiles;
        private readonly float fadeOutTimer = 0.7f;
        private Color currentColor;
        private float alphaPercentage;
        private void Awake()
        {
            tiles = GetComponent<Tilemap>();
            alphaPercentage = tiles.color.a;
            currentColor = tiles.color;
        }
        private void Update()
        {
            if (isFading)
            {
                ColorFade();
            }
        }
        public void FadeOut()
        {
            isFading = true;
            Destroy(gameObject, fadeOutTimer);
        }

        private void ColorFade()
        {
            currentColor.a -= Time.deltaTime / fadeOutTimer * alphaPercentage;
            tiles.color = currentColor;
        }
    }
}
