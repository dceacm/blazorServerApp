namespace BlazorServerApp.Data
{
    public record class Error
    {
        public int StatusCode { get; set; }
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string StackTrace { get; set; } = null!;
    }
}
