namespace Invite.Entities.Requests;

public class CommentCreateRequest
{
    public string Content { get; set; } = default!;
    public int Stars { get; set; }
}
