using UnityEngine;

namespace Dodelie.Tools
{
    /// <summary>
    /// Abstract class having minimal amount of necessary references to possible input.
    /// </summary>
    public abstract class Controls
    {
        public Vector2 leftInput { get; protected set; }
        public Vector2 rightInput { get; protected set; }
    }
}
