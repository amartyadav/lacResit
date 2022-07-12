namespace Compiler.CodeGeneration
{
    /// <summary>
    /// A memory address location
    /// </summary>
    /// <remarks>
    /// Basically this is just a short, but creating a type gives us clearer properties, functions declarations, etc.
    /// </remarks>
    public struct Address
    {
        /// <summary>
        /// The memory location
        /// </summary>
        private short MemoryLocation { get; }

        /// <summary>
        /// Creates a new address
        /// </summary>
        /// <param name="memoryLocation">The memory location of the address</param>
        private Address(short memoryLocation) { MemoryLocation = memoryLocation; }

        /// <summary>
        /// Converts a short to an address
        /// </summary>
        /// <param name="s">The short to convert</param>
        public static implicit operator Address(short s) { return new Address(s); }

        /// <summary>
        /// Converts an address to a short
        /// </summary>
        /// <param name="address">The address to convert</param>
        public static implicit operator short(Address address) { return address.MemoryLocation; }
    }
}