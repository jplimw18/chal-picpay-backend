namespace PicpayChal.App.Entities;

public abstract record BaseEntity
{
    public long Id { get; private set; } = 0L;
}