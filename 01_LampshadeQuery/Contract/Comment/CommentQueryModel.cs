﻿namespace _01_LampshadeQuery.Contract.Comment;

public class CommentQueryModel {
    public string Name { get; set; }
    public string Message { get; set; }
    public long Id { get; set; }
    public string CreationDate { get; set; }
    public long ParentId { get; set; }
    public string ParentName { get; set; }
    public List<CommentQueryModel> Children { get; set; }
}