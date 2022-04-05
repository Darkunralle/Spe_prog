using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour, ILootBox
{
    [SerializeField, Tooltip("La clé du coffre")]
    private KeyType m_key;

    [SerializeField, Tooltip("Animation du coffre")]
    private Animator m_animator;

    private string m_openTriggerName = "Open";
    private int m_openHash;

    public bool OpenChest(out KeyType o_key)
    {
        bool keyFounded = false;
        //Loot de la clé m_key
        if (m_key == null)
        {
            Debug.Log("Pas de clé pour toi bebou");
        }
        else
        {
            keyFounded = true;
            Debug.Log($"Tu as reçu la clé {m_key}");
            m_animator?.SetTrigger(m_openHash);
        }
        o_key = m_key;
        return keyFounded;
    }

    private void Awake()
    {
        if(m_animator == null)
        {
            m_animator = GetComponent<Animator>();
            if(m_animator == null)
            {
                Debug.Log("Gros noob bebou");
                throw new System.ArgumentNullException();
            }
        }
        m_openHash = Animator.StringToHash(m_openTriggerName);
    }
}