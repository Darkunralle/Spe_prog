using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("Le layer des portes")]
    private LayerMask m_layerDoor;

    [SerializeField, Tooltip("Le layer des chests")]
    private LayerMask m_layerChest;

    [SerializeField, Tooltip("Le characontroller")]
    private CharacterController m_charaController;

    private float m_dirX;
    private float m_dirY;

    [SerializeField, Tooltip("la vitesse de déplacement du player")]
    private float m_speed;

    [SerializeField, Tooltip("Trousseau de clé")]
    private List<KeyType> m_trousseauKey = new List<KeyType>();

    private void Update()
    {
        m_dirX = Input.GetAxis("Horizontal");
        m_dirY = Input.GetAxis("Vertical");

        float deltaSpeed = m_speed * Time.deltaTime;
        m_charaController.Move(new Vector3(m_dirX * deltaSpeed, 0, m_dirY * deltaSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((m_layerChest.value & (1 << other.gameObject.layer)) > 0)
        {
            LootBox myLootBox = other.GetComponent<LootBox>();
            if (myLootBox != null && myLootBox.OpenChest(out KeyType key))
            {
                if (!m_trousseauKey.Contains(key))
                {
                    m_trousseauKey.Add(key);
                }
            }
        }
        else if ((m_layerDoor.value & (1 << other.gameObject.layer)) > 0)
        {
            Door myDoor = other.GetComponent<Door>();
            if (myDoor)
            {
                myDoor.OpenDoor(m_trousseauKey);
            }
        }
    }
}