namespace Common.Models
{
    public interface IOrderItem
    {
        public string DropZone { get; set; }
        public int Order { get; set; }
        public string DisplayName { get; set; }
    }
}
