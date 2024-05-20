using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FindTheWay.Character
{
    /// <summary>
    /// apply running the Runner by the path points
    /// </summary>
    public class Runner : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] Vector3 offset = new Vector3(0.5f, 0f, 0.5f);
        [SerializeField] int delayStartFinish_ms = 700;
        [SerializeField] float speed = 6f;
        [SerializeField] float speedMoveRatio = 0.1f;
        [SerializeField] float speedRotateRatio = 1.5f;

        public event Action OnFinished;


        private void Start()
        {
            animator.applyRootMotion = false;
        }

        public async void RunPath(List<Vector3> path)
        {
            if (path != null)
            {
                transform.position = path[0] + offset;
                await Task.Delay(delayStartFinish_ms);
                for (int point = 1; point < path.Count; point++)
                {
                    path[point] += offset;
                    await Move(path[point], path[0], path[path.Count - 1]);
                }
                animator.SetFloat("Speed", 0f);
                await Task.Delay(delayStartFinish_ms);
            }
            OnFinished?.Invoke();
            Destroy(gameObject);
        }

        private async Task Move(Vector3 target, Vector3 start, Vector3 goal)
        {
            while (transform.position != target)
            {
                Animate(start, goal, transform.position);

                float delta = speed * Time.deltaTime;

                transform.rotation = GetNewRotation(target, transform.position, delta);
                transform.position = GetNewPosition(target, transform.position, delta);

                await Task.Yield();
            }
        }

        private Vector3 GetNewPosition(Vector3 target, Vector3 position, float delta)
        {
            return Vector3.MoveTowards(position, target, delta * speedMoveRatio);
        }

        private Quaternion GetNewRotation(Vector3 target, Vector3 position, float delta)
        {
            Quaternion rotation = Quaternion.LookRotation(target - position);
            return Quaternion.Lerp(transform.rotation, rotation, delta * speedRotateRatio);
        }

        private void Animate(Vector3 start, Vector3 goal, Vector3 position)
        {
            float slowDown = Mathf.Min((position - start).magnitude,
                                        (position - goal).magnitude);
            slowDown = Mathf.Clamp01(Mathf.Abs(slowDown));
            animator.SetFloat("Speed", speed * slowDown);
        }
    }

}
