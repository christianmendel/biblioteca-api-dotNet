﻿using Biblioteca.Dto.token;
using Biblioteca.Dto.Usuario;
using Biblioteca.Settings.Validations;

namespace Biblioteca.Contracts.Service
{
    public interface IUsuarioService
    {
        Task<Notifiable> CadastraUsuario(IncluirUsuarioRequest incluirUsuarioRequest);
        Task<TokenResponse> Login(LoginRequest loginRequest);
    }
}
