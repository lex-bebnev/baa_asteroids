using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Asteroids.Engine.V2.EntitySystem;

namespace Asteroids.Engine.V2.Managers
{
    /// <summary>
    ///     Class ComponentTypeManager
    /// </summary>
    public static class ComponentTypeManager
    {
        /// <summary>
        ///     The component types.
        /// </summary>
        private static readonly Dictionary<Type, ComponentType> ComponentTypes = new Dictionary<Type, ComponentType>();
        
        /// <summary>
        ///     Ensure that the given component type is "registered" component type for solution
        /// </summary>
        /// <typeparam name="T">Component for which you want the component type.</typeparam>
        /// <returns>Component Type.</returns>
        public static ComponentType GetTypeFor<T>() where T : IEntityComponent
        {
            return GetTypeFor(typeof(T));
        }
        
        /// <summary>
        ///      Ensure that the given component type is "registered" component type for solution
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static ComponentType GetTypeFor(Type component)
        {
            Debug.Assert(component != null, "Component must not be null.");
            ComponentType result;
            if (!ComponentTypes.TryGetValue(component, out result))
            {
                result = new ComponentType();
                ComponentTypes.Add(component, result);
            }

            return result;
        }

        public static void Initialize(params Assembly[] assembliesToScan)
        {
            
        }
        
        public static void Initialize(IEnumerable<Type> types, bool ignoreInvalidTypes = false)
        {
            
        }

        internal static IEnumerable<Type> GetTypesFromBits(uint bits)
        {
            foreach (KeyValuePair<Type,ComponentType> keyValuePair in ComponentTypes)
            {
                if ((keyValuePair.Value.Bit & bits) != 0)
                {
                    yield return keyValuePair.Key;
                }
            }
        }
        
        /// <summary>
        ///     Sets the type for specified ComponentType T.
        /// </summary>
        /// <typeparam name="T">
        ///     The <see langword="Type" /> of T.
        /// </typeparam>
        /// <param name="type">The type.</param>
        internal static void SetTypeFor<T>(ComponentType type)
        {
            ComponentTypes.Add(typeof(T), type);
        }
    }
}