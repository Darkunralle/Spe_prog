using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    [SerializeField, Tooltip("Type de clés")]
    private KeyType m_neededKey;

    [SerializeField, Tooltip("Animation de la porte")]
    private Animator m_animator;

    private string m_openTriggerName = "Open";
    private int m_openHash;
    public void OpenDoor(List<KeyType> p_playerKeys)
    {
        if (m_neededKey)
        {
            //check si le joueur n'a pas la clé nécessaire
            if (p_playerKeys == null || !p_playerKeys.Contains(m_neededKey))
            {
                Debug.Log($"La clé{m_neededKey.name} est nécessaire");
                return;
            }
        }
        
        // ouvre la porte
        Debug.Log("je m'ouvre");
        m_animator?.SetTrigger(m_openHash);
        //Destroy(gameObject);
    }

    private void Awake()
    {
        // test pour trouver l'animator si on ne l'as pas mis sur le serialized field
        if(m_animator == null)
        {
            m_animator = GetComponent<Animator>();
            if(m_animator == null)
            {
                Debug.Log("Gros noob");
                throw new System.ArgumentNullException();
            }
        }
        m_openHash = Animator.StringToHash(m_openTriggerName);
    }
}