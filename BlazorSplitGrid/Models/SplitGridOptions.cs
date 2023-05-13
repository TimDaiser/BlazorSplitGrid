using BlazorSplitGrid.Extensions;

namespace BlazorSplitGrid.Models;

public class SplitGridOptions
{
    public int? MinSize { get; set; }
    public int? MaxSize { get; set; }
    public Dictionary<int, int>? MinSizes { get; set; }
    public Dictionary<int, int>? MaxSizes { get; set; }
    public int? ColumnMinSize { get; set; }
    public int? ColumnMaxSize { get; set; }
    public Dictionary<int, int>? ColumnMinSizes { get; set; }
    public Dictionary<int, int>? ColumnMaxSizes { get; set; }
    public int? RowMinSize { get; set; }
    public int? RowMaxSize { get; set; }
    public Dictionary<int, int>? RowMinSizes { get; set; }
    public Dictionary<int, int>? RowMaxSizes { get; set; }
    public int? SnapOffset { get; set; }
    public int? ColumnSnapOffset { get; set; }
    public int? RowSnapOffset { get; set; }
    public int? DragInterval { get; set; }
    public int? ColumnDragInterval { get; set; }
    public int? RowDragInterval { get; set; }
    public string? Cursor { get; set; }
    public string? ColumnCursor { get; set; }
    public string? RowCursor { get; set; }
    public bool HasOnDrag { get; set; }
    public bool HasOnDragStart { get; set; }
    public bool HasOnDragStop { get; set; }

    public Dictionary<string, object> ToInteroperable()
    {
        return new Dictionary<string, object>()
            .AddIfNotNull("hasOnDrag", HasOnDrag)
            .AddIfNotNull("hasOnDragStart", HasOnDragStart)
            .AddIfNotNull("hasOnDragStop", HasOnDragStop)
            .AddIfNotNull("minSize", MinSize)
            .AddIfNotNull("maxSize", MaxSize)
            .AddIfNotNull("minSizes", MinSizes)
            .AddIfNotNull("maxSizes", MaxSizes)
            .AddIfNotNull("columnMinSize", ColumnMinSize)
            .AddIfNotNull("columnMaxSize", ColumnMaxSize)
            .AddIfNotNull("columnMinSizes", ColumnMinSizes)
            .AddIfNotNull("columnMaxSizes", ColumnMaxSizes)
            .AddIfNotNull("rowMinSize", RowMinSize)
            .AddIfNotNull("rowMaxSize", RowMaxSize)
            .AddIfNotNull("rowMinSizes", RowMinSizes)
            .AddIfNotNull("rowMaxSizes", RowMaxSizes)
            .AddIfNotNull("snapOffset", SnapOffset)
            .AddIfNotNull("columnSnapOffset", ColumnSnapOffset)
            .AddIfNotNull("rowSnapOffset", RowSnapOffset)
            .AddIfNotNull("dragInterval", DragInterval)
            .AddIfNotNull("rowDragInterval", RowDragInterval)
            .AddIfNotNull("columnDragInterval", ColumnDragInterval)
            .AddIfNotNull("cursor", Cursor)
            .AddIfNotNull("columnCursor", ColumnCursor)
            .AddIfNotNull("rowCursor", RowCursor);
    }
}
