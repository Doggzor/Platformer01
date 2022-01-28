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
        private float fadeOutTimer = 0.7f;
        private Color currentColor;
        private void Awake()
        {
            tiles = GetComponent<Tilemap>();
            if (tiles.color.a < 1)
            {
                Debug.LogWarning($"Color alpha component of {gameObject.name} not set to 100%, it might behave different than expected.");
            }
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
            StartCoroutine(Co_FadeOut());
        }
        private IEnumerator Co_FadeOut()
        {
            isFading = true;
            yield return new WaitForSeconds(fadeOutTimer);
            Destroy(gameObject);
        }

        private void ColorFade()
        {
            currentColor.a -= Time.deltaTime / fadeOutTimer;
            tiles.color = currentColor;
        }
    }
}
