using UnityEngine;

namespace CMCore.Utilities.Extensions
{
    public static class Vector3Extensions
    {
        // Returns a new Vector3 with the x value set to the given value
        public static Vector3 WithX(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }

        // Returns a new Vector3 with the y value set to the given value
        public static Vector3 WithY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }

        // Returns a new Vector3 with the z value set to the given value
        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }
        

        // Returns a new Vector3 with the given values added to each component
        public static Vector3 Add(this Vector3 vector, float x, float y, float z)
        {
            return new Vector3(vector.x + x, vector.y + y, vector.z + z);
        }

        // Returns a new Vector3 with the given value added to each component
        public static Vector3 Add(this Vector3 vector, float value)
        {
            return new Vector3(vector.x + value, vector.y + value, vector.z + value);
        }

        // Returns a new Vector3 with the given values subtracted from each component
        public static Vector3 Subtract(this Vector3 vector, float x, float y, float z)
        {
            return new Vector3(vector.x - x, vector.y - y, vector.z - z);
        }

        // Returns a new Vector3 with the given value subtracted from each component
        public static Vector3 Subtract(this Vector3 vector, float value)
        {
            return new Vector3(vector.x - value, vector.y - value, vector.z - value);
        }

        // Returns the dot product of two vectors
        public static float Dot(this Vector3 vector1, Vector3 vector2)
        {
            return Vector3.Dot(vector1, vector2);
        }

        // Returns the cross product of two vectors
        public static Vector3 Cross(this Vector3 vector1, Vector3 vector2)
        {
            return Vector3.Cross(vector1, vector2);
        }

        // Returns the angle between two vectors in radians
        public static float Angle(this Vector3 vector1, Vector3 vector2)
        {
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(vector1.normalized, vector2.normalized), -1f, 1f));
        }

        // Returns the projection of the vector onto the given plane
        public static Vector3 ProjectOnPlane(this Vector3 vector, Vector3 planeNormal)
        {
            return vector - Vector3.Project(vector, planeNormal);
        }

        // Returns the reflection of the vector off the given plane
        public static Vector3 Reflect(this Vector3 vector, Vector3 planeNormal)
        {
            return Vector3.Reflect(vector, planeNormal);
        }

        // Returns the vector rotated around the given axis by the given angle in degrees
        public static Vector3 RotateAround(this Vector3 vector, Vector3 axis, float angle)
        {
            return Quaternion.AngleAxis(angle, axis) * vector;
        }
    }
}