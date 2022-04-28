namespace CommentManagement.Application.Contract.Comment {
    public class AddComment {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Website { get; set; }
        public long ParentId { get; set; }
        public int Type { get; set; }
        public long RecordOwnerId { get; set; }
    }
}
