using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    /// <summary>
    /// Déclaration des variables membres et création du Singleton en pvt
    /// 
    /// </summary>

    private static T m_instance = null;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {

                FindOrCreateInstance();
            }
            return m_instance;
        }

        ///PAS BESOIN DU SET DANS CETTE CONDITION
        /// set { m_instance = value; }

    }

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this);
        }
    }


    private static void FindOrCreateInstance()
    {

        m_instance = FindObjectOfType<T>();
        if (m_instance != null) return;

        GameObject go = new GameObject("Singleton");

        // il se retourne lui même donc on peut mettre le m_instance
        m_instance = go.AddComponent<T>();


        go.name = (m_instance as Singleton<T>).GetSingletonName();


    }

    protected abstract string GetSingletonName();
}
