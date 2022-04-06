using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dungeon
{
    public class PlayerActions
    {
        private readonly Player player;
        private readonly Rigidbody2D rb;
        private readonly BoxCollider2D collider;
        public PlayerActions(Player p)
        {
            player = p;
            rb = p.GetComponent<Rigidbody2D>();
            collider = p.GetComponent<BoxCollider2D>();
        }
        public void Flip() => player.transform.localScale *= (Vector2.left + Vector2.up);
        public void SetGravity(float value) => rb.gravityScale = value;
        public void Move()
        {
            rb.AddForce(player.PlayerInput.directionX * player.Stats.acceleration * Vector2.right, ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Min(Mathf.Abs(rb.velocity.x), player.Stats.speed) * player.PlayerInput.directionX, rb.velocity.y);
        }
        public void Jump()
        {
            //Reset some values before performing the actual jump
            player.PlayerInput.jumpPressTime = -1f; ; //Important for consistent jump heights
            rb.velocity *= Vector3.right; //Reset Velocity.y to 0
            //Actual jump
            rb.AddForce(Vector2.up * player.Stats.jumpForce, ForceMode2D.Impulse);
        }
        public void Bounce(float bounceForce)
        {
            //Reset some values before performing the actual bounce
            player.PlayerInput.jumpPressTime = -1f; ; //Important for consistent bounce heights
            rb.velocity *= Vector3.right; //Reset Velocity to 0
            //Actual bounce
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
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
