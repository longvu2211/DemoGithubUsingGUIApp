namespace WebAPIDemo.Dto
{
    public class AccountMemberDto
    {
        public int AccountId { get; set; }

        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? EmailAddress { get; set; }
    }
}
