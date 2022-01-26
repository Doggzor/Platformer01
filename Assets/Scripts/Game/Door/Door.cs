using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Dungeon
{
    public class Door : MonoBehaviour
    {
        [SerializeField] Color color;
        public bool isFading = false;
        private float fadeOutTimerStart = 0.7f;
        private float fadeOutTimerCurrent;
        private Tilemap tiles;
        private void Awake()
        {
            tiles = transform.Find("Tiles").GetComponent<Tilemap>();
            fadeOutTimerCurrent = fadeOutTimerStart;
        }
        private void Update()
        {
            if (isFading)
            {
                FadeOut();
            }
        }
        public void FadeOut()
        {
            fadeOutTimerCurrent -= Time.deltaTime;
            color.a = fadeOutTimerCurrent / fadeOutTimerStart;
            tiles.color = color;
            if (fadeOutTimerCurrent <= 0)
            {
                Destroy(gameObject);
            }
        }

        //For Inspector
        public void ApplyColor()
        {
            transform.Find("Tiles").GetComponent<Tilemap>().color = color;
            transform.Find("Key").GetComponent<SpriteRenderer>().color = color;
        }
       
    }
}
