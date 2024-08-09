namespace Invite.Entities.Requests;

public class PaginationRequest
{
    const int MaxPageSize = 30;
    public int PageNumebr { get; set; } = 1;
    private int _pageSize;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
