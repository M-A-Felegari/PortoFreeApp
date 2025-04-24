namespace PortoFree.Application.Exceptions;

public class NotFoundException(Type type, string id) : Exception($"{type.Name} with id '{id}' not found");