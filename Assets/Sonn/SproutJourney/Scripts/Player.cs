using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sonn.SproutJourney
{
    public class Player : MonoBehaviour
    {
        public GridManager gridManager;
        public float speed;
        public Vector3 pos, scale;

        private Rigidbody m_rb;

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            CreatePosAndScale();    
        }

        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;
            if (moveDirection.sqrMagnitude > 0.01f)
            {
                Vector3 pos = m_rb.position + speed * Time.deltaTime * moveDirection;
                if (gridManager.IsInsideGrid(pos))
                {
                    m_rb.MovePosition(pos);
                }
                else
                {
                    Debug.Log("⚠️ Không thể di chuyển — ngoài ô map!");
                }    
            }
        }
        private void CreatePosAndScale()
        {
            transform.position = new(transform.position.x + pos.x,
                                     transform.position.y + pos.y,
                                     transform.position.z + pos.z);

            transform.localScale = new(transform.localScale.x * scale.x,
                                       transform.localScale.y * scale.y,
                                       transform.localScale.z * scale.z); 
        }    
    }
}

