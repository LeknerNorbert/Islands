using Islands.Models.Enums;

namespace Islands.DTOs
{
    public class AdDTO
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
        public Item ReplacementItem { get; set; }
        public int ReplacementAmount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
