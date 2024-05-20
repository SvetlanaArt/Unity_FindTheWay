using System;
using System.Collections.Generic;
using UnityEngine;

namespace FindTheWay.Character
{
    /// <summary>
    /// Spawn a runner onto pointA
    /// </summary>
    public class SpawnCharacter : MonoBehaviour
    {
        [SerializeField] GameObject character;

        public event Action OnRunnerFinished;

        private Runner run;

        public void CreateRunner(List<Vector3> path)
        {
            if (path == null)
                return;
            GameObject runner = Instantiate(
                                    character,
                                    path[0],
                                    Quaternion.identity,
                                    this.transform);
            if (runner == null)
                return;
            run = runner.GetComponent<Runner>();

            run.OnFinished += RunnerFinish;
            run.RunPath(path);
        }

        private void RunnerFinish()
        {
            run.OnFinished -= RunnerFinish;
            OnRunnerFinished?.Invoke();
        }
    }

}
