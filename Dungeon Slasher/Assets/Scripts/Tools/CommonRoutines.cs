using System.Collections;
using UnityEngine;

namespace Dodelie.Tools
{
    public static class CommonRoutines
    {
        public static IEnumerator WaitForSeconds(float time, Event onFinish)
        {
            yield return new WaitForSeconds(time);
            onFinish.Invoke();
        }

        public static IEnumerator Progression(float time, ProgressionEvent onTick, Event onFinish)
        {
            var timer = 0f;

            while (timer < time)
            {
                timer += Time.deltaTime;
                onTick.Invoke(timer / time);
                yield return null;
            }
            onFinish.Invoke();
        }

        public static IEnumerator DoubleProgression(float time, float subTime, Event onTick, Event onFinish)
        {
            var timer = 0f;
            var subTimer = 0f;

            while (timer < time)
            {
                timer += Time.deltaTime;
                while (subTimer < subTime)
                {
                    subTimer += Time.deltaTime;
                    yield return null;
                }
                onTick.Invoke();
                subTimer = 0f;
            }
            onFinish.Invoke();
        }

        public static IEnumerator CustomProgression(float time, AnimationCurve curve, ProgressionEvent onTick, Event onFinish)
        {
            var timer = 0f;

            while (timer < time)
            {
                timer += Time.deltaTime;
                onTick.Invoke(curve.Evaluate(timer / time));
                yield return null;
            }
            onFinish.Invoke();
        }

        public delegate void Event();
        public delegate void ProgressionEvent(float progression);
    }
}