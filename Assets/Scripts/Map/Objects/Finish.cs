using System;
using UnityEngine;
namespace FindTheWay.Map.Objects
{
    /// <summary>
    /// This class trigger finishing of runner 
    /// I decided to give him an oopotunity to celebrate his finish 
    /// </summary>
    public class Finish : MonoBehaviour
    {
        [SerializeField] ParticleSystem salut;

        public event Action<Vector3> OnSelebrateFinish;

        private void OnTriggerEnter(Collider other)
        {
            OnSelebrateFinish?.Invoke(transform.position);
            salut.Play();
        }
    }

}
