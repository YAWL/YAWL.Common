namespace YAWL.Common.Settings
{
    /// <summary>
    /// Describes a generic setting. Used for application settings which are persisted
    /// in the application local storage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISetting<T>
    {
        /// <summary>
        /// Wrapped value is held here.
        /// </summary>
        T Value { get; set; }

        /// <summary>
        /// Settings are stored as key/value pairs.
        /// </summary>
        string SettingName { get; }

        /// <summary>
        /// Call this method to perform actual saving.
        /// </summary>
        void Save();
    }
}
