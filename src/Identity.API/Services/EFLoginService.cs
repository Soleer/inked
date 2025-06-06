﻿namespace Inked.Identity.API.Services;

public class EFLoginService : ILoginService<ApplicationUser>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public EFLoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ApplicationUser> FindByUsername(string user)
    {
        return await _userManager.FindByEmailAsync(user);
    }

    public async Task<bool> ValidateCredentials(ApplicationUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public Task SignIn(ApplicationUser user)
    {
        return _signInManager.SignInAsync(user, true);
    }

    public Task SignInAsync(ApplicationUser user, AuthenticationProperties properties,
        string authenticationMethod = null)
    {
        return _signInManager.SignInAsync(user, properties, authenticationMethod);
    }
}
