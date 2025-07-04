﻿using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Semzeri.DAL.Entities;

namespace Semzeri.API.Extensions;

public static class ClaimPrincipleExtension
{
    public static async Task<AppUser?> GetUserByEmailAsync(this UserManager<AppUser> userManager, ClaimsPrincipal claimsPrincipal)
    {
        return await userManager.Users.FirstOrDefaultAsync(z=>z.Email == claimsPrincipal.GetEmail());
    }
    
    public static string? GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.Email);
    }
}