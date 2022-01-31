using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dungeon
{
    public class PlayerActions
    {
        private Player player;
        private Rigidbody2D rb;
        private BoxCollider2D collider;
        public PlayerActions(Player p)
        {
            player = p;
            rb = p.GetComponent<Rigidbody2D>();
            collider = p.GetComponent<BoxCollider2D>();
        }
        public void Move()
        {
            rb.AddForce(new Vector2(player.PlayerInput.directionX * player.Stats.acceleration, 0), ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Min(Mathf.Abs(rb.velocity.x), player.Stats.speed) * player.PlayerInput.directionX, rb.velocity.y);
        }
        public void Jump()
        {
            //Reset some values before performing the actual jump
            player.PlayerInput.jumpPressTime = -1f; ; //Important for consistent jump heights
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = player.Stats.gravityScale;
            //Actual jump
            rb.AddForce(new Vector2(0, player.Stats.jumpForce), ForceMode2D.Impulse);
        }
        public void Flip()
        {
            player.transform.localScale *= new Vector2(-1, 1);
        }
        public IEnumerator Co_TriggerDeath()
        {
            collider.enabled = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(Random.Range(-4f, 4f), 10f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
