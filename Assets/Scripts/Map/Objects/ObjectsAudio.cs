using UnityEngine;

namespace FindTheWay.Map.Objects
{
    /// <summary>
    /// Play map objects sounds
    /// </summary>
    public class ObjectsAudio : MonoBehaviour
    {
        [SerializeField] AudioClip putObject;
        [SerializeField] AudioClip reachPointB;
        [SerializeField] float volume = 1f;

        public void PlayPutObject(Vector3 position)
        {
            AudioSource.PlayClipAtPoint(putObject, position, volume);
        }

        public void PlayReachPointB(Vector3 position)
        {
            AudioSource.PlayClipAtPoint(reachPointB, position, volume);
        }
    }

}
