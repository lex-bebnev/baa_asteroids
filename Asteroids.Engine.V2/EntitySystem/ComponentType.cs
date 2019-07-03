using System;
using Asteroids.Engine.V2.Managers;

namespace Asteroids.Engine.V2.EntitySystem
{
    /// <summary>
    ///     Represent a component Type.
    /// </summary>
    public sealed class ComponentType
    {
        /// <summary>
        ///     The bit next instance of the <see cref="ComponentType"/> class will get.
        /// </summary>
        private static UInt64 nextBit;
        
        /// <summary>
        ///     The id next instance of the <see cref="ComponentType"/> class will get.
        /// </summary>
        private static int nextId;

        /// <summary>
        ///     Initialize static members of the <see cref="ComponentType"/> class.
        /// </summary>
        static ComponentType()
        {
            nextBit = 1;
            nextId = 0;
        }
        
        /// <summary>
        ///     Initialize the new instance of the <see cref="ComponentType"/> class.
        /// </summary>
        internal ComponentType()
        {
            this.Id = nextId;
            this.Bit <<= 1;
        }
        
        /// <summary>
        ///     Gets the bit index that represents this type of the component.
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        ///     Gets the bit that represents this type of the component.
        /// </summary>
        public UInt64 Bit { get; private set; }
    }

    /// <summary>
    ///     The component type class.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    internal static class ComponentType<T> where T : IEntityComponent
    {
        /// <summary>
        ///     Initializes static members of the <see cref="ComponentType{T}"/> class.
        /// </summary>
        static ComponentType()
        {
            CType = ComponentTypeManager.GetTypeFor<T>();
            if (CType == null)
            {
                CType = new ComponentType();
                ComponentTypeManager.SetTypeFor<T>(CType);
            }
        }
        
        /// <summary>
        ///     Get the type of the Component
        /// </summary>
        /// <value>
        ///     The type of the Component
        /// </value>
        public static ComponentType CType { get; private set; }
    }
}