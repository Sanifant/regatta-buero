namespace LRV.Regatta.Buero.Models
{
    public class FinishObject
    {
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstPath { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string SecondPath { get; internal set; }
    }
}
