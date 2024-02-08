namespace Persistence.Authentication.CurrentUserContext;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}
