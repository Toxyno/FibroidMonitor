// Global using directives

global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using FibroidMonitor.Api.Auth;
global using FibroidMonitor.Application.Contracts.FMonitorInterface;
global using FibroidMonitor.Application.Contracts.Repositories;
global using FibroidMonitor.Domain;
global using FibroidMonitor.Domain.Enumeration;
global using FibroidMonitor.Infrastructure;
global using FibroidMonitor.Persistence;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;