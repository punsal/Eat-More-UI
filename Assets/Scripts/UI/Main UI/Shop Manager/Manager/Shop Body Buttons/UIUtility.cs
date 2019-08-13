using UnityEngine;

namespace UIUtility
{
    public static class ExtensionRectTransform
    {
        /// <summary>
        /// Modifies Components of a Vector2. Best-Practice is setting return value to original operand. 
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x">(Optional) modifies operand's x attribute.</param>
        /// <param name="y">(Optional) modifies operand's y attribute.</param>
        /// <returns>Initialize return value to method caller operand.</returns>
        /// <seealso cref="Vector2"/>
        public static Vector2 ModifyV2(this Vector2 vector, float? x = null, float? y = null)
        {
            vector = new Vector2(x ?? vector.x, y ?? vector.y);
            return vector;
        }

        /// <summary>
        /// Modifies Components of a Vector2. Best-Practice is setting return value to original operand.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="x">(Optional) modifies operand's x attribute.</param>
        /// <param name="y">(Optional) modifies operand's y attribute.</param>
        /// <param name="z">(Optional) modifies operand's z attribute.</param>
        /// <returns>Initialize return value to method caller operand.</returns>
        /// <seealso cref="Vector3"/>
        public static Vector3 ModifyV3(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            vector = new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
            return vector;
        }
    }
}

/// <summary>
/// Simplifies System.Array methods
/// </summary>
namespace ArrayUtils
{
    /// <summary>
    /// Declares some basic methods for generic arrays.
    /// </summary>
    public static class ObjectArrayExtension
    {
        public static void Clear(this Object[] array)
        {
            System.Array.Clear(array, 0, array.Length);
        }
    }
}