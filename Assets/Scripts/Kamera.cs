using UnityEngine;
using System.Collections;

public class Kamera : MonoBehaviour 
{

    public float IzlīdzināšanasLaiks = 0.15f;
    public float AttālumaIzlīdzināšanasLaiks = 0.05f;
    private Vector3 MistisksĀtrums = Vector3.zero;
    private float AttālumaIzmaiņasĀtrums = 0f;
    private PlayerManager PlayerManager = null;
    private Transform Player;//Spēlētāja atsaite
    /// <summary>
    /// Ortogrāfiskās kameras izmērs
    /// </summary>
    private float Izmērs = 9f;

    void Awake()
    {
        // Atrodam spēlētāju.
        PlayerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        Izmērs = camera.orthographicSize;
    }

    void LateUpdate()
    {
        if (PlayerManager.Player != null)
        {
            Player = PlayerManager.Player.transform;
            //kamera seko tad, ja spēlētājs pastāv un ir aktīvs
            if (Player != null && Player.gameObject.activeInHierarchy == true)
            {
                Vector3 pos = Player.transform.position;
                transform.position = new Vector3(pos.x ,pos.y ,transform.position.z) ;
                AttālināšanāsNoĀtruma();
            }
        }
     }

    void AttālināšanāsNoĀtruma()
    {
        Player player = Player.GetComponent<Player>();
        float ātrumaAttālinājums = (Player.rigidbody2D.velocity.magnitude / player.MaxSpeed);
        float attālinājumaReizinātājs = Mathf.Max(player.MaxSpeed / Izmērs, 1);
        ātrumaAttālinājums *= attālinājumaReizinātājs;
        float mērķaAttālums = Izmērs + ātrumaAttālinājums;
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, mērķaAttālums, ref AttālumaIzmaiņasĀtrums, AttālumaIzlīdzināšanasLaiks);
    }
}
