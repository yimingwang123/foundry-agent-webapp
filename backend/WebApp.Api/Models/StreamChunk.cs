namespace WebApp.Api.Models;

/// <summary>
/// Represents a chunk of streaming response data.
/// Can contain text content, annotations (citations), MCP tool approval requests, or response metadata.
/// </summary>
public record StreamChunk
{
    /// <summary>
    /// Text content chunk (delta). Null if this chunk contains annotations or approval request.
    /// </summary>
    public string? TextDelta { get; init; }
    
    /// <summary>
    /// Annotations/citations extracted from the response. Null if this chunk contains text or approval request.
    /// </summary>
    public List<AnnotationInfo>? Annotations { get; init; }
    
    /// <summary>
    /// MCP tool approval request. Null if this chunk contains text or annotations.
    /// </summary>
    public McpApprovalRequest? McpApprovalRequest { get; init; }
    
    /// <summary>
    /// Response ID from completed streaming. Used to link subsequent messages in a conversation.
    /// </summary>
    public string? ResponseId { get; init; }
    
    /// <summary>
    /// Creates a text delta chunk.
    /// </summary>
    public static StreamChunk Text(string delta) => new() { TextDelta = delta };
    
    /// <summary>
    /// Creates an annotations chunk.
    /// </summary>
    public static StreamChunk WithAnnotations(List<AnnotationInfo> annotations) => new() { Annotations = annotations };
    
    /// <summary>
    /// Creates an MCP approval request chunk.
    /// </summary>
    public static StreamChunk McpApproval(McpApprovalRequest request) => new() { McpApprovalRequest = request };
    
    /// <summary>
    /// Creates a response ID chunk for linking subsequent messages.
    /// </summary>
    public static StreamChunk WithResponseId(string responseId) => new() { ResponseId = responseId };
    
    /// <summary>
    /// Whether this chunk contains text content.
    /// </summary>
    public bool IsText => TextDelta != null;
    
    /// <summary>
    /// Whether this chunk contains annotations.
    /// </summary>
    public bool HasAnnotations => Annotations != null && Annotations.Count > 0;
    
    /// <summary>
    /// Whether this chunk contains an MCP approval request.
    /// </summary>
    public bool IsMcpApprovalRequest => McpApprovalRequest != null;
    
    /// <summary>
    /// Whether this chunk contains a response ID.
    /// </summary>
    public bool HasResponseId => ResponseId != null;
}

/// <summary>
/// Represents an MCP tool call requiring user approval.
/// </summary>
public record McpApprovalRequest
{
    public required string Id { get; init; }
    public required string ToolName { get; init; }
    public required string ServerLabel { get; init; }
    public string? Arguments { get; init; }
}
