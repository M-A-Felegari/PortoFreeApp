namespace PortoFree.Application.Common.Events;

//we get ownerId to reduce his usedSpace after deletion
public record DeleteImageEvent(string Path, int OwnerId); 