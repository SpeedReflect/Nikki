using GlobalLib.Core;



namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT { get => GameINT.Carbon; }

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR { get => GameINT.Carbon.ToString(); }

        /// <summary>
        /// Database to which the class belongs to.
        /// </summary>
        public Database.Carbon Database { get; set; }
    }
}