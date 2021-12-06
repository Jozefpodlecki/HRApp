using System;

namespace HRApp.Web
{
    public interface IUserContext
    {
        string? Email { get; }
        Guid? UserId { get;  }
    }
}
