using System;

namespace HRApp.Web
{
    public interface IUserContext
    {
        Guid? UserId { get;  }
    }
}
