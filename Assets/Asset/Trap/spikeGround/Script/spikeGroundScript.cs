using UnityEngine;

public class spikeGroundScript : MonoBehaviour
{
    /// <summary>
    /// Vérifie si le joueur est dans la zone des pics.
    /// </summary>
    bool _playerIn;

    /// <summary>
    /// Référence au script gérant l’animation des pics.
    /// </summary>
    private SpikeAnimationState _spikeAnimationState;

    /// <summary>
    /// Vérifie si le joueur vient de recevoir des dégâts.
    /// </summary>
    bool _hurt = false;

    /// <summary>
    /// Entité du joueur touché par les pics.
    /// </summary>
    Entity _entity;

    /// <summary>
    /// GameObject du joueur touché par les pics.
    /// </summary>
    private GameObject _player;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        _spikeAnimationState = GetComponentInChildren<SpikeAnimationState>();
    }

    // Update is called once per frame

    /// <summary>
    /// Inflige des dégâts au joueur touché
    /// et le repousse légèrement.
    /// </summary>
    public void EnleverDegat()
    {
        if(_entity != null)
        {
            _entity.TakeDamage(1);
            _player.GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 5), ForceMode2D.Impulse);
            _hurt = true;
        }
        else
        {
            Debug.Log("Player est null");
        }
    }

    /// <summary>
    /// Met l’état de dégâts à true.
    /// </summary>
    void Hurt()
    {
        _hurt = true;
    }

    /// <summary>
    /// Réinitialise l’état de dégâts.
    /// </summary>
    public void ResetHurt()
    {
        _hurt = false;
    }

    /// <summary>
    /// Fonction appelée lorsqu’une collision commence avec les pics.
    /// </summary>
    /// <param name="collision">
    /// Collision détectée.
    /// </param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnTriggerEnter2D" + collision.gameObject.name);

        _player = collision.gameObject;

        // Vérifie si l’objet touché est l’ours
        if(_player.name == "Ours")
        {
            _entity = _player.GetComponentInParent<Bear>();
        }

        // Vérifie si l’objet touché est le druide
        if(_player.name == "Druide")
        {
            _entity = _player.GetComponentInParent<Player>();
        }

        // Vérifie si l’objet touché est le poisson
        if(_player.name == "Poisson")
        {
            _entity = _player.GetComponentInParent<Fish>();
        }

        // Vérifie si l’objet touché est l’oiseau
        if(_player.name == "Oiseau")
        {
            _entity = _player.GetComponentInParent<Bird>();
        }

        // Vérifie si une entité valide est détectée
        if (_entity != null && _entity.Hurt == false)
        {
            Debug.Log("Player touché par les piques");
            EnleverDegat();
        }
    }

    /// <summary>
    /// Fonction appelée lorsqu’une collision se termine avec les pics.
    /// </summary>
    /// <param name="collision">
    /// Collision qui se termine.
    /// </param>
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnTriggerExit2D" + collision.gameObject.name);

        _player = null;
        _entity = null;
    }
}