using BlazorSplitGrid.Elements;
using BlazorSplitGrid.Extensions;
using BlazorSplitGrid.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorSplitGrid;

public partial class SplitGrid : ComponentBase
{
    [Parameter]
    public string? Id { get; set; }

    [Parameter]
    public EventCallback<DragEventArgs> OnDrag { get; set; }

    [Parameter]
    public EventCallback<DragEventArgs> OnDragStart { get; set; }

    [Parameter]
    public EventCallback<DragEventArgs> OnDragStop { get; set; }

    [Parameter] 
    public RenderFragment? ChildContent { get; set; }

    [Parameter] 
    public int? MinSize { get; set; }

    [Parameter] 
    public int? MaxSize { get; set; }

    [Parameter] 
    public int? ColumnMinSize { get; set; }

    [Parameter] 
    public int? ColumnMaxSize { get; set; }

    [Parameter] 
    public Dictionary<int, int>? ColumnMinSizes { get; set; } = new();

    [Parameter] 
    public Dictionary<int, int>? ColumnMaxSizes { get; set; } = new();

    [Parameter] 
    public int? RowMinSize { get; set; }

    [Parameter] 
    public int? RowMaxSize { get; set; }

    [Parameter] 
    public Dictionary<int, int>? RowMinSizes { get; set; } = new();

    [Parameter]
    public Dictionary<int, int>? RowMaxSizes { get; set; } = new();

    [Parameter] 
    public int? SnapOffset { get; set; }

    [Parameter] 
    public int? ColumnSnapOffset { get; set; }

    [Parameter] 
    public int? RowSnapOffset { get; set; }

    [Parameter] 
    public int? DragInterval { get; set; }
    
    [Parameter] 
    public int? ColumnDragInterval { get; set; }

    [Parameter] 
    public int? RowDragInterval { get; set; }

    [Parameter] 
    public string? Cursor { get; set; }

    [Parameter] 
    public string? ColumnCursor { get; set; }

    [Parameter] 
    public string? RowCursor { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    public ElementReference Element { get; set; }

    public string SplitGridId { get; }

    public string Classes => AttributeBuilder.New()
        .Append("split-grid")
        .Append(SplitGridId)
        .Append(Class)
        .Build();

    public string Styles => AttributeBuilder.New()
        .Append(Style)
        .Build();

    private readonly Dictionary<string, GutterItem> _columns = new();
    private readonly Dictionary<string, GutterItem> _rows = new();

    private SplitGridInterop? _splitGrid;

    public SplitGrid()
    {
        SplitGridId = $"split-grid-{Guid.NewGuid().ToString()}";
    }

    protected override Task OnInitializedAsync()
    {
        _splitGrid = new SplitGridInterop(JsRuntime);
        return base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
            await Initialise();
    }

    public async Task Initialise()
    {
        if (_splitGrid is null)
            return;

        var options = new SplitGridOptions
        {
            MinSize = MinSize,
            MaxSize = MaxSize,
            ColumnMinSize = ColumnMinSize,
            ColumnMaxSize = ColumnMaxSize,
            ColumnMinSizes = ColumnMinSizes,
            ColumnMaxSizes = ColumnMaxSizes,
            RowMinSize = RowMinSize,
            RowMaxSize = RowMaxSize,
            RowMinSizes = RowMinSizes,
            RowMaxSizes = RowMaxSizes,
            SnapOffset = SnapOffset,
            ColumnSnapOffset = ColumnSnapOffset,
            RowSnapOffset = RowSnapOffset,
            DragInterval = DragInterval,
            RowDragInterval = RowDragInterval,
            ColumnDragInterval = ColumnDragInterval,
            Cursor = Cursor,
            ColumnCursor = ColumnCursor,
            RowCursor = RowCursor,
            HasOnDrag = OnDrag.HasDelegate,
            HasOnDragStart = OnDragStart.HasDelegate,
            HasOnDragStop = OnDragStop.HasDelegate
        };

        await _splitGrid.Initialise(_rows.Values, _columns.Values, options);
        _splitGrid.OnDrag += (_, args) => OnDrag.InvokeAsync(args);
        _splitGrid.OnDragStart += (_, args) => OnDragStart.InvokeAsync(args);
        _splitGrid.OnDragStop += (_, args) => OnDragStop.InvokeAsync(args);
    }

    public async Task Refresh()
    {
        await InvokeAsync(StateHasChanged);
    }

    public async Task AddColumnGutter(string querySelector, int track)
    {
        if (_splitGrid is null)
            return;

        await _splitGrid.AddColumnGutter(querySelector, track);
    }

    public async Task AddRowGutter(string querySelector, int track)
    {
        if (_splitGrid is null)
            return;

        await _splitGrid.AddRowGutter(querySelector, track);
    }

    public async Task RemoveColumnGutter(string querySelector, int track, bool immediate = true)
    {
        if (_splitGrid is null)
            return;

        await _splitGrid.RemoveColumnGutter(querySelector, track, immediate);
    }

    public async Task RemoveRowGutter(string querySelector, int track, bool immediate = true)
    {
        if (_splitGrid is null)
            return;

        await _splitGrid.RemoveRowGutter(querySelector, track, immediate);
    }

    public async Task Destroy(bool immediate = true)
    {
        if (_splitGrid is null)
            return;

        await _splitGrid.Destroy(immediate);
    }

    internal GutterItem AddRow(SplitGridGutter gutter)
    {
        var gutterItem = new GutterItem(gutter.SplitGridId, _rows.NextTrack());

        if (gutter.MinSize.HasValue)
        {
            RowMinSizes ??= new Dictionary<int, int>();
            RowMinSizes[gutterItem.Track] = gutter.MinSize.Value;
        }

        if (gutter.MaxSize.HasValue)
        {
            RowMaxSizes ??= new Dictionary<int, int>();
            RowMaxSizes[gutterItem.Track] = gutter.MaxSize.Value;
        }

        return _rows[gutter.SplitGridId] = gutterItem;
    }

    internal GutterItem AddColumn(SplitGridGutter gutter)
    {
        var gutterItem = new GutterItem(gutter.SplitGridId, _columns.NextTrack());

        if (gutter.MinSize.HasValue)
        {
            ColumnMinSizes ??= new Dictionary<int, int>();
            ColumnMinSizes[gutterItem.Track] = gutter.MinSize.Value;
        }

        if (gutter.MaxSize.HasValue)
        {
            ColumnMaxSizes ??= new Dictionary<int, int>();
            ColumnMaxSizes[gutterItem.Track] = gutter.MaxSize.Value;
        }

        return _columns[gutter.SplitGridId] = gutterItem;
    }
}
