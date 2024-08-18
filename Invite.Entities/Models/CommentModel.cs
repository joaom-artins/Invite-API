using System.ComponentModel.DataAnnotations;

namespace Invite.Entities.Models;

public class CommentModel
{
    public Guid Id { get; set; }
    public UserModel User { get; set; } = default!;
    public Guid UserId { get; set; }
    public string Content { get; set; } = default!;
    public HallModel? Hall { get; set; }
    public Guid? HallId { get; set; }
    public BuffetModel? Buffet { get; set; }
    public Guid? BuffetId { get; set; }
    public CerimonialistModel? Cerimonialist { get; set; }
    public Guid? CerimonialistId { get; set; }
    [Range(0, 5)]
    public int Stars { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}