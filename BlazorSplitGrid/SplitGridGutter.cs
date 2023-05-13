using BlazorSplitGrid.Elements;
using BlazorSplitGrid.Extensions;
using BlazorSplitGrid.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorSplitGrid;

public abstract class SplitGridGutter : ComponentBase
{
    [CascadingParameter]
    public SplitGrid SplitGrid { get; set; } = null!;

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    private string Classes => AttributeBuilder.New()
        .Append("split-grid-gutter")
        .Append($"split-grid-gutter-{_direction.ToClassName()}")
        .ConditionalAppend(() => _item is not null, $"split-grid-gutter-{_direction.ToClassName()}-{_item!.Track}")
        .Append(Class)
        .Build();

    private readonly Direction _direction;
    private readonly string _id;
    private GutterItem? _item;

    protected SplitGridGutter(Direction direction)
    {
        _direction = direction;
        _id = $"split-grid-gutter-{Guid.NewGuid().ToString()}";
    }

    protected override void OnInitialized()
    {
        _item = _direction == Direction.Row ? SplitGrid.AddRow(_id) : SplitGrid.AddColumn(_id);
    }

    public async Task Refresh()
    {
        await InvokeAsync(StateHasChanged);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "id", _id);
        builder.AddAttribute(2, "class", Classes);
        builder.AddAttribute(3, "style", Style);
        builder.CloseElement();
    }
}
