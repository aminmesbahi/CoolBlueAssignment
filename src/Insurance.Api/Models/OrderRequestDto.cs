using System.Collections.Generic;

namespace Insurance.Api.Models;

public record OrderRequestDto
{
    public IEnumerable<OrderRequestItem> Items { get; set; } = new List<OrderRequestItem>();
}
public record OrderRequestItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}