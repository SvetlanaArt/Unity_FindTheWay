using UnityEngine;
namespace FindTheWay.Character
{
    /// <summary>
    /// Play audio for character
    /// </summary>
    public class CharacterAudio : MonoBehaviour
    {
        [SerializeField] AudioClip footStepAudio;
        [SerializeField] float volume = 1f;


        public void OnFootstep()
        {
            AudioSource.PlayClipAtPoint(footStepAudio, transform.position, volume);
        }

    }
}

