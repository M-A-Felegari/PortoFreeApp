namespace PortoFree.Application.Exceptions;

public class NotEnoughSpaceException() 
    : ForbiddenException("you dont have enough available space to perform this action");