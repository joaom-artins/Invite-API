namespace Invite.Entities.Requests;

public class CommentUpdateRequest
{
    public string Content { get; set; } = default!;
    public int Stars { get; set; }
}